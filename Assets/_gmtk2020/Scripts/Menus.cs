﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public CanvasGroup gameplayUI;
    public CanvasGroup menuUI;
    public CanvasGroup gameoverUI;

    private void Start()
    {
        menuUI.alpha = 1;
        gameoverUI.alpha = 0;
        gameplayUI.alpha = 0;
    }

    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGame()
    {
        gameplayUI.interactable = false;
        menuUI.interactable = false;
        menuUI.blocksRaycasts = false;

        float timer = 1;

        do
        {
            timer -= Time.deltaTime;

            menuUI.alpha = Mathf.Lerp(0, 1, timer);
            yield return null;
        }
        while (menuUI.alpha > 0);

        timer = 1;
        do
        {
            timer -= Time.deltaTime;

            gameplayUI.alpha = Mathf.Lerp(1, 0, timer);
            yield return null;
        }
        while (gameplayUI.alpha < 1);

        gameplayUI.interactable = true;
        GameLoop.instance.StartLoop();
    }

    public void GameOverScreen() => StartCoroutine(ShowGameOver());
    IEnumerator ShowGameOver()
    {
        gameoverUI.gameObject.SetActive(true);
        gameplayUI.interactable = false;
        menuUI.interactable = false;
        menuUI.blocksRaycasts = false;

        float timer = 1;

        do
        {
            timer -= Time.deltaTime*2;

            gameplayUI.alpha = Mathf.Lerp(0, 1, timer);
            yield return null;
        }
        while (gameplayUI.alpha > 0);

        timer = 1;
        do
        {
            timer -= Time.deltaTime*3;

            gameoverUI.alpha = Mathf.Lerp(1, 0, timer);
            yield return null;
        }
        while (gameoverUI.alpha < 1);
    }
    public void Retry() => StartCoroutine(RetryGameplay());
    IEnumerator RetryGameplay()
    {
        gameplayUI.interactable = false;
        menuUI.interactable = false;
        menuUI.blocksRaycasts = false;

        float timer = 1;

        do
        {
            timer -= Time.deltaTime;

            gameoverUI.alpha = Mathf.Lerp(0, 1, timer);
            yield return null;
        }
        while (gameoverUI.alpha > 0);
        gameoverUI.gameObject.SetActive(false);

        timer = 1;
        do
        {
            timer -= Time.deltaTime;

            gameplayUI.alpha = Mathf.Lerp(1, 0, timer);
            yield return null;
        }
        while (gameplayUI.alpha < 1);

        gameplayUI.interactable = true;
        GameLoop.instance.StartLoop();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
