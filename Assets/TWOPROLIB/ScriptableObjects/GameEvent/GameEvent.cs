using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "GameEvent/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UngisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
