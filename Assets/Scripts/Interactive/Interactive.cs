using Managers;
using Utils;

namespace Interactive
{
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class Interactive : MonoBehaviour
    {
        public ItemName requireItem;
        public bool isDone;

        public void CheckItem(ItemName itemName)
        {
            if (itemName == requireItem && !isDone)
            {
                isDone = true;
                OnClickedAction();
                // 使用这个物品，移除物品
                // TODO 假设物品只能用一次
                EventHandler.CallItemUseEvent(itemName);
                EventHandler.CallAfterInteractEvent(name);
            }
        }

        /// <summary>
        /// 默认在使用正确的物品时执行(且未被交互过时才执行)
        /// </summary>
        protected virtual void OnClickedAction()
        {
            // 子类可以重写此方法
        }

        public virtual void EmptyClicked()
        {
            Debug.Log("空点");
        }
    }
}