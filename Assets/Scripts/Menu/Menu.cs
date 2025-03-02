using SaveLoad;
using Teleport;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menu
{
    public class Menu : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void StartGame(int gameWeek)
        {
            EventHandler.CallStartNewGameEvent(gameWeek);
        }

        public void ContinueGame()
        {
            SaveLoadManager.Instance.Load();
        }

        public void GoBackToMenu()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            TeleportManager.Instance.Transition(currentScene, "Menu");
            SaveLoadManager.Instance.Save();
        }
    }
}