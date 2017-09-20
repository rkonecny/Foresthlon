using Assets.assets.Scripts.Leaderboard;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.assets.Scripts.Menu
{
    /**
    * <summary>Class for interacting with the main menu</summary>
    **/
    public class MenuController : MonoBehaviour
    {
        public GameObject[] MenuObjects;

        public bool IsInMenu;

        public Text PlayButtonText;

        public LeaderboardController LeaderboardController;

        public TGCConnectionController InputController;

        public DisplayData ConnectionStatusDisplay;

        private const string Play = "Play";
        private const string Resume = "Resume";

        /**
        * <summary>Shows the screen at the start of the game</summary>
        **/
        public void ShowStartMenu()
        {
            LeaderboardController.CloseLeaderboard();
            ShowMenu();
            PlayButtonText.text = Play;
        }

        /**
        * <summary>Shows the menu screen</summary>
        **/
        public void ShowMenu()
        {
            Cursor.visible = true;

            IsInMenu = true;

            foreach (var menuObject in MenuObjects)
            {
                menuObject.SetActive(true);
            }

            PlayButtonText.text = Resume;
        }

        /**
        * <summary>Hides the menu screen</summary>
        **/
        public void HideMenu()
        {
            Cursor.visible = false;

            IsInMenu = false;

            foreach (var menuObject in MenuObjects)
            {
                menuObject.SetActive(false);
            }
        }

        /**
        * <summary>Restarts the game - the player is back at start and the current attempt is discarded</summary>
        **/
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /**
        * <summary>Shows the leaderboard screen</summary>
        **/
        public void ShowLeaderboard()
        {
            LeaderboardController.OpenLeaderboard();
        }

        /**
        * <summary>Exits the game</summary>
        **/
        public void ExitGame()
        {
            Application.Quit();
        }

        /**
        * <summary>Reconnects to Neurosky headset</summary>
        **/
        public void ReconnectInput()
        {
            InputController.Connect();
        }

        /**
        * <summary>Disconnects the Neurosky headset</summary>
        **/
        public void DisconnectInput()
        {
            InputController.Disconnect();
            ConnectionStatusDisplay.indexSignalIcons = 1;
        }
    }
}