using System;
using Inventory.Data;

namespace Utils
{
    public static class EventHandler
    {
        // 背包物品新增后事件
        public static event Action<ItemDetails, int> AfterInventoryAddEvent;

        public static void CallAfterInventoryAddEvent(ItemDetails itemDetails, int index)
        {
            AfterInventoryAddEvent?.Invoke(itemDetails, index);
        }

        // 背包物品减少后事件
        public static event Action<ItemDetails, int> AfterInventoryRemoveEvent;

        public static void CallAfterInventoryRemoveEvent(ItemDetails itemDetails, int index)
        {
            AfterInventoryRemoveEvent?.Invoke(itemDetails, index);
        }

        // 背包物品加载后事件
        public static event Action AfterInventoryLoadEvent;

        public static void CallAfterInventoryLoadEvent()
        {
            AfterInventoryLoadEvent?.Invoke();
        }

        // 物品被拾取后事件
        public static event Action<ItemName> AfterItemPickupEvent;

        public static void CallAfterItemPickupEvent(ItemName itemName)
        {
            AfterItemPickupEvent?.Invoke(itemName);
        }

        // 物品被使用时事件
        public static event Action<ItemName> ItemUseEvent;

        public static void CallItemUseEvent(ItemName itemName)
        {
            ItemUseEvent?.Invoke(itemName);
        }

        // 物品变为可用时事件
        public static event Action<ItemName> AfterItemEnableEvent;

        public static void CallAfterItemEnableEvent(ItemName itemName)
        {
            AfterItemEnableEvent?.Invoke(itemName);
        }

        // 交互体被交互时事件
        public static event Action<string> AfterInteractEvent;

        public static void CallAfterInteractEvent(string interactName)
        {
            AfterInteractEvent?.Invoke(interactName);
        }

        // 场景卸载前事件
        public static event Action BeforeSceneUnloadEvent;

        public static void CallBeforeSceneUnloadEvent()
        {
            BeforeSceneUnloadEvent?.Invoke();
        }

        // 场景加载后事件
        public static event Action AfterSceneLoadEvent;

        public static void CallAfterSceneLoadEvent()
        {
            AfterSceneLoadEvent?.Invoke();
        }

        // 物品被选择的事件
        public static event Action<ItemDetails, bool> ItemSelectEvent;

        public static void CallItemSelectEvent(ItemDetails itemDetails, bool isSelected)
        {
            ItemSelectEvent?.Invoke(itemDetails, isSelected);
        }

        // 显示对话的事件
        public static event Action<string> ShowDialogueEvent;

        public static void CallShowDialogueEvent(string dialogue)
        {
            ShowDialogueEvent?.Invoke(dialogue);
        }

        // 游戏状态改变的事件
        public static event Action<GameState> GameStateChangeEvent;

        public static void CallGameStateChangeEvent(GameState isPaused)
        {
            GameStateChangeEvent?.Invoke(isPaused);
        }

        // 小游戏检查是否完成的事件
        public static event Action MiniGameCheckEvent;

        public static void CallMiniGameCheckEvent()
        {
            MiniGameCheckEvent?.Invoke();
        }
        
        // 开始新游戏的事件
        public static event Action<int> StartNewGameEvent;
        
        public static void CallStartNewGameEvent(int gameWeek)
        {
            StartNewGameEvent?.Invoke(gameWeek);
        }
    }
}