using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Event int", menuName = "GameEvent/Event/Game Event int", order = 53)]
public class GameEventInt : ScriptableObject
{
    /// <summary>
    /// reference all listener in this list for this Event 
    /// </summary>
    private readonly List<GameEventListenerInt> eventListeners =
        new List<GameEventListenerInt>();

    public void Raise(int raisedNumber)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(raisedNumber);
    }

    public void RegisterListener(GameEventListenerInt listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListenerInt listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
