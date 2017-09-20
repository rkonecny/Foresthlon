namespace Assets.assets.Scripts.Helpers
{
    /**
     * <summary>Simple helper class for getting time in the given format</summary>
     * <example>01:31:123</example>
    **/
    public static class TimeHelper
    {
        private static int GetMinutes(float time)
        {
            return (int)time / 60;
        }

        private static int GetSeconds(float time)
        {
            return (int)time % 60;
        }

        private static int GetMiliseconds(float time)
        {
            return (int)(time * 100) % 100;
        }

        /**
         * <summary>Represents the time in the game as a specified string</summary>
         * <param name="time">Time in seconds</param>
         * <returns>Time as a string</returns>
         * <example>01:33:012</example>
         **/ 
        public static string GetTimeText(float time)
        {
            return string.Format("{0:00}:{1:00}:{2:000}",
                GetMinutes(time), GetSeconds(time), GetMiliseconds(time));
        }
    }
}