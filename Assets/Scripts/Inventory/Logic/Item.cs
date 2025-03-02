using UnityEngine;
using Utils;

namespace Inventory.Logic
{
    public class Item: MonoBehaviour
    {
        public ItemName itemName;
        
        // 点击物品时触发的方法
        public void ItemClicked()
        {
            // InventoryManager.Instance.AddItem(itemName);
            gameObject.SetActive(false);
            EventHandler.CallAfterItemPickupEvent(itemName);
        }
    }
}