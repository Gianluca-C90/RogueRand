using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event System Class
/// </summary>
public static class EventManager
{
    #region Fields
    // Lista degli invoker per l'evento TESTEVENT
    // La lista deve essere del tipo di Classe che invoca l'evento 
    static List<TestInvoker> listaInvokersTestEvent = new List<TestInvoker>();

    // Lista dei Listener per l'evento TESTEVENT
    // La firma della unityaction è senza parametri
    static List<UnityAction> listaListenersTestEvent =
        new List<UnityAction>();
    #endregion

    #region Public Methods

    /// <summary>
    /// Questa Funzione va ad aggiungere un invoker alla lista degli invokers dell'evento TESTEVENT
    /// e per ogni listener nella lista dei listeners di TESTEVENT
    /// va a collegare l'invoker ai suoi listeners
    /// </summary>
    /// <param name="invoker">l'invoker di classe TestInvoker </param>
    public static void AggiungiInvokerInListaInvokersTestEvent(TestInvoker invoker)
    {
        // add invoker to list and add all listeners to invoker
        listaInvokersTestEvent.Add(invoker);
        foreach (UnityAction listener in listaListenersTestEvent)
        {
            invoker.AggiungiListenerInListaListenersTestEvent(listener);
        }
    }


    /// <summary>
    ///  Questa Funzione va ad aggiungere un listener alla lista dei listeners dell'evento TESTEVENT
    /// </summary>
    /// <param name="listener">il listener eredita da UnityAction</param>
    public static void AggiungiListenerInListaListenersTestEvent(UnityAction listener)
    {

        // add listener to list and to all invokers
        listaListenersTestEvent.Add(listener);
        foreach  (TestInvoker invoker in listaInvokersTestEvent)
        {
            invoker.AggiungiListenerInListaListenersTestEvent(listener);
        }
    }

    #endregion
}
