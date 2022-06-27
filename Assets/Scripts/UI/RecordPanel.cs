using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class RecordPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monsterCountText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private TextMeshProUGUI currentCountText;
    private int currentCount;
    private int currentRecord;
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
        EventSystem.current.OnEnemiesAmountChanged += UpdateMonsterCount;
        EventSystem.current.OnEnemiesDeath += UpdateCurrentCount;
    }


    public void UpdateCurrentCount(int count)
    {
        currentCountText.text = (Int16.Parse(currentCountText.text) +count).ToString();
       currentCount += count;
        if (currentCount > currentRecord)
        {
            UpdateRecord();
        }
    }

    private void UpdateRecord()
    {
        SaveData saveData = new SaveData();
        saveData.record = currentCount;
        currentRecord = currentCount;
        recordText.text = "Record: " + currentRecord.ToString();
        if (_dataManager)
        {
            _dataManager.Save(saveData);
        }
    }

    public void UpdateMonsterCount(int currentMonsterAmount)
    {
        monsterCountText.text = "Monsters " + currentMonsterAmount.ToString();
    }
   
  
    private void OnDisable()
    {
        EventSystem.current.OnEnemiesAmountChanged -= UpdateMonsterCount;
        EventSystem.current.OnEnemiesDeath -= UpdateCurrentCount;

    }
}
