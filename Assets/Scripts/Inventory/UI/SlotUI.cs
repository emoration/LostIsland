using Inventory.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Inventory.UI
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image itemImage;
        public ItemDetails itemDetails;
        public ItemTooltip itemTooltip;
        private bool _isSelected;

        public void SetItem(ItemDetails itemDetails)
        {
            this.itemDetails = itemDetails;
            this.gameObject.SetActive(true);
            itemImage.sprite = itemDetails.itemSprite;
            itemImage.SetNativeSize();
        }

        public void SetEmpty()
        {
            itemDetails = null;
            itemImage.sprite = null;
            this.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isSelected = !_isSelected;
            EventHandler.CallItemSelectEvent(itemDetails, _isSelected);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (gameObject.activeSelf)
            {
                itemTooltip.gameObject.SetActive(true);
                itemTooltip.UpdateItemName(itemDetails.itemName);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemTooltip.gameObject.SetActive(false);
        }
    }
}