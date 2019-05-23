using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gon
{
    public class ChangeSceneTo : MonoBehaviour
    {

        //public SceneAsset scene;
        public string sceneName;

        public void ChangeScene ()
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}