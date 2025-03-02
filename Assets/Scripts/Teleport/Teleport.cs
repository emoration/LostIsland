using UnityEngine;

namespace Teleport
{
    public class Teleport : MonoBehaviour
    {
        public string fromScene;
        public string toScene;

        // 场景切换
        public void TeleportToScene()
        {
            TeleportManager.Instance.Transition(fromScene, toScene);
        }
    }
}