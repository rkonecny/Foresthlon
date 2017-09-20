using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.assets.Scripts.Helpers;
using System;
using Assets.assets.Scripts.Inputs;

namespace Assets.assets.Scripts.UI.ShootingUI
{
    /**
     * <summary>Class for controlling the shooting part of the game</summary>
     **/ 
    public class ShootingController : MonoBehaviour
    {
        public Slider Slider;

        public Text FeedbackText;

        public ShootingInputController ShootingInput;

        public event EventHandler<ShootingEventArgs> PassedShooting;

        // sign for the slider - if it should increment or decrement
        private int sign = 1;

        private bool isSliderRunning;

        private int attemptCounter;

        void Start()
        {
            StopShooting();
        }

        void Update()
        {
            // if the slider is active, update slider circle value
            if (isSliderRunning && ShootingInput.GetInput)
            {
                Slider.value += Time.deltaTime * SliderMovementIncrement();
            }
        }

        /**
         * <summary>Starts shooting interaction</summary>
         **/ 
        public void StartShooting()
        {
            Slider.value = Slider.maxValue / 2f;
            SetSliderVisibility(true);

            isSliderRunning = true;

            FeedbackText.enabled = true;
            FeedbackText.text = string.Empty;
        }

        /**
         * <summary>Stops shooting interaction</summary>
         **/
        public void StopShooting()
        {
            SetSliderVisibility(false);
            FeedbackText.enabled = false;
        }

        /**
         * <summary>The player shoots on the target</summary>
         **/
        public void Shoot()
        {
            isSliderRunning = false;

            var isHit = IsHit();

            FeedbackText.text = isHit ? Constants.HitText : Constants.MissText;

            StartCoroutine(WaitTillNextAction(isHit)); 

        }

        /**
         * <summary>Raised when the player hits the target</summary>
         **/
        protected virtual void OnShootingPassed()
        {
            if (PassedShooting != null)
            {
                PassedShooting(this, new ShootingEventArgs { NumberOfTries = attemptCounter });
            }
        }

        /**
         * <summary>Determines if the shot hits the target</summary>
         * <returns>If the shot was sucessful returns true, if misses return false</returns>
         **/
        private bool IsHit()
        {
            return Slider.value >= Constants.MinHitValue && Slider.value <= Constants.MaxHitValue;
        }

        /**
         * <summary>
         *  After the shot, wait for predefined amount of time before any other action should occure. 
         *  If the attempt was successful, stop shooting and move to next action - either running or end of the game
         *  Repeat the shooting if the hit misses the target
         * </summary>
         * <param name="isHit">Determines if the shot was sucessful</param>
         **/
        private IEnumerator WaitTillNextAction(bool isHit)
        {
            yield return new WaitForSeconds(Constants.ShotWaitingTime);

            attemptCounter++;

            if (isHit)
            {
                StopShooting();
                OnShootingPassed();
                attemptCounter = 0;
            }
            else
            {
                StartShooting();
            }

        }

        /**
         * <summary>Method for moving the slider circle</summary>
         * <returns>Increment or decrement of slider value</returns>
         **/
        private float SliderMovementIncrement()
        {
            if (Slider.value >= Slider.maxValue || Slider.value <= Slider.minValue)
            {
                sign *= -1;
            }

            return Constants.GetSliderMovementSpeedIncrement(ShootingInput.InputSpeed) * sign;
        }

        /**
         * <summary>Sets the visibility of the shooting UI</summary>
         * <param name="shouldBeVisible">If true, display the UI, hide otherwise</param>
         **/
        private void SetSliderVisibility(bool shouldBeVisible)
        {
            var alpha = shouldBeVisible ? 1 : 0;

            var sliderComponents = Slider.GetComponentsInChildren<CanvasRenderer>();

            foreach (var component in sliderComponents)
            {
                component.SetAlpha(alpha);
            }
        }
    }
}