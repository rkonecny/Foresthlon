using Assets.assets.Scripts.Data;
using Assets.assets.Scripts.Data.Entities;
using Assets.assets.Scripts.UI.GeneralUI;
using UnityEngine;

namespace Assets.assets.Scripts
{
    /**
     * <summary>Class responsible for handling the player's data log</summary>
     **/ 
    public class AttemptLoggerController : MonoBehaviour
    {
        public TimeTextController Time;

        private AttemptLog attemptLog;

        private AttemptLogWriter logWriter;

        void Start()
        {
            attemptLog = new AttemptLog();
            logWriter = new AttemptLogWriter();
        }

        /**
         * <summary>Adds a single entry to the log</summary>
         * <param name="inputType">Either attention or meditation</param>
         * <param name="value">Input value</param>
         **/
        public void AddLogInputEntry(string inputType, int value)
        {
            attemptLog.AddLogEntry(inputType, value, Time.Time);
        }

        /**
         * <summary>Adds attempt header - players name, number of attempts and the final time</summary>
         * <param name="attempt">Attempt header to be added to the log</param>
         **/
        public void AddAttemptHeader(Attempt attempt)
        {
            attemptLog.AttemptSummary = attempt;
        }

        /**
         * <summary>Saves the log file to disk</summary>
         **/
        public void SaveAttemptLog()
        {
            logWriter.SaveAttemptLogToFile(attemptLog);
        }
        
    }
}