using Assets.assets.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.assets.Scripts.Data.Entities
{
    /**
     * <summary>Collection of all attempts, used for leaderboard</summary>
     **/
    [Serializable]
    public class AttemptCollection
    {
        public List<Attempt> AttemptsList { get; set; }

        /**
         * <summary>Returns the whole leaderboard as a single string</summary>
         **/
        public override string ToString()
        {
            var builder = new StringBuilder();

            int position = 1;

            foreach (var attempt in AttemptsList)
            {
                builder.AppendLine(BuildAttemptLine(attempt, position));
                position++;
            }

            return builder.ToString();
        }

        /**
         * <summary>Creates single entry</summary>
         * <param name="attempt">Attempt to be represented as a string</param> 
         * <param name="position">Position on the leadeboard for a given attempt</param>
         * <returns>String representation of a single leaderboard entry</returns>
         **/
        private string BuildAttemptLine(Attempt attempt, int position)
        {
            return string.Format(Constants.LeaderboardEntryFormatString, position,
                attempt.Name, TimeHelper.GetTimeText(attempt.Time), attempt.Board1, attempt.Board2,
                attempt.Board3, attempt.Board4, attempt.GetTotalBoardShootingAttempts());
        }
    }
}
