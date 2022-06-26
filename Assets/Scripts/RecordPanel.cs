using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class RecordPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monsterCount;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI currentCount;

    private void Awake()
    {
        
    } 
    void Start()
    {
        EventSystem.current.OnEnemiesSpawn += UpdateMonsterCount;
    }


    public void UpdateCurrentCount()
    {
        
    }

    public void UpdateRecord()
    {
        
    }

    public void UpdateMonsterCount(int currentMonsterAmount)
    {
        monsterCount.text = "Monsters " + currentMonsterAmount.ToString();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        EventSystem.current.OnEnemiesSpawn -= UpdateMonsterCount;

    }
}
