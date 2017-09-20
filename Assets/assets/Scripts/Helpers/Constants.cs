namespace Assets.assets.Scripts.Helpers
{
    /**
     * <summary>Class for storing various constants of the game</summary>
    **/
    public static class Constants
    {
        #region Movement

        /**
        * <summary>Constant for calculating the distance - 
        * speed is in 0 - 1 range, so we need to multiply this by this coeficient</summary>
        **/
        public const float InputToSpeedCoeficient = 7f;

        /**
        * <summary>Distance after start/shooting which will spawn a new shooting line</summary>
        **/
        public const float ShootingRangeEmitDistance = 76.7f;

        /**
        * <summary>Minimum input value for movement</summary>
        **/
        public const float MinMovementInputValue = 0f;

        /**
        * <summary>Calculates speed from the input</summary>
        * <param name="inputSpeed">Input speed value</param>
        * <returns>Value used for controlling the speed of the emmitted particles</returns>
        **/
        public static float GetSpeedForInput(float inputSpeed)
        {
            return (inputSpeed / 100f);
        }

        /**
        * <summary>Calculates speed of archer's movement animation</summary>
        * <param name="inputSpeed">Input speed value</param>
        * <returns>Value used for setting the animation speed of archer's movement</returns>
        **/
        public static float GetAnimationSpeedForMovement(float inputSpeed)
        {
            return (inputSpeed + 50) / 100;
        }

        #endregion

        #region Shooting

        /**
        * <summary>Total number of shooting ranges required for finishing the game</summary>
        **/
        public const int NumberOfRounds = 4;

        /**
        * <summary>Represents the maximum speed of shooting slider's movement</summary>
        **/
        public const float MaxSliderSpeed = 10f;

        /**
        * <summary>Text displayed after successful shooting attempt</summary>
        **/
        public const string HitText = "HIT!";

        /**
        * <summary>Text displayed after failed shooting attempt</summary>
        **/
        public const string MissText = "MISS!";

        /**
        * <summary>Represents minimum value on the slider required to hit for successful attempt</summary>
        * <remarks>Slider is in [0,100] interval</remarks>
        **/
        public const float MinHitValue = 40f;

        /**
        * <summary>Represents maximum value on the slider required to hit for successful attempt</summary>
        * <remarks>Slider is in [0,100] interval</remarks>
        **/
        public const float MaxHitValue = 60f;

        /**
        * <summary>Represents delay between shot and next action (running/shooting again)</summary>
        **/
        public const float ShotWaitingTime = 2f;

        /**
        * <summary>Represents increment/decrement of the shooting slider in a unit of time</summary>
        * <param name="inputSpeed">Input speed - the higher it is, the smaller will the increment be</param>
        * <returns>Value used for incrementing/decrementing the shooting slider</returns>
        **/
        public static float GetSliderMovementSpeedIncrement(float inputSpeed)
        {
            return 10 + ((100 - inputSpeed) * 4.4f);
        }

        #endregion

        #region Leaderboard

        /**
        * <summary>Header of the leaderboard</summary>
        * <returns>Header of the leaderboard</returns>
        * <remarks>In case the NumberOfRounds is not 4, this section should be modified as well</remarks>
        **/
        public static string GetLeaderboardHeaderString()
        {
            return string.Format("{0,-2}  {1,-24} {2,-10} {3,-3} {4,-3} {5,-3} {6,-2} {7}",
                "#", "Name", "Time", "1", "2", "3", "4", "Total").ToString();
        }

        /**
        * <summary>String format for leaderboard entry</summary>
        * <return>String format for leaderboard entry</return>
        * <remarks>In case the NumberOfRounds is not 4, this section should be modified as well</remarks>
        **/
        public const string LeaderboardEntryFormatString = "{0,3}  {1,-24} {2,-10} {3,-3} {4,-3} {5,-3} {6,-3} {7}";

        #endregion
    }
}
