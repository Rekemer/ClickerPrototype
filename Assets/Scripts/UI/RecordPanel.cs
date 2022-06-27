using System;
using Core;
using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RecordPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI monsterCountText;
        [SerializeField] private TextMeshProUGUI recordText;
        [SerializeField] private TextMeshProUGUI currentCountText;
        private int _currentCount;
        private int _currentRecord;
        private DataManager _dataManager;

        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
            if (_dataManager)
            {
                _dataManager.Load();
            }
        }

        void Start()
        {
            if (_dataManager != null)
            {
                var save = _dataManager.SaveData;
                if (save != null)
                {
                    recordText.text = "Record " + save.record.ToString();
                }
            }

            EventSystem.Current.OnEnemiesAmountChanged += UpdateMonsterCount;
            EventSystem.Current.OnEnemiesDeath += UpdateCurrentCount;
        }


        private void UpdateCurrentCount(int count)
        {
            currentCountText.text = (Int16.Parse(currentCountText.text) + count).ToString();
            _currentCount += count;
            if (_currentCount > _currentRecord)
            {
                UpdateRecord();
            }
        }

        private void UpdateRecord()
        {
            var saveData = new SaveData();
            saveData.record = _currentCount;
            _currentRecord = _currentCount;
            recordText.text = "Record: " + _currentRecord.ToString();
            if (_dataManager)
            {
                _dataManager.Save(saveData);
            }
        }

        private void UpdateMonsterCount(int currentMonsterAmount)
        {
            monsterCountText.text = "Monsters " + currentMonsterAmount.ToString();
        }


        private void OnDisable()
        {
            EventSystem.Current.OnEnemiesAmountChanged -= UpdateMonsterCount;
            EventSystem.Current.OnEnemiesDeath -= UpdateCurrentCount;
        }
    }
}