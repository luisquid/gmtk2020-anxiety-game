using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyKey : MonoBehaviour
{
    public CanvasGroup keyGroup;
    public Text keyText;

    public KeyCode kcode;
    public float duration;

    private float timePressed;

    private void Start()
    {
        timePressed = duration;
    }

    public bool IsCompleted()
    {
        if(Input.GetKey(kcode))
        {
            timePressed -= Time.deltaTime;

            keyGroup.alpha = timePressed / duration;

            if(keyGroup.alpha <= 0)
            {
                return true;
            }
        }

        return false;
    }
}
