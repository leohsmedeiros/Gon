using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Gon {
    public class PlayerInputs : MonoBehaviour  {
        public RxOnMouseOver btnMoveBack;
        public RxOnMouseOver btnMoveForward;
        public Button btnJump;
        public Button btnAttack;

        public Animator playerAnimator;
        public Rigidbody2D playerRigidbody;

        private void Start() {
            btnMoveBack.onMouseOver.Subscribe(_ => {
                playerAnimator.SetBool("move", true);
                LeanTween.scaleX(playerAnimator.gameObject, -1, Time.deltaTime);
                LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x - 0.5f, Time.deltaTime);
            });

            btnMoveForward.onMouseOver.Subscribe(_ => {
                playerAnimator.SetBool("move", true);
                LeanTween.scaleX(playerAnimator.gameObject, 1, Time.deltaTime);
                LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x + 0.5f, Time.deltaTime);
            });

            btnJump.OnPointerDownAsObservable().ThrottleFirst(new System.TimeSpan(0, 0, 2)).Subscribe(_ => {
                playerRigidbody.AddForce(Vector3.up * 2500);
            });

            btnAttack.OnPointerDownAsObservable().Subscribe(_ => {
                playerAnimator.SetBool("attack", true);
            });
        }
    }
}
