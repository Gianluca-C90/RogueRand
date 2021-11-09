using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static List<TestInvoker> listaInvokersTestEvent = new List<TestInvoker>();
    static List<UnityAction> listaListenersTestEvent =
        new List<UnityAction>();

    #region public methods
    public static void AggiungiInvokerInListaInvokersTestEvent(TestInvoker invoker)
    {
        // add invoker to list and add all listeners to invoker
        listaInvokersTestEvent.Add(invoker);
        foreach (UnityAction listener in listaListenersTestEvent)
        {
            invoker.AggiungiListenerInListaListenersTestEvent(listener);
        }
    }

    public static void AggiungiListenerInListaListenersTestEvent(UnityAction listener)
    {
        listaListenersTestEvent.Add(listener);
        foreach  (TestInvoker invoker in listaInvokersTestEvent)
        {
            invoker.AggiungiListenerInListaListenersTestEvent(listener);
        }
    }
    #endregion
}
