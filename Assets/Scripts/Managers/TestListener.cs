using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.AggiungiListenerInListaListenersTestEvent(FireTestEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Private Methods
    void FireTestEvent()
    {
        Debug.Log("The Event Just Fired!");
    }
    #endregion
}
