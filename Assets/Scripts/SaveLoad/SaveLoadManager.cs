using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Utils;
using EventHandler = Utils.EventHandler;

namespace SaveLoad
{
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        private string jsonFolder;
        private List<ISaveable> saveableList = new();
        private Dictionary<string, GameSaveData> saveDataDict = new();


        protected override void Awake()
        {
            base.Awake();
            jsonFolder = Application.persistentDataPath + "/SaveData/";
        }

        private void OnEnable()
        {
            EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        }

        private void OnDisable()
        {
            EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        }

        public void Register(ISaveable saveable)
        {
            saveableList.Add(saveable);
        }

        private void OnStartNewGameEvent(int obj)
        {
            var resultPath = jsonFolder + "SaveData" + obj + ".json";
            if (File.Exists(resultPath))
                File.Delete(resultPath);
        }

        public void Save()
        {
            saveDataDict.Clear();

            foreach (var saveable in saveableList)
            {
                var saveData = saveable.GenerateSaveData();
                saveDataDict.Add(saveable.GetType().Name, saveData);
            }

            var resultPath = jsonFolder + "SaveData" + ".json";
            var jsonString = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);

            if (!File.Exists(jsonFolder))
                Directory.CreateDirectory(jsonFolder);

            File.WriteAllText(resultPath, jsonString);
        }

        public void Load()
        {
            var resultPath = jsonFolder + "SaveData" + ".json";

            if (!File.Exists(resultPath))
            {
                Debug.LogWarning("Save file not found");
                return;
            }

            var jsonString = File.ReadAllText(resultPath);

            saveDataDict = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(jsonString);

            foreach (var saveable in saveableList)
            {
                var saveData = saveDataDict[saveable.GetType().Name];
                saveable.RestoreSaveData(saveData);
            }
        }
    }
}