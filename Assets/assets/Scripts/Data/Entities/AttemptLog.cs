using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.assets.Scripts.Data.Entities
{
    /**
     * <summary>Xml log for a single run</summary>
     **/
    [Serializable]
    public class AttemptLog
    {
        public Attempt AttemptSummary { get; set; }

        [XmlArray("InputData")]
        public List<LogEntry> InputEntries { get; private set; }

        public AttemptLog()
        {
            InputEntries = new List<LogEntry>();
        }

        /**
         * <summary>Collection of all attempts, used for leaderboard</summary>
         * <param name="inputType">Either attention or meditation</param>
         * <param name="value">Value of the input</param>
         * <param name="time">Time in the game</param>
         **/
        public void AddLogEntry(string inputType, int value, float time)
        {
            InputEntries.Add(new LogEntry { Type = inputType, Value = value, Time = time });
        }

    }
}
