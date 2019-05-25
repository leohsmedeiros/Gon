using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Gon
{
    public class GameoverPanelController : MonoBehaviour
    {
        public GameBoard gameBoard;
        public Image background;
        public GameObject board;
        public Text tvScore; 

        //public ImageFillAmountTo ivBackground;
        //public ChangeEnableTo board;
        //public Text tvKillAmount;

        void Start ()
        {
            gameBoard.score.SubscribeToText(tvScore, x => x.ToString());
        }

        void OnEnable ()
        {
            //board.ChangeEnable(false);

            Debug.Log("OnEnable");

            LeanTween.alpha(background.GetComponent<RectTransform>(), 0.8f, 1)
            //LeanTween.alpha(background.gameObject, 0.8f, 1)
                .setFrom(0.0f)
                .setOnComplete((obj) =>
                {
                    board.SetActive(true);
                });
                
            //ivBackground.DeleteObservers();
            //ivBackground.AddObserver(this);
            //ivBackground.StartUpdating();
        }
    }
}