using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data
{
    public class SceneLoader : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void LoadNextScene()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            var nextSceneIndex = ++currentSceneIndex;
            var totalSceneCount = SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex % totalSceneCount);
        }

        public void ReloadScene()
        {
            int currIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currIndex);
        }
    }
}