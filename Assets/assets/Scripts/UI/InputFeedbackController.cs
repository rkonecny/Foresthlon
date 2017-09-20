using Assets.assets.Scripts.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.assets.Scripts.UI
{
    /**
     * <summary>Class for display feedback for the player of the input</summary>
     **/ 
    public class InputFeedbackController : MonoBehaviour
    {
        public Slider InputValueSlider;

        public Text InputType;

        public MovementInputController MovementInput;

        public ShootingInputController ShootingInput;

        void Update()
        {
            SetFeedback();
        }

        /**
         * <summary>Sets the feedback UI for either attention or meditation level of the player</summary>
         **/
        public void SetFeedback()
        {
            if (MovementInput.GetInput)
            {
                InputType.text = "Attention";
                InputValueSlider.value = MovementInput.InputSpeed;
            }
            else if (ShootingInput.GetInput)
            {
                InputType.text = "Meditation";
                InputValueSlider.value = ShootingInput.InputSpeed;
            }
            else
            {
                InputType.text = string.Empty;
                InputValueSlider.enabled = false;
            }
        }
    }

}