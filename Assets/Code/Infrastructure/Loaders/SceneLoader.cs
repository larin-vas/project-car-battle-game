using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loaders
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string _sceneName;

        private void Awake()
        {
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
        }
    }
}