using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Inventory.UI
{
    public class ItemTooltip : MonoBehaviour
    {
        public Text nameText;

        public void UpdateItemName(ItemName itemName)
        {
            nameText.text = itemName switch
            {
                ItemName.Key => "信箱钥匙",
                ItemName.Ticket => "一张船票",
                _ => ""
            };
        }
    }
}