using Inventory.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Cursor
{
    public class CursorManager : MonoBehaviour
    {
        // 鼠标控制的手型图片的位置
        public Transform cursorTransform;
        // 鼠标点击的物体
        private ItemName _clickedItem;
        // 鼠标是否拿着物品
        private bool _isHoldingItem;

        /// 鼠标在世界坐标的位置
        private static Vector3 MouseWorldPos =>
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8));

        /// 检测鼠标点击范围的碰撞体
        private static Collider2D Collider2DAtMousePosition =>
            Physics2D.OverlapPoint(MouseWorldPos);
        /// 是否可以点击
        private bool CanClick =>
            Collider2DAtMousePosition != null;

        private void Update()
        {
            // 如果鼠标拿着物品，手型图片跟随鼠标
            if (_isHoldingItem)
                cursorTransform.position = Input.mousePosition;

            if (IsInteractingWithUI())
                return;

            if (CanClick && Input.GetMouseButtonDown(0))
            {
                ClickAction(Collider2DAtMousePosition.gameObject);
            }
        }

        private void OnEnable()
        {
            EventHandler.ItemSelectEvent += OnItemSelectEvent;
            EventHandler.ItemUseEvent += OnItemUseEvent;
        }

        private void OnDisable()
        {
            EventHandler.ItemSelectEvent -= OnItemSelectEvent;
            EventHandler.ItemUseEvent -= OnItemUseEvent;
        }

        private void OnItemUseEvent(ItemName obj)
        {
            _clickedItem = ItemName.None;
            _isHoldingItem = false;
            cursorTransform.gameObject.SetActive(false);
        }

        private void OnItemSelectEvent(ItemDetails itemDetails, bool isSelected)
        {
            _isHoldingItem = isSelected;
            if (isSelected)
                _clickedItem = itemDetails.itemName;
            else
                _clickedItem = ItemName.None;
            cursorTransform.gameObject.SetActive(_isHoldingItem);
        }

        private void ClickAction(GameObject clickObject)
        {
            switch (clickObject.tag)
            {
                case "Teleport":
                    var teleport = clickObject.GetComponent<Teleport.Teleport>();
                    if (teleport == null)
                        Debug.LogWarning("Teleport component not found");
                    teleport?.TeleportToScene();
                    break;
                case "Item":
                    var item = clickObject.GetComponent<Inventory.Logic.Item>();
                    if (item == null)
                        Debug.LogWarning("Item component not found");
                    item?.ItemClicked();
                    break;
                case "Interactive":
                    var interactive = clickObject.GetComponent<Interactive.Interactive>();
                    if (_isHoldingItem)
                        interactive?.CheckItem(_clickedItem);
                    else
                        interactive?.EmptyClicked();
                    break;
            }
        }

        private bool IsInteractingWithUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}