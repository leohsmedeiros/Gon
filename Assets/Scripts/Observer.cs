using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    public interface Observer
    {
        void Notify(Object arg);    
    }
}
