using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gon
{
    public class MainMenuEventButtons : MonoBehaviour
    {
        public RectTransform rightMenu;
        public GameObject ivMenuArrow;
        public GameObject creditsWindow;

        private readonly float[] openClosePositions = { 100f, -100f };
        private bool isMenuClosed = true;

        public void OnClickOpenCloseMenuBtn()
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

        public void OnClickToShowHideCredits ()
        {
            creditsWindow.SetActive(!creditsWindow.activeSelf);
        }

        public void OnClickToChangeScene (string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

    }
}