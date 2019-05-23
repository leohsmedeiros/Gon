using UnityEngine;

namespace Gon
{
    public class MainMenuSceneController : MonoBehaviour
    {
        public GameObject panel;
        public GameObject starsOnPanel;
        public GameObject lightsParticles;
        public GameObject ivMenuArrow;
        public RectTransform rightMenu;

        private readonly float[] openClosePositions = { 100f, -100f };
        private bool isMenuClosed = true;

        void Start ()
        {
            LeanTween.move(panel, Vector3.zero, 1.5f)
                .setOnComplete((action) =>
                {
                    LeanTween.scale(starsOnPanel, Vector3.one * 0.7f, 1)
                        .setOnComplete((action2) =>
                        {
                            lightsParticles.SetActive(true);
                        });
                });
        }

        public void onClickOpenCloseMenuBtn()
        {
            float origin = isMenuClosed ? openClosePositions[0] : openClosePositions[1];
            float destiny = isMenuClosed ? openClosePositions[1] : openClosePositions[0];

            LeanTween.moveX(rightMenu, destiny, 0.5f)
                .setFrom(origin)
                .setOnComplete(action => {
                    isMenuClosed = !isMenuClosed;
                    LeanTween.scaleX(ivMenuArrow, isMenuClosed ? 1 : -1, 0);
                });
        }
    }
}
