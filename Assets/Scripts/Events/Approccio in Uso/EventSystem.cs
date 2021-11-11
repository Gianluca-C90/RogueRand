using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSystem : MonoBehaviour
{
    [SerializeField] List<GameEventListener> listenForTheseEvents;

    private void OnEnable()
    {
        foreach(GameEventListener listener in listenForTheseEvents)
        {
            listener.Event.RegisterListener(listener);
        }
    }

    private void OnDisable()
    {
        foreach (GameEventListener listener in listenForTheseEvents)
        {
            listener.Event.UnregisterListener(listener);
        }
    }

    public void OnEventRaised()
    {
        foreach (GameEventListener response in listenForTheseEvents )
        {
            response.Response.Invoke();
        }
    }
}
