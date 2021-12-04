using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Debug.Log(AlgoServer.instance.connected);
        if (AlgoServer.instance.connected)
            SceneManager.LoadScene(index);
        else
            AlgoServer.Hello();
    }
}
