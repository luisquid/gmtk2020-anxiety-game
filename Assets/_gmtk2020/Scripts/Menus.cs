using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        sources = FindObjectsOfType<AudioSource>();
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
        gameplayUI.blocksRaycasts = false;
        gameplayUI.interactable = false;

        yield return new WaitForSeconds(4f);

        gameoverUI.gameObject.SetActive(true);
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
        GameLoop.instance.chirpsManager.Setup();

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

        gameplayUI.blocksRaycasts = true;
        gameplayUI.interactable = true;
        GameLoop.instance.StartLoop();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private AudioSource[] sources;
    public Image volumeIcon;
    public void ToggleAudio()
    {
        if(!sources[0].mute)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].mute = true;
            }
            volumeIcon.color = Color.white.setAlpha(0.5f);
        }
        else
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].mute = false;
            }
            volumeIcon.color = Color.white.setAlpha(1);
        }
    }
}
