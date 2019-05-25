using UnityEngine;

namespace Gon
{
    public class MainMenuSceneController : MonoBehaviour
    {
        public GameObject panel;
        public GameObject starsOnPanel;
        public GameObject lightsParticles;

        void Start () {
            LeanTween.move(panel, Vector3.zero, 1.5f)
                .setOnComplete((action) => {
                    LeanTween.scale(starsOnPanel, Vector3.one * 0.7f, 1)
                        .setOnComplete((action2) => { lightsParticles.SetActive(true); }); });
        }
    }
}
