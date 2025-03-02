using System;
using Inventory.Logic;
using Managers;
using UnityEngine;
using EventHandler = Utils.EventHandler;

namespace Interactive
{
    public class MailBox : Interactive
    {
        private SpriteRenderer spriteRenderer;

        private BoxCollider2D boxCollider2D;

        public Sprite openSprite;

        public Item mailItem;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            Utils.EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        }

        private void OnDisable()
        {
            Utils.EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        }

        // 注册事件，isDone时(信箱被打开)，场景加载后把mailbox的sprite设置为openSprite、关闭boxCollider2D
        // 如果信箱被打开，不显示mail(取消激活)
        private void OnAfterSceneLoadEvent()
        {
            if (isDone) // 信箱被打开
            {
                spriteRenderer.sprite = openSprite;
                boxCollider2D.enabled = false;
                if (StateSaveManager.itemStates.ContainsKey(mailItem.itemName))
                {
                    mailItem.gameObject.SetActive(StateSaveManager.itemStates[mailItem.itemName]);
                }
                else
                {
                    mailItem.gameObject.SetActive(true);
                }
            }
            else
            {
                mailItem.gameObject.SetActive(false);
            }
        }

        protected override void OnClickedAction()
        {
            spriteRenderer.sprite = openSprite;
            boxCollider2D.enabled = false;
            mailItem.gameObject.SetActive(true);
            EventHandler.CallAfterItemEnableEvent(mailItem.itemName);
        }
    }
}