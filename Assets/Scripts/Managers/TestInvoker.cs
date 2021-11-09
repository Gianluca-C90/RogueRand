using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Just a Class that functions as testing for Invoking TestEvent
/// </summary>
public class TestInvoker : MonoBehaviour
{
    // Crea un'istanza di TESTEVENT
    TestEvent testEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Costruttore di TESTEVENT
        testEvent = new TestEvent();

        //Aggiungi TestInvoker alla lista di Invokers per Test Event
        EventManager.AggiungiInvokerInListaInvokersTestEvent(this);
    }

    // Logica di test per testare Invoke dell'evento
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            testEvent.Invoke();
            Destroy(this.gameObject);
        }
    }

    // Utilizziamo il delegato per andare ad appendere alla lista dei listener per l'evento TESTEVENT
    public void AggiungiListenerInListaListenersTestEvent(UnityAction listener)
    {
        testEvent.AddListener(listener);
    }
}
