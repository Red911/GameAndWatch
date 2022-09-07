using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_GameEvent/Event Listener/Int Event", order: 51)]
public class GameEventListenerInt : MonoBehaviour
{
    [SerializeField] private GameEventInt gameEvent;
    [SerializeField] private UnityEvent<int> response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(int numberRaised)
    {
        response.Invoke(numberRaised);
    }
}