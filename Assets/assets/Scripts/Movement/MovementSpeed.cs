using UnityEngine;

namespace Assets.assets.Scripts.Movement
{
    /**
    * <summary>Class which represents the movement speed of the archer</summary>
    **/
    public class MovementSpeed : MonoBehaviour
    {
        private float _speed;

        /**
        * <summary>Movement speed of the archer</summary>
        **/
        public float Speed
        {
            get { return _speed; }
            set { _speed = Helpers.Constants.GetSpeedForInput(value); } 
        }
    }
}