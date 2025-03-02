using System;
using MiniGame;
using SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using EventHandler = Utils.EventHandler;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        void Start()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
            EventHandler.CallGameStateChangeEvent(GameState.Gameplay);
        }
    }
}