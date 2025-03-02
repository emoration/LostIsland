using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Inventory.Data
{
    [CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "SO/Inventory/ItemDataList_SO")]
    public class ItemDataList_SO : ScriptableObject
    {
        public List<ItemDetails> itemDetailsList;
        // 查找物品details的方法
        public ItemDetails GetItemDetails(ItemName itemName)
        {
            return itemDetailsList.Find(x => x.itemName == itemName);
        }
    }

    [Serializable]
    public class ItemDetails
    {
        public ItemName itemName;
        public Sprite itemSprite;
    }
}