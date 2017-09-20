using System.Collections.Generic;
using UnityEngine;
using Assets.assets.Scripts.Environment.ShootingRange;
using System;
using Assets.assets.Scripts.Environment.Background;
using Assets.assets.Scripts.Helpers;
using Assets.assets.Scripts.Archer;
using Assets.assets.Scripts.UI.GeneralUI;
using Assets.assets.Scripts.UI.ShootingUI;
using Assets.assets.Scripts.Menu;
using Assets.assets.Scripts.Inputs;
using Assets.assets.Scripts.FinishScreen;
using Assets.assets.Scripts.Leaderboard;

namespace Assets.assets.Scripts
{
    /**
     * <summary>Main class used for controlling the whole game</summary>
     **/ 
    public class GameController : MonoBehaviour
    {

        public ShootingRangeEmitter ShootingRangeController;

        public LineController LineController;

        public MovementInputController MovementInputController;

        public ShootingInputController ShootingInputController;

        public DistanceCalculator Distance;

        public TimeTextController TimeText;

        public ArcherAnimationController ArcherAnimator;

        public ShootingController ShootingController;

        public MenuController MenuController;

        public FinishScreenController FinishScreenController;

        public LeaderboardController LeaderboardController;

        private TreesEmitterController treesEmitter;

        private float time;

        private int shootingRoundsCounter;

        private GameStateEnum gameState;
        private GameStateEnum prePauseGameState;

        // key - which station, value - how many attempts
        private Dictionary<int, int> shootingRangeResults;

        void Start()
        {
            treesEmitter = new TreesEmitterController();
            ShootingController.PassedShooting += OnShootingPassed;
            LineController.LineTriggered += OnLineTriggered;
            time = 0f;
            shootingRoundsCounter = 0;
            shootingRangeResults = new Dictionary<int, int>(Constants.NumberOfRounds);


            gameState = GameStateEnum.PreStart;
            FinishScreenController.HideFinishScreen();
            LeaderboardController.CloseLeaderboard();

            MenuController.ShowStartMenu();
        }

        // Main game loop
        void Update()
        {
            // switch for various game actions
            switch (gameState)
            {
                case GameStateEnum.PreStart:
                    if (Input.GetKeyDown(KeyCode.Space) && !MenuController.IsInMenu)
                    {
                        gameState = GameStateEnum.Running;
                        StartRunning();
                    }
                    break;

                case GameStateEnum.Pause:
                    if (!MenuController.IsInMenu)
                    {
                        gameState = prePauseGameState;
                    }
                    break;

                case GameStateEnum.Shooting:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Shoot();
                    }
                    break;

                case GameStateEnum.End:
                    if (!FinishScreenController.IsScreenDisplayed)
                    {
                        FinishScreenController.ShowFinishScreen(shootingRangeResults, time);
                    }
                    break;
            }

            // if the game is running, track the time
            if (IsGameRunning())
            {
                time += Time.deltaTime;
                TimeText.Time = time;
            }

            //toggles menu if Escape button is pressed
            if (Input.GetKeyDown(KeyCode.Escape) && gameState != GameStateEnum.End)
            {
                ToggleMenu();
            }

            if (ShouldSpawnShootingRange())
            {
                SpawnShootingRange();
                shootingRoundsCounter++;
            }

            ChangeInput();
        }

        /**
         * <summary>Checks if the game has ended</summary>
         **/
        private void CheckForGameEnd()
        {
            if (shootingRoundsCounter >= Constants.NumberOfRounds)
            {
                gameState = GameStateEnum.End;
            }
        }

        /**
         * <summary>Starts shooting if the player is at the shooting line</summary>
         **/
        public void OnLineTriggered(object source, EventArgs args)
        {
            StartShooting();
        }

        /**
         * <summary>When the player passed the shooting, saves number of attempts and starts running if the game has not been finished yet</summary>
         **/
        public void OnShootingPassed(object source, ShootingEventArgs args)
        {
            if (!shootingRangeResults.ContainsKey(shootingRoundsCounter - 1))
            {
                shootingRangeResults.Add(shootingRoundsCounter - 1, args.NumberOfTries);
            }

            StartRunning();

            CheckForGameEnd();

        }

        /**
         * <summary>Spawns the shooting range</summary>
         **/
        private void SpawnShootingRange()
        {
            ShootingRangeController.SpawnShootingRange();
            treesEmitter.StopEmitting();
        }

        /**
         * <summary>Checks if the shooting range should be spawned</summary>
         **/
        private bool ShouldSpawnShootingRange()
        {
            var treshold = Distance.Distance - (shootingRoundsCounter * 100 + Constants.ShootingRangeEmitDistance);
            return Math.Abs(treshold) < 0.5;
        }

        /**
         * <summary>Changes the input source for running/shooting</summary>
         **/
        private void ChangeInput()
        {
            MovementInputController.GetInput = false;
            ShootingInputController.GetInput = false;

            if (gameState == GameStateEnum.Running)
            {
                MovementInputController.GetInput = true;
            }

            if (gameState == GameStateEnum.Shooting)
            {
                ShootingInputController.GetInput = true;
            }
        }

        /**
         * <summary>Determines whether the game is running or the player is in menu/finished the game</summary>
         * <returns>True if the player is in the game, false if in menu or finished the game</returns>
         **/
        private bool IsGameRunning()
        {
            return gameState == GameStateEnum.Running || gameState == GameStateEnum.Shooting;
        }

        /**
         * <summary>Starts the running part of the game</summary>
         **/
        private void StartRunning()
        {
            gameState = GameStateEnum.Running;
            treesEmitter.StartEmitting();
            ArcherAnimator.State = ArcherAnimationStateEnum.Running;
        }

        /**
         * <summary>Starts the shooting part of the game</summary>
         **/
        private void StartShooting()
        {
            gameState = GameStateEnum.Shooting;
            ArcherAnimator.State = ArcherAnimationStateEnum.Idle;
            ShootingController.StartShooting();
        }

        /**
         * <summary>Player shoots on the target</summary>
         **/
        private void Shoot()
        {
            ArcherAnimator.State = ArcherAnimationStateEnum.Shooting;

            ShootingController.Shoot();
        }

        /**
         * <summary>Toggles the game menu</summary>
         **/
        private void ToggleMenu()
        {
            if (MenuController.IsInMenu)
            {
                MenuController.HideMenu();
            }
            else
            {
                if (gameState == GameStateEnum.PreStart)
                {
                    MenuController.ShowStartMenu();
                }
                else
                {
                    prePauseGameState = gameState;
                    gameState = GameStateEnum.Pause;
                    MenuController.ShowMenu();
                }
            }
        }
    }
}