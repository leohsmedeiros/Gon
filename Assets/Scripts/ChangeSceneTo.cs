using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Gon
{
    public class ChangeSceneTo : MonoBehaviour
    {
        public SceneAsset scene;

        public void ChangeScene ()
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}