using UnityEngine;

namespace Gon
{
    public class MoveTo : LimitedUpdateMonoBehaviour
    {
        public Vector3 destinyPosition;
        public float speed = 40;
        public float accuracy = 0.0f;
        public bool autoStart;

        void Start() {
            if (autoStart)
                StartUpdating();
        }

        public override void LimitedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     destinyPosition,
                                                     Time.deltaTime * speed);

            float distance = Vector3.Distance(transform.position, destinyPosition);
            Debug.Log(distance);

            if (distance >= 0 && distance <= accuracy)
            {
                NotifyObservers(this);
                StopUpdating();
            }
        }

    }
}
