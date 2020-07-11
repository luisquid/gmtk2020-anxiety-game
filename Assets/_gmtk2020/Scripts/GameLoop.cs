using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public static GameLoop instance;

    public MainMenu menu;
    public KeyController keyManager;

    public float timeSurvived;
    public Text score;
    public bool isPlaying;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(isPlaying)
        {
            timeSurvived += Time.deltaTime;
            int min = (int)timeSurvived / 60;
            int sec = (int)timeSurvived - 60 * min;
            int mil = (int)(100 * (timeSurvived - min * 60 - sec));

            score.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, mil);
        }
    }

    public void StartLoop()
    {
        Debug.Log("LOOOPS!");
        isPlaying = true;
        keyManager.StartSpawning();
    }

    public void GameOver()
    {
        keyManager.StopSpawning();

    }
}
