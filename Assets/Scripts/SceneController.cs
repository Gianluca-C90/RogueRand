using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(int index)
    {
#if !UNITY_EDITOR
        //Debug.Log(AlgoServer.instance.connected);

        if (AlgoServer.instance.connected)
            SceneManager.LoadScene(index);
        else
            AlgoServer.Hello();
#elif UNITY_EDITOR
        SceneManager.LoadScene(index);

#endif

    }
}

