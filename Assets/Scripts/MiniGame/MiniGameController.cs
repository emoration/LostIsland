using Managers;
using EventHandler = Utils.EventHandler;
using MiniGame.Data;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace MiniGame
{
    public class MiniGameController : Singleton<MiniGameController>
    {
        public UnityEvent OnGameEnd = new();
        public MiniGame_SO miniGame_SO;
        public MiniGame_SO[] miniGame_SOList;
        public GameObject lineParent;
        public LineRenderer lineRenderer;
        public Ball ballPrefab;
        public Transform[] holderTransforms;

        private void Start()
        {
            miniGame_SO = miniGame_SOList[StateSaveManager.week - 1];
            DrawLine();
            CreateBall();
        }

        private void OnEnable()
        {
            EventHandler.MiniGameCheckEvent += CheckGame;
        }

        private void OnDisable()
        {
            EventHandler.MiniGameCheckEvent -= CheckGame;
        }

        private void CheckGame()
        {
            foreach (var ball in FindObjectsOfType<Ball>())
            {
                if (!ball.isMatch)
                    return;
            }

            foreach (var holder in holderTransforms)
            {
                holder.GetComponent<Collider2D>().enabled = false;
            }

            StateSaveManager.activatableStates["door"] = false;
            StateSaveManager.activatableStates["Teleport from H2 to H3"] = true;

            OnGameEnd.Invoke();
        }

        public void ResetGame()
        {
            for (int i = 0; i < lineParent.transform.childCount; i++)
            {
                Destroy(lineParent.transform.GetChild(i).gameObject);
            }

            foreach (var holder in holderTransforms)
            {
                if (holder.childCount == 0)
                    continue;

                Destroy(holder.GetChild(0).gameObject);
            }

            DrawLine();
            CreateBall();
        }

        public void DrawLine()
        {
            foreach (var connection in miniGame_SO.connectionDetails)
            {
                var line = Instantiate(lineRenderer, lineParent.transform);
                line.SetPosition(0, holderTransforms[connection.from].position);
                line.SetPosition(1, holderTransforms[connection.to].position);

                // 创建连接关系
                holderTransforms[connection.from].GetComponent<Holder>().linkedHolders
                    .Add(holderTransforms[connection.to].GetComponent<Holder>());
                holderTransforms[connection.to].GetComponent<Holder>().linkedHolders
                    .Add(holderTransforms[connection.from].GetComponent<Holder>());
            }
        }

        public void CreateBall()
        {
            for (int i = 0; i < miniGame_SO.startBallOrder.Count; i++)
            {
                if (miniGame_SO.startBallOrder[i] == BallName.None)
                {
                    holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                    continue;
                }

                Ball ball = Instantiate(ballPrefab, holderTransforms[i]);

                holderTransforms[i].GetComponent<Holder>().CheckBall(ball);
                holderTransforms[i].GetComponent<Holder>().isEmpty = false;
                ball.SetupBall(miniGame_SO.GetBallDetails(miniGame_SO.startBallOrder[i]));
            }
        }
    }
}