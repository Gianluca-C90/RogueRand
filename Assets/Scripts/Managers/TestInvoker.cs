using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestInvoker : MonoBehaviour
{
    TestEvent testEvent;
    // Start is called before the first frame update
    void Start()
    {
        testEvent = new TestEvent();
        EventManager.AggiungiInvokerInListaInvokersTestEvent(this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            testEvent.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void AggiungiListenerInListaListenersTestEvent(UnityAction listener)
    {
        testEvent.AddListener(listener);
    }
}
