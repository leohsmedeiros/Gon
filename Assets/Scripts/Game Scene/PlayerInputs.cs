using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Gon {
    public class PlayerInputs : MonoBehaviour  {
        //public RxOnMouseOver btnMoveBack;
        //public RxOnMouseOver btnMoveForward;

        public FixedJoystick joystick;
        public Button btnJump;
        public Button btnAttack;

        public Animator playerAnimator;
        public Rigidbody2D playerRigidbody;

        private void Start() {
            this.gameObject.UpdateAsObservable().Subscribe(_ => {
                Debug.Log("joystick.Horizontal: " + joystick.Horizontal);

                if (joystick.Horizontal < 0) {
                    playerAnimator.SetBool("move", true);
                    LeanTween.scaleX(playerAnimator.gameObject, -1, Time.deltaTime);
                } else if (joystick.Horizontal > 0) {
                    playerAnimator.SetBool("move", true);
                    LeanTween.scaleX(playerAnimator.gameObject, 1, Time.deltaTime);
                }else {
                    playerAnimator.SetBool("move", false);
                }

                LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x + joystick.Horizontal, Time.deltaTime);

                //if (joystick.Horizontal < 0) {
                //    playerAnimator.SetBool("move", true);
                //    LeanTween.scaleX(playerAnimator.gameObject, -1, Time.deltaTime);
                //    LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x + -1, Time.deltaTime);
                //} else if (joystick.Horizontal > 0) {
                //    playerAnimator.SetBool("move", true);
                //    LeanTween.scaleX(playerAnimator.gameObject, 1, Time.deltaTime);
                //    LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x + 1, Time.deltaTime);
                //} else {
                //    playerAnimator.SetBool("move", false);
                //}


                //if (joystick.Horizontal < 0) move = -1;
                //else if (joystick.Horizontal > 0) move = 1;
                //else move = 0;

                //LeanTween.scaleX(playerAnimator.gameObject, move, Time.deltaTime);
                //LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x + move, Time.deltaTime);
            });

            //btnMoveBack.onMouseOver.Subscribe(_ => {
            //    playerAnimator.SetBool("move", true);
            //    LeanTween.scaleX(playerAnimator.gameObject, -1, Time.deltaTime);
            //    LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x - 0.5f, Time.deltaTime);
            //});

            //btnMoveForward.onMouseOver.Subscribe(_ => {
            //    playerAnimator.SetBool("move", true);
            //    LeanTween.scaleX(playerAnimator.gameObject, 1, Time.deltaTime);
            //    LeanTween.moveX(playerAnimator.gameObject, playerAnimator.transform.position.x + 0.5f, Time.deltaTime);
            //});

            btnJump.OnPointerDownAsObservable().ThrottleFirst(new System.TimeSpan(0, 0, 2)).Subscribe(_ => {
                playerRigidbody.AddForce(Vector3.up * 2500);
            });

            btnAttack.OnPointerDownAsObservable().Subscribe(_ => {
                playerAnimator.SetBool("attack", true);
            });
        }
    }
}
