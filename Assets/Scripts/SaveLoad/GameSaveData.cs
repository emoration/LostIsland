using System;
using System.Collections.Generic;
using Utils;

namespace SaveLoad
{
    [Serializable]
    public class GameSaveData
    {
        // 保存当前场景
        public string currentScene;
        
        // 保存Item状态(字典)
        public Dictionary<ItemName, bool> itemStates = new();
        // 保存Interactive状态(字典)
        public Dictionary<string, bool> interactiveStates = new();
        // 保存可激活物体的状态(字典)
        public Dictionary<string, bool> activatableStates = new();
        // 保存周目
        public int gameWeek;
        
        // 保存背包物品
        public List<ItemName> items;
    }
}