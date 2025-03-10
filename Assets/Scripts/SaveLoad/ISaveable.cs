﻿namespace SaveLoad
{
    public interface ISaveable
    {
        void SaveableRegister()
        {
            SaveLoadManager.Instance.Register(this);
        }

        GameSaveData GenerateSaveData();
        
        void RestoreSaveData(GameSaveData saveData);
    }
}