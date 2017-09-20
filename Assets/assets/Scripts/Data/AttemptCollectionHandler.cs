using Assets.assets.Scripts.Data.Entities;

namespace Assets.assets.Scripts.Data
{
    /**
    * <summary>Class that handles saving and loading the leaderboard</summary>
    **/
    public class AttemptCollectionHandler
    {
        private AttemptCollectionDataAccess dataAccess;
        private AttemptCollectionSerializer serializer;

        private AttemptCollection collection;

        public AttemptCollectionHandler()
        {
            dataAccess = new AttemptCollectionDataAccess();
            serializer = new AttemptCollectionSerializer();
            collection = new AttemptCollection();
        }

        /**
         * <summary>Save single attempt to the leaderboard</summary>
         * <param name="attempt">Attempt to be saved</param>
         **/
        public void SaveAttempt(Attempt attempt)
        {
            collection = LoadAttemptCollection();
            collection.AttemptsList.Add(attempt);
            collection.AttemptsList.Sort();

            var xmlString = serializer.SerializeAttempts(collection);
            dataAccess.SaveXmlCollection(xmlString);
        }

        /**
         * <summary>Load the leaderboard</summary>
         * <returns>Attempt collection used by the leaderboard</returns>
         **/
        public AttemptCollection LoadAttemptCollection()
        {
            var xmlString = dataAccess.LoadXmlCollection();

            collection = serializer.DeserializeAttempts(xmlString);
            return collection;
        }

        /**
         * <summary>Get position in the leaderboard according to the final time of the run</summary>
         * <param name="time">Time of the run</param>
         * <returns>Position in the leaderboard</returns>
         **/
        public int GetPositionByTime(float time)
        {
            LoadAttemptCollection();
            int position = 1;

            foreach (var attempt in collection.AttemptsList)
            {
                if (time < attempt.Time)
                {
                    return position;
                }

                position++;
            }

            return position;
        }
    }
}
