using Assets.assets.Scripts.Data;
using Assets.assets.Scripts.Data.Entities;
using Assets.assets.Scripts.Helpers;
using Assets.assets.Scripts.Leaderboard;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.assets.Scripts.FinishScreen
{
    /**
    * <summary> Controller of the finish screen </summary>
    **/
    public class FinishScreenController : MonoBehaviour
    {
        public Text BoardText1;
        public Text BoardText2;
        public Text BoardText3;
        public Text BoardText4;
        public Text TotalText;

        public Text FinalText;

        public InputField NameField;
        public Button OkButton;

        public LeaderboardController LeaderboardController;

        public AttemptLoggerController Logger;

        public bool IsScreenDisplayed;

        private GameObject[] finishScreenObjects;

        private string playerName;
        private float playerTime;
        private List<int> playerShootings;

        private AttemptCollectionHandler attemptCollectionHandler;


        void Start()
        {
            LoadGameObjects();
        }

        /**
        * <summary> Hides finish screen </summary>
        **/
        public void HideFinishScreen()
        {
            LoadGameObjects();
            ToggleVisibility(false);
        }

        /**
        * <summary> Shows finish screen </summary>
        **/
        public void ShowFinishScreen(IDictionary<int, int> shootingRangeResults, float time)
        {
            playerTime = time;
            playerShootings = shootingRangeResults.Values.ToList<int>();

            ToggleVisibility(true);
            SetBoardScores();
            SetFinalText();
        }

        /**
        * <summary> Saving the attempt after entering player name </summary>
        **/
        public void SaveRun()
        {
            GetNameAndDisableTheInput();
            SaveAttempt();
        }

        /**
        * <summary> Save the attempt to the leaderboard and log the run </summary>
        **/
        private void SaveAttempt()
        {
            var attempt = new Attempt
            {
                Name = playerName,
                Time = playerTime,
                Board1 = playerShootings[0],
                Board2 = playerShootings[1],
                Board3 = playerShootings[2],
                Board4 = playerShootings[3]
            };
            attemptCollectionHandler.SaveAttempt(attempt);

            Logger.AddAttemptHeader(attempt);
            Logger.SaveAttemptLog();
        }

        /**
        * <summary> Loads uninitialised objects </summary>
        **/
        private void LoadGameObjects()
        {
            if (finishScreenObjects == null)
            {
                finishScreenObjects = GameObject.FindGameObjectsWithTag("FinishScreen");
            }

            if (attemptCollectionHandler == null)
            {
                attemptCollectionHandler = new AttemptCollectionHandler();
            }
        }

        /**
        * <summary> Get the name and disable the inputfield afterwards </summary>
        **/
        private void GetNameAndDisableTheInput()
        {
            if (!string.IsNullOrEmpty(NameField.text))
            {
                playerName = NameField.text;

                OkButton.interactable = false;
                NameField.readOnly = true;
            }
        }

        /**
        * <summary> Toggles visibility of the finish screen </summary>
        * <param name="shouldBeVisible">True if the finish screen should be visible, false otherwise</param>
        **/
        private void ToggleVisibility(bool shouldBeVisible)
        {
            IsScreenDisplayed = shouldBeVisible;

            Cursor.visible = shouldBeVisible;

            foreach (var finishScreenObject in finishScreenObjects)
            {
                finishScreenObject.SetActive(shouldBeVisible);
            }
        }

        /**
        * <summary> Sets attempts needed at every shooting </summary>
        **/
        private void SetBoardScores()
        {
            BoardText1.text = playerShootings[0].ToString();
            BoardText2.text = playerShootings[1].ToString();
            BoardText3.text = playerShootings[2].ToString();
            BoardText4.text = playerShootings[3].ToString();

            TotalText.text = playerShootings.Sum().ToString();
        }

        /**
        * <summary> Sets the time and the position in the leaderboard </summary>
        **/
        private void SetFinalText()
        {
            FinalText.text = string.Format("Your time: {0} - that is #{1} on the leaderboard!", 
                TimeHelper.GetTimeText(playerTime), attemptCollectionHandler.GetPositionByTime(playerTime));
        }

        /**
        * <summary> Restarts game if the attempt was already saved </summary>
        **/
        public void RestartGame()
        {
            if (!string.IsNullOrEmpty(playerName))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        /** 
        * <summary> Shows leaderboard if the attempt was already saved </summary>
        **/
        public void ShowLeaderboard()
        {
            if (!string.IsNullOrEmpty(playerName))
            {
                LeaderboardController.OpenLeaderboard();
            }
        }

        /**
        * <summary> Exits the game if the attempt was already saved </summary>
        **/
        public void ExitGame()
        {

            if (!string.IsNullOrEmpty(playerName))
            {
                Application.Quit();
            }
        }
    }
}