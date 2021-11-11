using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Just a class to test listening for a TESTEVENT event
/// </summary>
public class TestListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //add TestListener's FireTestEvent Funcion as a listener for the event TESTEVENT
        EventManager.AggiungiListenerInListaListenersTestEvent(FireTestEvent);
    }


    #region Private Methods
    /// <summary>
    /// just a dumb function to print some log when TESTEVENT is triggered
    /// </summary>
    void FireTestEvent()
    {
        Debug.Log("The Event Just Fired!");
    }
    #endregion
}
