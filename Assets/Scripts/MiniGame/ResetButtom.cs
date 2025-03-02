using System;
using DG.Tweening;
using UnityEngine;

namespace MiniGame
{
    public class ResetButton : Interactive.Interactive
    {
        private Transform gear;

        private void Awake()
        {
            gear = transform.GetChild(0);
        }

        public override void EmptyClicked()
        {
            MiniGameController.Instance.ResetGame();
            gear.DOPunchRotation(Vector3.forward * 100, 1, 1, 0);
        }
    }
}