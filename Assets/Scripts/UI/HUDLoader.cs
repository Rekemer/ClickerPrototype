using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class HUDLoader : MonoBehaviour
    {
        private void Awake()
        {
            if (SceneManager.GetSceneByName("HUD").isLoaded == false)
            {
                SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
            }
        }
    }
}