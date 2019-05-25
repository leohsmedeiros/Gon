using UnityEngine;
using UniRx;

namespace Gon {
    public class RxOnMouseOver : MonoBehaviour {
        public readonly Subject<Unit> onMouseOver = new Subject<Unit>();
        private bool onMouseDown = false;

        private void OnMouseDown() {
            onMouseDown = true;
        }

        private void OnMouseUp() {
            onMouseDown = false;
        }

        private void OnMouseOver() {
            if (onMouseDown)
                onMouseOver.OnNext(Unit.Default);
        }
    }
}
