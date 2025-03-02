using Inventory.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Button LeftButton;
        public Button RightButton;
        public SlotUI slotUI;
        public int currentIndex = -1;

        private void OnEnable()
        {
            Utils.EventHandler.AfterInventoryAddEvent += OnAfterInventoryAdd;
            Utils.EventHandler.AfterInventoryRemoveEvent += onAfterInventoryRemove;
            Utils.EventHandler.AfterInventoryLoadEvent += OnAfterInventoryLoadEvent;

            Utils.EventHandler.AfterInventoryAddEvent += UpdateButtonAvailability;
            Utils.EventHandler.AfterInventoryRemoveEvent += UpdateButtonAvailability;
            Utils.EventHandler.AfterInventoryLoadEvent += UpdateButtonAvailability;
        }

        private void OnDisable()
        {
            Utils.EventHandler.AfterInventoryAddEvent -= OnAfterInventoryAdd;
            Utils.EventHandler.AfterInventoryRemoveEvent -= onAfterInventoryRemove;
            Utils.EventHandler.AfterInventoryLoadEvent -= OnAfterInventoryLoadEvent;

            Utils.EventHandler.AfterInventoryAddEvent -= UpdateButtonAvailability;
            Utils.EventHandler.AfterInventoryRemoveEvent -= UpdateButtonAvailability;
            Utils.EventHandler.AfterInventoryLoadEvent -= UpdateButtonAvailability;
        }

        private void OnAfterInventoryAdd(Data.ItemDetails itemDetails, int index)
        {
            if (itemDetails == null)
            {
                Debug.LogWarning("试图添加空物品到背包");
                return;
            }

            slotUI.SetItem(itemDetails);
            currentIndex = index;
        }

        private void onAfterInventoryRemove(Data.ItemDetails itemDetails, int index)
        {
            // 如果没有物品了，重置UI
            if (Logic.InventoryManager.Instance.items.Count == 0)
            {
                ResetUI();
                return;
            }

            // 如果移除的不是当前显示的物品，不进行操作
            if (index != currentIndex)
                return;

            // 如果移除的是当前显示的物品
            // 移除当前物品后，自动显示背包中的下一个物品(如果是最后一个物品，则显示前一个)
            // 计算currentIndex
            if (index == Logic.InventoryManager.Instance.items.Count)
            {
                currentIndex = index - 1;
            }
            else
            {
                currentIndex = index;
            }

            // 更新UI
            slotUI.SetItem(
                Logic.InventoryManager.Instance.itemData.GetItemDetails(
                    Logic.InventoryManager.Instance.items[currentIndex]));
        }

        private void OnAfterInventoryLoadEvent()
        {
            if (Logic.InventoryManager.Instance.items.Count == 0)
            {
                ResetUI();
                return;
            }

            slotUI.SetItem(
                Logic.InventoryManager.Instance.itemData.GetItemDetails(
                    Logic.InventoryManager.Instance.items[0]));
        }

        private void ResetUI()
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            UpdateButtonAvailability();
        }

        public void SwitchItem(int offset)
        {
            if (Logic.InventoryManager.Instance.items.Count == 0)
                return;

            currentIndex += offset;
            if (currentIndex < 0)
                currentIndex = 0;
            if (currentIndex >= Logic.InventoryManager.Instance.items.Count)
                currentIndex = Logic.InventoryManager.Instance.items.Count - 1;

            slotUI.SetItem(
                Logic.InventoryManager.Instance.itemData.GetItemDetails(
                    Logic.InventoryManager.Instance.items[currentIndex]));
            UpdateButtonAvailability();
        }


        private void UpdateButtonAvailability()
        {
            LeftButton.interactable = currentIndex != -1 && currentIndex > 0;
            RightButton.interactable =
                currentIndex != -1 && currentIndex < Logic.InventoryManager.Instance.items.Count - 1;
        }

        private void UpdateButtonAvailability(ItemDetails itemDetails, int index)
        {
            UpdateButtonAvailability();
        }
    }
}