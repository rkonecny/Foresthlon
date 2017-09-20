using Assets.assets.Scripts.Data;
using Assets.assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.assets.Scripts.Leaderboard
{
    /**
     * <summary>Class for handling the leaderboard</summary>
     **/
    public class LeaderboardController : MonoBehaviour
    {
        public Text Header;

        public Text LeaderboardContent;

        private AttemptCollectionHandler attemptCollectionHandler;

        private GameObject[] leaderboardObjects;


        void Start()
        {
            CloseLeaderboard();
        }

        /**
         * <summary>Opens the leaderboard</summary>
         **/
        public void OpenLeaderboard()
        {
            ToggleVisibility(true);
            SetHeader();
            GetData();
        }

        /**
         * <summary>Closes the leaderboard</summary>
         **/
        public void CloseLeaderboard()
        {
            LoadGameObjects();
            ToggleVisibility(false);
        }

        /**
         * <summary>Loads all game objects connected to the leaderboard and initialises 
         * the AttemptCollectionHandler for storing the leaderboard to the disk</summary>
         **/
        private void LoadGameObjects()
        {
            if (leaderboardObjects == null)
            {
                leaderboardObjects = GameObject.FindGameObjectsWithTag("Leaderboard");
            }

            if (attemptCollectionHandler == null)
            {
                attemptCollectionHandler = new AttemptCollectionHandler();
            }
        }

        /**
         * <summary>Toggles the visibility of the leaderboard</summary>
         * <param name="shouldBeVisible">True if the finish screen should be visible, false otherwise</param>
         **/
        private void ToggleVisibility(bool shouldBeVisible)
        {
            foreach (var finishScreenObject in leaderboardObjects)
            {
                finishScreenObject.SetActive(shouldBeVisible);
            }
        }

        /**
         * <summary>Sets the header of the leaderboard</summary>
         **/ 
        private void SetHeader()
        {
            Header.text = Constants.GetLeaderboardHeaderString();
        }

        /**
         * <summary>Loads all attempts in the leaderboard and displays it on the leaderboard screen</summary>
         **/ 
        private void GetData()
        {
            LeaderboardContent.text = attemptCollectionHandler.LoadAttemptCollection().ToString();
        }
    }
}