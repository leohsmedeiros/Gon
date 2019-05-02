using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    public class GonLogoController : MonoBehaviour, Observer
    {

        public MoveTo moveToObject;
        public GameObject lightsParticles;
        public ScaleTo scaleToObject;

        void Start ()
        {
            moveToObject.AddObserver(this);
            scaleToObject.AddObserver(this);
        }

        public void Notify(Object arg)
        {
            //MoveTo is finished and now start scaling stars
            if (arg is MoveTo)
            {
                lightsParticles.SetActive(true);
                scaleToObject.StartUpdating();
            }
        }

    }
}
