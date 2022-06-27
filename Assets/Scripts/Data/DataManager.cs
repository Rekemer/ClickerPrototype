using UnityEngine;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        private ISave _saver;
        private SaveData _saveData;

        public SaveData SaveData => _saveData;

        private void Awake()
        {
            _saver = new JsonSaver();
            _saveData = new SaveData();
            DontDestroyOnLoad(this.gameObject);
        }

        public void Load()
        {
            _saveData = _saver.Load();
        }

        public void Save(SaveData saveData)
        {
            _saver.Save(saveData);
        }
    }
}