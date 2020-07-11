using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public static GameLoop instance;

    public Menus menu;
    public KeyController keyManager;
    public Image[] hpSprites;

    public float timeSurvived;
    public Text score;
    public bool isPlaying;

    [Header("Game Over")]
    public Text finalScore;
    public Text bestScore;

    private int currentHP;

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
        timeSurvived = 0;
        currentHP = hpSprites.Length;
        for (int i = 0; i < hpSprites.Length; i++)
        {
            hpSprites[i].gameObject.SetActive(currentHP >= i);
        }

        isPlaying = true;
        keyManager.StartSpawning();
    }

    public void Damage()
    {
        currentHP--;
        for (int i = 0; i < hpSprites.Length; i++)
        {
            hpSprites[i].gameObject.SetActive(currentHP-1 >= i);
        }

        if (currentHP < 1)
            GameOver();
    }

    public void GameOver()
    {
        isPlaying = false;
        keyManager.StopSpawning();

        finalScore.text = score.text;

        if(PlayerPrefs.GetFloat("highScore",0) < timeSurvived)
        {
            PlayerPrefs.SetFloat("highScore", timeSurvived);
            bestScore.text = "New High Score: " + score.text;
        }
        else
        {
            int min = (int)PlayerPrefs.GetFloat("highScore", 0) / 60;
            int sec = (int)PlayerPrefs.GetFloat("highScore", 0) - 60 * min;
            int mil = (int)(100 * (PlayerPrefs.GetFloat("highScore", 0) - min * 60 - sec));
            bestScore.text = "High score: " + string.Format("{0:00}:{1:00}:{2:00}", min, sec, mil);
        }
        menu.GameOverScreen();
    }
}
