using UnityEngine;
using UnityEngine.UI;

namespace Gon
{
    [RequireComponent(typeof(Image))]
    public class ImageFillAmountTo : LimitedUpdateMonoBehaviour
    {
        private Image image;
        public float speed;

        void Start()
        {
            image = this.GetComponent<Image>();
        }

        public override void StartUpdating()
        {
            base.StartUpdating();
            image.fillAmount = 0;
        }

        public override void LimitedUpdate()
        {

            image.fillAmount += Mathf.Lerp(0, 1, Time.deltaTime * speed);

            if (image.fillAmount == 1)
            {
                NotifyObservers(this);
                StopUpdating();
            }

        }

    }
}