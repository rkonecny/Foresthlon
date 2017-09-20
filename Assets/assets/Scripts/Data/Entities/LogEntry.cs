using System;
using System.Xml.Serialization;

namespace Assets.assets.Scripts.Data.Entities
{
    /**
     * <summary>Single input log information</summary> 
     **/
    [Serializable]
    public class LogEntry
    {
        [XmlAttribute]
        public float Time { get; set; }

        //either attention or meditation
        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public int Value { get; set; }
    }
}
