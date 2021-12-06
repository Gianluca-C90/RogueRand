using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLerp : MonoBehaviour
{
    public CanvasGroup canvas;
    float t = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (canvas.alpha < 1)
        {
            canvas.alpha = Mathf.Lerp(0, 1, t);

            t += 0.5f * Time.deltaTime;
        }

    }
}
