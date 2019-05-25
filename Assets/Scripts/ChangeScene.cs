using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gon
{
    public class ChangeScene : MonoBehaviour
    {
        public void ChangeSceneTo (string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}