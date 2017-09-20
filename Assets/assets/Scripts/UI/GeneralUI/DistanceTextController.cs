using UnityEngine;
using UnityEngine.UI;

namespace Assets.assets.Scripts.UI.GeneralUI
{
    /**
    * <summary>Class responsible for displaying achieved distance on the screen</summary>
    **/
    public class DistanceTextController : MonoBehaviour
    {
        public Text DistanceLabel;

        public Helpers.DistanceCalculator Distance;

        void Update()
        {
            DistanceLabel.text = string.Format("{0:000} m", Distance.Distance);
        }
    }
}
