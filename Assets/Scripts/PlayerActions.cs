using UnityEngine;

namespace Gon
{
    [RequireComponent(typeof(PlayerInputs))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerActions : MonoBehaviour, Observer
    {
        private Rigidbody2D rigidbody2D;
        private Animator animator;

        public float speedMovement = 10;
        public float jumpForce = 20;


        void Start()
        {
            this.GetComponent<PlayerInputs>().AddObserver(this);
            this.rigidbody2D = this.GetComponent<Rigidbody2D>();
            this.animator = this.GetComponent<Animator>();
        }

        public void Notify(Object arg)
        {
            if (arg is PlayerInputs)
            {
                PlayerInputs.PlayerInputType inputType = ((PlayerInputs) arg).InputType;

                animator.SetBool("attack", false);
                switch (inputType)
                {
                    case PlayerInputs.PlayerInputType.Idle:
                        animator.SetBool("move", false);
                        break;

                    case PlayerInputs.PlayerInputType.MoveForward:
                        this.transform.localScale = new Vector3(1, 1, 1);
                        this.transform.position += (Vector3.right * Time.deltaTime * speedMovement);
                        animator.SetBool("move", true);
                        break;

                    case PlayerInputs.PlayerInputType.MoveBack:
                        this.transform.localScale = new Vector3(-1, 1, 1);
                        this.transform.position += (Vector3.left * Time.deltaTime * speedMovement);
                        animator.SetBool("move", true);
                        break;

                    case PlayerInputs.PlayerInputType.Jump:
                        Debug.Log("velocity: " + rigidbody2D.velocity);
                        if (rigidbody2D.velocity.y == 0)
                            rigidbody2D.AddForce(Vector3.up * jumpForce * 100);
                        break;

                    case PlayerInputs.PlayerInputType.Attack:
                        animator.SetBool("attack", true);
                        break;
                }

            }
        }

    }
}