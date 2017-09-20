using Assets.assets.Scripts.Data.Entities;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.assets.Scripts.Data
{
    /**
    * <summary> Save single run data to an xml file</summary>
    **/
    public class AttemptLogWriter
    {
        private XmlSerializer serializer;

        private string dataFolder;

        public AttemptLogWriter()
        {
            serializer = new XmlSerializer(typeof(AttemptLog));
            dataFolder = "Logs";
            System.IO.Directory.CreateDirectory(dataFolder);
        }

        /**
         * <summary> Saving attempt xml to Logs folder </summary>
         * <param name="attemptLog">Log to save</param>
         **/
        public void SaveAttemptLogToFile(AttemptLog attemptLog)
        {
            var document = new XmlDocument();

            var xmlString = SerializeAttemptLog(attemptLog);

            document.LoadXml(xmlString);

            var filename = GetFileName(attemptLog);

            document.Save(filename);
        }

        /**
         * <summary> Generates unique filename, consists of time and player name </summary>
         * <param name="attemptLog">Log for which the filename is created</param>
         * <returns>Filename of the log</returns>
         * <example> 201704241538 - Test2.xml </example>
         **/
        private string GetFileName(AttemptLog attemptLog)
        {
            string filename = string.Format("{0} - {1}.xml", DateTime.Now.ToString("yyyMMddHHmm"), attemptLog.AttemptSummary.Name);

            return Path.Combine(dataFolder, filename);
        }

        /**
         * <summary> Serialize attemptlog to xml string </summary>
         * <param name="attemptLog">Log to serialize</param>
         * <returns>Serialized log</returns>
         **/
        private string SerializeAttemptLog(AttemptLog attemptLog)
        {
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, attemptLog);
                return writer.ToString();
            }
        }
    }
}
