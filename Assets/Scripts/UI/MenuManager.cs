using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _recordMenu;
    [SerializeField] private GameObject _credits;
    [SerializeField] private TextMeshProUGUI _recordText;
     private DataManager _dataManager;

    private void Awake()
    {
        OpenMainMenu();
        _dataManager = FindObjectOfType<DataManager>();
    }

    public void OpenRecordMenu()
    {
        _mainMenu.SetActive(false);
        _credits.SetActive(false);
        _recordMenu.SetActive(true);
    }
    public void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
        _recordMenu.SetActive(false);
        _credits.SetActive(false);
    }

    public void OpenCredits()
    {
        _credits.SetActive(true);
        _recordMenu.SetActive(false);
        _mainMenu.SetActive(false);
    }
    void Start()
    {
        if (_dataManager)
        {
            _dataManager.Load();

            _recordText.text = "Your Record: " + _dataManager.SaveData.record.ToString();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
