﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyKey : MonoBehaviour
{
    public CanvasGroup keyGroup;
    public Text keyText;

    public KeyCode kcode;
    public float initialSize;

    private float timeLeft, duration;

    public void Setup(KeyCode newCode, float difficulty = 0)
    {
        kcode = newCode;
        keyText.text = kcode.ToString();

        duration = Mathf.Lerp(1, 5, difficulty);
        timeLeft = duration;

        initialSize =  (0.5f + difficulty * 1.5f);
        transform.localScale = Vector3.one * initialSize;

        transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));
    }

    public bool IsCompleted()
    {
        if(Input.GetKey(kcode))
        {
            timeLeft -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * initialSize, timeLeft/duration);
  
            if(transform.localScale.magnitude <= 0.3f)
            {
                return true;
            }
        }

        return false;
    }
}
