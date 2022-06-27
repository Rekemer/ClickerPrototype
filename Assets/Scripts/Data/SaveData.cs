using System.IO;
using UnityEngine;

namespace Data
{
    public class SaveData
    {
        public int record;
    }

    public class JsonSaver : ISave
    {
        private static readonly string _fileName = Application.persistentDataPath + "/saveData1.sav";

        public SaveData Load()
        {
            SaveData dataToLoad = new SaveData();
            if (File.Exists(_fileName))
            {
                using (StreamReader reader = new StreamReader(_fileName))
                {
                    var json = reader.ReadToEnd();
                    JsonUtility.FromJsonOverwrite(json, dataToLoad);
                    return dataToLoad;
                }
            }

            return null;
        }

        public void Save(SaveData saveData)
        {
            var json = JsonUtility.ToJson(saveData);

            var fileStream = new FileStream(_fileName, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }
    }
}