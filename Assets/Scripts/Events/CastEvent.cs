using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CastEvent", menuName = "ScriptableObjects/CastEvent", order = 6)]
public class CastEvent : ScriptableObject
{
    private readonly List<CastEventListener> eventListeners = 
        new List<CastEventListener>();

    public void Raise(KeyCode keycode)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(keycode);
    }

    public void RegisterListener(CastEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(CastEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
