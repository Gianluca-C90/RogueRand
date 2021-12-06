using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptionsManager : MonoBehaviour
{
    private static CaptionsManager instance;

    private static int index = 0;

    private void OnEnable() 
    {
        if (instance == null)
            instance = this;

        ActivateCaption();
    }

    public static void ActivateCaption()
    {
        if (instance.transform.childCount > index)
        {
            instance.transform.GetChild(index).gameObject.SetActive(true);
            Time.timeScale = 0;
        }        
    }

    public static void CloseCaption()
    {
        instance.transform.GetChild(index).gameObject.SetActive(false);
        index++;
        Time.timeScale = 1;
    }
}
