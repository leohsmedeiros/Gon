using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    public abstract class LimitedUpdateMonoBehaviour : ObservableMonoBehaviour {
        private bool shouldUpdate = false;

        public void StartUpdating () {
            shouldUpdate = true;
        }

        public void StopUpdating() {
            shouldUpdate = false;
        }

        void Update() {
            if (shouldUpdate) {
                LimitedUpdate();
            }
        }

        public abstract void LimitedUpdate();

    }
}