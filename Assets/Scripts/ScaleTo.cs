using UnityEngine;

namespace Gon
{
    public class ScaleTo : LimitedUpdateMonoBehaviour
    {
        public Vector3 destinyScale = Vector3.one;
        public float speed = 0.1f;
        public float accuracy = 0.0f;
        public bool autoStart;

        void Start() {
            if (autoStart)
                StartUpdating();
        }

        public override void LimitedUpdate()
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale,
                                                     destinyScale,
                                                     Time.deltaTime * speed);

            float distance = Vector3.Distance(transform.localScale, destinyScale);
            Debug.Log(distance);

            if (distance >= 0 && distance <= accuracy)
            {
                NotifyObservers(this);
                StopUpdating();
            }
        }

    }
}
