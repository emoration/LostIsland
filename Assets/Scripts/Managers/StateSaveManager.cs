using System;
using System.Collections.Generic;
using Inventory.Logic;
using SaveLoad;
using UnityEngine;
using Utils;
using EventHandler = Utils.EventHandler;

namespace Managers
{
    /// <summary>
    /// 保存场景中的物体的状态
    /// </summary>
    public class StateSaveManager : Singleton<StateSaveManager>, ISaveable
    {
        // 保存Item状态(字典)
        public static Dictionary<ItemName, bool> itemStates = new();
        // 保存Interactive状态(字典)
        public static Dictionary<string, bool> interactiveStates = new();
        // 保存可激活物体的状态(字典)
        public static Dictionary<string, bool> activatableStates = new()
        {
            { "Teleport from H2 to H3", false }
        };
        // 周目
        public static int week = 1;

        private void Start()
        {
            ISaveable saveable = this;
            saveable.SaveableRegister();
        }

        private void OnEnable()
        {
            EventHandler.BeforeSceneUnloadEvent += SaveSceneStates;
            EventHandler.AfterSceneLoadEvent += RecoverSceneStates;
            EventHandler.AfterItemPickupEvent += SetItemStatesInactive;
            EventHandler.AfterItemEnableEvent += SetItemStatesActive;
            EventHandler.AfterInteractEvent += SetInteractiveStatesDone;
            EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        }

        private void OnDisable()
        {
            EventHandler.BeforeSceneUnloadEvent -= SaveSceneStates;
            EventHandler.AfterSceneLoadEvent -= RecoverSceneStates;
            EventHandler.AfterItemPickupEvent -= SetItemStatesInactive;
            EventHandler.AfterItemEnableEvent -= SetItemStatesActive;
            EventHandler.AfterInteractEvent -= SetInteractiveStatesDone;
            EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        }

        private void OnStartNewGameEvent(int obj)
        {
            itemStates.Clear();
            interactiveStates.Clear();
            activatableStates = new()
            {
                { "Teleport from H2 to H3", false }
            };
            week = obj;
        }

        // 保存整个场景中物体的状态
        private void SaveSceneStates()
        {
            foreach (var item in FindObjectsOfType<Item>()) // 能找到的Item都是激活的(找不到未激活的物体)
            {
                itemStates[item.itemName] = true;
            }

            foreach (var interactive in FindObjectsOfType<Interactive.Interactive>())
            {
                interactiveStates[interactive.name] = interactive.isDone;
            }

            foreach (var activatable in FindObjectsOfType<Activatable>())
            {
                activatableStates[activatable.name] = true;
            }
        }

        // 恢复整个场景中物体的状态
        private void RecoverSceneStates()
        {
            foreach (var item in FindObjectsOfType<Item>())
            {
                // 如果物体存在于字典中，根据字典中的值设置物体的激活状态(如果不存在，不做任何操作)
                if (itemStates.TryGetValue(item.itemName, out var state))
                {
                    item.gameObject.SetActive(state);
                }
            }

            foreach (var interactive in FindObjectsOfType<Interactive.Interactive>())
            {
                if (interactiveStates.TryGetValue(interactive.name, out var state))
                {
                    interactive.isDone = state;
                }
            }

            foreach (var activatable in FindObjectsOfType<Activatable>())
            {
                if (activatableStates.TryGetValue(activatable.name, out var state))
                {
                    activatable.gameObject.SetActive(state);
                }
            }
        }

        // 保存物体状态，物品被收集后取消激活
        private void SetItemStatesInactive(ItemName itemName)
        {
            itemStates[itemName] = false;
        }

        private void SetItemStatesActive(ItemName obj)
        {
            itemStates[obj] = true;
        }

        // 保存交互体状态，交互体被交互后设置为true
        private void SetInteractiveStatesDone(string interactName)
        {
            interactiveStates[interactName] = true;
        }

        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new GameSaveData
            {
                gameWeek = week,
                itemStates = itemStates,
                interactiveStates = interactiveStates,
                activatableStates = activatableStates
            };
            return saveData;
        }

        public void RestoreSaveData(GameSaveData saveData)
        {
            week = saveData.gameWeek;
            itemStates = saveData.itemStates;
            interactiveStates = saveData.interactiveStates;
            activatableStates = saveData.activatableStates;
        }
    }
}