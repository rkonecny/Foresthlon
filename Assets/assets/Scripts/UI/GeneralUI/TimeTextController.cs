using Assets.assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.assets.Scripts.UI.GeneralUI
{
    /**
    * <summary>Class responsible for displaying time on the screen</summary>
    **/
    public class TimeTextController : MonoBehaviour
    {
        public Text TimerLabel;

        public float Time { get; set; }

        void Update()
        {
            TimerLabel.text = TimeHelper.GetTimeText(Time);
        }
    }
}
