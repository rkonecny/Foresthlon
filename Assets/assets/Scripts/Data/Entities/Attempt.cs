using System;
using System.Xml.Serialization;

namespace Assets.assets.Scripts.Data.Entities
{
    /**
     * <summary>Class for storing info about a single run</summary>
     **/
    [Serializable]
    public class Attempt : IComparable
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public float Time { get; set; }

        //scores of each shootings
        [XmlAttribute]
        public int Board1 { get; set; }

        [XmlAttribute]
        public int Board2 { get; set; }

        [XmlAttribute]
        public int Board3 { get; set; }

        [XmlAttribute]
        public int Board4 { get; set; }

        public int CompareTo(object obj)
        {
            var attempt = obj as Attempt;

            return Time.CompareTo(attempt.Time);
        }

        /**
         * <summary>Sum of all shooting attempts</summary>
         * <returns>Score of the shooting - how many attempts the player needed to pass</returns>
         **/ 
        public int GetTotalBoardShootingAttempts()
        {
            return Board1 + Board2 + Board3 + Board4;
        }
    }
}
