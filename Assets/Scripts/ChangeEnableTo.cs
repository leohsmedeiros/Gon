using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    public class ChangeEnableTo : MonoBehaviour
    {
        public GameObject target;

        public void ChangeEnable ()
        {
            target.SetActive(!target.activeSelf);
        }
    }
}