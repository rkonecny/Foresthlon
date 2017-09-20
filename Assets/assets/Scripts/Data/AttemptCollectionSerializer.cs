using Assets.assets.Scripts.Data.Entities;
using System.IO;
using System.Xml.Serialization;

namespace Assets.assets.Scripts.Data
{
    /**
    * <summary>Serializer of AttemptCollection</summary>
    **/
    public class AttemptCollectionSerializer
    {
        private XmlSerializer serializer;

        public AttemptCollectionSerializer()
        {
            serializer = new XmlSerializer(typeof(AttemptCollection));
        }

        /**
         * <summary>Serialize collection to xml string</summary>
         **/
        public string SerializeAttempts(AttemptCollection attempts)
        {
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, attempts);
                return writer.ToString();
            }
        }

        /**
         * <summary>Deserialize collection from xml string</summary>
         **/
        public AttemptCollection DeserializeAttempts(string attempts)
        {
            using (var reader = new StringReader(attempts))
            {
                var data = serializer.Deserialize(reader);
                return (AttemptCollection)data;
            }
        }
    }
}
