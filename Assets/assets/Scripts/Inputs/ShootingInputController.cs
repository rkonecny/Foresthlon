using UnityEngine;

namespace Assets.assets.Scripts.Inputs
{
    /**
     * <summary>Class for getting the shooting slider speed</summary>
     **/
    public class ShootingInputController : MonoBehaviour
    {
        public bool GetInput = false;

        public float InputSpeed { get { return input; } }

        public TGCConnectionController Controller;

        public AttemptLoggerController Logger;

        private float input = 50f;

        // Use this for initialization
        void Start()
        {
            Controller.UpdateMeditationEvent += OnUpdateMeditation;
        }

        public void OnUpdateMeditation(int value)
        {
            if (GetInput)
            {
                input = value;

                Logger.AddLogInputEntry("Meditation", value);
            }
            else
            {
                input = 50f;
            }
        }
    }
}

