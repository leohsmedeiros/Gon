using UnityEngine;
using UnityEngine.UI;

namespace Gon
{
    public class GameoverPanelController : MonoBehaviour, Observer
    {
        public ImageFillAmountTo ivBackground;
        public ChangeEnableTo board;
        public Text tvKillAmount;

        void OnEnable ()
        {
            board.ChangeEnable(false);

            ivBackground.DeleteObservers();
            ivBackground.AddObserver(this);
            ivBackground.StartUpdating();
        }

        public void Notify(Object arg)
        {
            board.ChangeEnable(true);
            tvKillAmount.text = "1";
        }
    }
}