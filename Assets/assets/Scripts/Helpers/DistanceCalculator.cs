using UnityEngine;

namespace Assets.assets.Scripts.Helpers
{
    /**
     * <summary>Class for calculating distance travelled from the movement speed</summary>
     **/ 
    public class DistanceCalculator : MonoBehaviour {

        public Movement.MovementSpeed Speed;

        public float Distance { get; private set; }

        void Update()
        {
            Distance += Time.deltaTime * Speed.Speed * Constants.InputToSpeedCoeficient;
        }
    }
}
