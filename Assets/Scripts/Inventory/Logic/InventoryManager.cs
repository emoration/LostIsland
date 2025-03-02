using System;
using System.Collections.Generic;
using Inventory.Data;
using SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using EventHandler = Utils.EventHandler;

namespace Inventory.Logic
{
    public class InventoryManager : Singleton<InventoryManager>, ISaveable
    {
        public ItemDataList_SO itemData;
        [SerializeField] public List<ItemName> items;

        private void Start()
        {
            InitItems();
            ISaveable saveable = this;
            saveable.SaveableRegister();
        }

        private void OnEnable()
        {
            EventHandler.ItemUseEvent += OnItemUseEvent;
            EventHandler.AfterItemPickupEvent += AddItem;
            EventHandler.StartNewGameEvent += ResetItems;
        }

        private void OnDisable()
        {
            EventHandler.ItemUseEvent -= OnItemUseEvent;
            EventHandler.AfterItemPickupEvent -= AddItem;
            EventHandler.StartNewGameEvent -= ResetItems;
        }

        // 物品被使用时
        private void OnItemUseEvent(ItemName itemName)
        {
            // 物品被使用后从背包中移除
            var index = items.IndexOf(itemName);
            if (index != -1)
            {
                items.RemoveAt(index);
                EventHandler.CallAfterInventoryRemoveEvent(itemData.GetItemDetails(itemName), index);
            }
            else
            {
                Debug.LogWarning("试图移除物品:" + itemName + "，但是物品不在背包中");
            }
        }

        // 添加物品
        public void AddItem(ItemName itemName)
        {
            // 物品是唯一的
            if (!items.Contains(itemName))
                items.Add(itemName);
            // 触发事件
            EventHandler.CallAfterInventoryAddEvent(itemData.GetItemDetails(itemName), items.Count - 1);
        }

        // 初始化物品列表
        public void InitItems(List<ItemName> itemNames)
        {
            // 初始化物品列表
            items = itemNames ?? new List<ItemName>();
            // 触发事件
            EventHandler.CallAfterInventoryLoadEvent();
        }

        // 初始化物品列表
        public void ResetItems(int obj)
        {
            InitItems(null);
        }

        public void InitItems()
        {
            InitItems(null);
        }

        public GameSaveData GenerateSaveData()
        {
            var saveData = new GameSaveData
            {
                items = items
            };
            return saveData;
        }

        public void RestoreSaveData(GameSaveData saveData)
        {
            InitItems(saveData.items);
        }
    }
}