using Assets.assets.Scripts.Movement;
using UnityEngine;

namespace Assets.assets.Scripts.Inputs
{
    /**
     * <summary>Class for getting the movement speed</summary>
     **/
    public class MovementInputController : MonoBehaviour
    {

        public MovementSpeed Speed;

        public bool GetInput = false;

        //controller of the Neurosky headset
        public TGCConnectionController Controller;

        public AttemptLoggerController Logger;

        public float InputSpeed { get { return input; } }

        private float input = 50f;

        // Use this for initialization
        void Start()
        {
            Controller.UpdateAttentionEvent += OnUpdateAttention;
        }

        // Update is called once per frame
        void Update()
        {
            Speed.Speed = GetInput ? input : 0;
        }

        public void OnUpdateAttention(int value)
        {
            if (GetInput)
            {
                input = value;

                Logger.AddLogInputEntry("Attention", value);
            }
        }
    }
}
