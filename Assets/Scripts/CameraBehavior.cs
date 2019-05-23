using UnityEngine;
using System.Collections;

namespace Gon
{
    public class CameraBehavior : MonoBehaviour
    {
        public Transform target;
        public float speed;

        void Update()
        {
            Vector3 newPos = this.transform.position;
            newPos.x = target.transform.position.x;

            if (newPos.x > this.transform.position.x)
                this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, Time.deltaTime * speed);

        }
    }
}