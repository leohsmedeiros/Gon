using UnityEngine;

namespace Gon
{
    public class PlayerInputs : LimitedUpdateMonoBehaviour
    {
        public enum PlayerInputType { Idle, MoveForward, MoveBack, Jump, Attack }
        public PlayerInputType InputType { private set; get; }

        public override void LimitedUpdate()
        {
            NotifyObservers(this);
        }

        public void OnStop ()
        {
            Debug.Log("OnStop");
            InputType = PlayerInputType.Idle;
            StopUpdating();
            NotifyObservers(this);
        }

        public void OnMoveForwardPressed ()
        {
            Debug.Log("OnMoveForwardPressed");
            InputType = PlayerInputType.MoveForward;
            StartUpdating();
        }

        public void OnMoveBackPressed()
        {
            Debug.Log("OnMoveBackPressed");
            InputType = PlayerInputType.MoveBack;
            StartUpdating();
        }

        public void OnJumpPressed()
        {
            InputType = PlayerInputType.Jump;
            NotifyObservers(this);
        }

        public void OnAttackPressed()
        {
            InputType = PlayerInputType.Attack;
            NotifyObservers(this);
        }

    }
}
