using Assets.assets.Scripts.Data.Entities;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.assets.Scripts.Data
{
    /**
     * <summary>Saving/loading leaderboard from the disc</summary>
     **/
    public class AttemptCollectionDataAccess
    {
        //persistentDataPath - Appdata/LocalLow/Roman/Foresthlon
        private string filename = Path.Combine(Application.persistentDataPath, "Leaderboard.xml");

        private XmlDocument document;

        public AttemptCollectionDataAccess()
        {
            document = new XmlDocument();
        }

        /**
         * <summary>Saving leaderboard to the disc </summary>
         * <param name="xmlCollection">Collection of attempts represented by xml string</param>
         **/
        public void SaveXmlCollection(string xmlCollection)
        {
            document.LoadXml(xmlCollection);

            document.Save(filename);
        }

        /**
         * <summary>Loading leaderboard from the disc </summary>
         * <returns>Collection represented as xml string</returns>
         **/
        public string LoadXmlCollection()
        {
            try
            {
                document.Load(filename);
            }
            catch (FileNotFoundException)
            {                
                HandleException();
            }

            return document.InnerXml;
        }

        /**
         * <summary>No file was created - create an empty one </summary>
         **/
        private void HandleException()
        {
            var serializer = new AttemptCollectionSerializer();
            SaveXmlCollection(serializer.SerializeAttempts(new AttemptCollection()));
        }
    }
}
