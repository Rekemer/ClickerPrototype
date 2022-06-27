using System;
using Data;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameEndWindow : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI _endText;
        [SerializeField] private GameObject messageWindow;
        [SerializeField] private Button messageWindowButton;
        private SceneLoader _loader;

        private void Awake()
        {
            _loader = FindObjectOfType<SceneLoader>();
        }

        private void Start()
        {
            
            if (_loader)
            {
                messageWindowButton.onClick.AddListener(_loader.ReloadScene);
            }
            gameObject.SetActive(false);
            EventSystem.current.OnEnemiesTooMany += GameOver;
            EventSystem.current.OnNoMoreEnemiesAreLeft += GameWin;
        }

      
        private void GameOver( )
        {
          
                // show lose message
                if (_endText)
                {
                    _endText.text = "You've Lost!";
                    gameObject.SetActive(true);
                }
                
            
        }

        private void GameWin()
        {
            if (_endText)
            {
                _endText.text = "You've Won!";
                if (messageWindow)
                {
                    messageWindow.SetActive(true);
                }
            }
        }

        private void OnDestroy()
        {
            EventSystem.current.OnEnemiesTooMany -= GameOver;
            EventSystem.current.OnNoMoreEnemiesAreLeft -= GameWin;
        }
    }
}