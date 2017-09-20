using System;

namespace Assets.assets.Scripts.Helpers
{
    /**
     * <summary>Arguments of the finished shooting range event</summary>
    **/
    public class ShootingEventArgs : EventArgs
    {
        /**
         * <summary>Number of attempts needed to pass the shooting</summary>
        **/
        public int NumberOfTries { get; set; }
    }
}
