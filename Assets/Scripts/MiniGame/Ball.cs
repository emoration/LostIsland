using MiniGame.Data;
using UnityEngine;
using Utils;

namespace MiniGame
{
    public class Ball : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public BallDetails ballDetails;
        public bool isMatch;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetupBall(BallDetails ball)
        {
            ballDetails = ball;
            if (isMatch)
                SetRight();
            else
                SetWrong();
        }

        public void SetRight()
        {
            _spriteRenderer.sprite = ballDetails.rightSprite;
        }

        public void SetWrong()
        {
            _spriteRenderer.sprite = ballDetails.wrongSprite;
        }
    }
}