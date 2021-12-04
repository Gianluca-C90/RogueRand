using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseValue : MonoBehaviour
{
    private TMP_Text text;
    private int tot;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        tot = 0;
    }

    public void Increase(int value)
    {
        tot += value;
        text.text = $"x {tot}";
    }

    public int GetTot()
    {
        return tot;
    }

}
