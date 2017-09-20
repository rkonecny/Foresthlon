using Assets.assets.Scripts.Helpers;
using Assets.assets.Scripts.Inputs;
using UnityEngine;

namespace Assets.assets.Scripts.Archer
{
    /**
     * <summary>Class responsible for changing animation and speed of animation of running</summary> 
     **/
    public class ArcherAnimationController : MonoBehaviour
    {
        public Animator ArcherAnimator;

        public ArcherAnimationStateEnum State;

        public MovementInputController Input;

        private float _runningSpeed;

        /**
         * <summary>Speed of running</summary> 
         **/
        public float RunningSpeed
        {
            set { _runningSpeed = value <= Constants.MinMovementInputValue ? 0 : Constants.GetAnimationSpeedForMovement(value); }
            private get { return _runningSpeed; }
        }

        private ArcherAnimationStateEnum currentState;
        
        void Start()
        {
            ArcherAnimator.Play("Archer_Idle_Start");
        }

        void Update()
        {
            RunningSpeed = Input.InputSpeed;
            ChangeAnimation();
        }

        /**
         * <summary>Change animation according to the state</summary> 
         **/
        private void ChangeAnimation()
        {
            ChangeSpeedOfAnimation();

            if (currentState != State)
            {
                string stateString = "Archer_";

                switch (State)
                {
                    case ArcherAnimationStateEnum.Idle:
                        stateString += "Idle";
                        currentState = ArcherAnimationStateEnum.Idle;
                        break;
                    case ArcherAnimationStateEnum.RunningIdle:
                        stateString += "Idle";
                        currentState = ArcherAnimationStateEnum.RunningIdle;
                        break;
                    case ArcherAnimationStateEnum.Running:
                        stateString += "Walk";
                        currentState = ArcherAnimationStateEnum.Running;
                        break;
                    case ArcherAnimationStateEnum.Shooting:
                        stateString += "Attack";
                        currentState = ArcherAnimationStateEnum.Idle;
                        State = ArcherAnimationStateEnum.Idle;
                        break;
                }

                ArcherAnimator.Play(stateString);
            } 
        }

        /**
         * <summary>Change animation speed for running animation</summary> 
         **/
        private void ChangeSpeedOfAnimation()
        {
            if (State == ArcherAnimationStateEnum.Running || State == ArcherAnimationStateEnum.RunningIdle)
            {
                if (RunningSpeed != 0)
                {
                    State = ArcherAnimationStateEnum.Running;
                    ArcherAnimator.speed = RunningSpeed;
                    return;
                }

                State = ArcherAnimationStateEnum.RunningIdle;
            }

            ArcherAnimator.speed = 1f;
        }
    }
}