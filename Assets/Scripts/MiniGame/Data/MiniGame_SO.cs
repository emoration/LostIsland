using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace MiniGame.Data
{
    [CreateAssetMenu(fileName = "MiniGame", menuName = "SO/MiniGame/MiniGame_SO")]
    public class MiniGame_SO : ScriptableObject
    {
        [Header("球的名字和对应的图片")]
        public List<BallDetails> ballDetails;

        [Header("游戏逻辑数据")]
        public List<ConnectionDetails> connectionDetails;
        public List<BallName> startBallOrder;

        public BallDetails GetBallDetails(BallName ballName)
        {
            return ballDetails.Find(x => x.ballName == ballName);
        }
    }

    [Serializable]
    public class BallDetails
    {
        public BallName ballName;
        public Sprite rightSprite;
        public Sprite wrongSprite;
    }

    [Serializable]
    public class ConnectionDetails
    {
        public int from;
        public int to;
    }
}