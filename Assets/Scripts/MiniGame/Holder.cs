using System.Collections.Generic;
using Utils;

namespace MiniGame
{
    public class Holder : Interactive.Interactive
    {
        public BallName matchBall;
        private Ball currentBall;
        public HashSet<Holder> linkedHolders = new();
        public bool isEmpty;

        public void CheckBall(Ball ball)
        {
            currentBall = ball;
            if (ball.ballDetails.ballName == matchBall)
            {
                currentBall.isMatch = true;
                currentBall.SetRight();
            }
            else
            {
                ball.isMatch = false;
                ball.SetWrong();
            }
        }

        public override void EmptyClicked()
        {
            foreach (var linkedHolder in linkedHolders)
            {
                if (!linkedHolder.isEmpty) continue;

                // 移动球
                currentBall.transform.position = linkedHolder.transform.position;
                // 设置父亲
                currentBall.transform.SetParent(linkedHolder.transform);

                linkedHolder.CheckBall(currentBall);
                linkedHolder.isEmpty = false;

                this.currentBall = null;
                this.isEmpty = true;

                EventHandler.CallMiniGameCheckEvent();
            }
        }
    }
}