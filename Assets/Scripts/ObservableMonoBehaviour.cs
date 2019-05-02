using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gon
{
    public class ObservableMonoBehaviour: MonoBehaviour
    {
        private List<Observer> observers;

        private void Awake()
        {
            observers = new List<Observer>();
        }

        public void AddObserver (Observer observer)
        {
            observers.Add(observer);
        }

        public void DeleteObserver(Observer observer)
        {
            observers.Remove(observer);
        }

        public void DeleteObservers()
        {
            observers.Clear();
        }

        public void NotifyObservers(Object arg)
        {
            foreach (Observer observer in observers)
            {
                observer.Notify(arg);
            }
        }

        public virtual void NotifyObservers()
        {
            NotifyObservers(null);
        }
    }
}
