using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    [RequireComponent(typeof(MoveTo))]
    public class OpenCloseMenu : MonoBehaviour, Observer
    {
        public Vector3 positionOpened;
        public Vector3 positionClosed;
        public Transform menuArrowTransform;

        private Vector3 scaleOpened;
        private Vector3 scaleClosed;

        private MoveTo moveToComponent;
        private bool isOpen = false;
        private bool isInteractEnabled = true;


        void Start()
        {
            moveToComponent = this.transform.GetComponent<MoveTo>();
            moveToComponent.AddObserver(this);

            scaleOpened = new Vector3(-1, 1, 1);
            scaleClosed = new Vector3(1, 1, 1);
        }

        public void OnClick()
        {
            if (isInteractEnabled)
            {
                isInteractEnabled = false;
                menuArrowTransform.localScale = isOpen ? scaleClosed : scaleOpened;
                moveToComponent.destinyPosition = isOpen ? positionClosed : positionOpened;
                moveToComponent.StartUpdating();
            }
        }

        public void Notify(Object arg)
        {
            Debug.Log("Notify");
            isOpen = !isOpen;
            isInteractEnabled = true;
        }
    }
}