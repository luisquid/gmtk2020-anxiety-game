using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup gameplayUI;
    public CanvasGroup menuUI;

    private void Start()
    {
        menuUI.alpha = 1;
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
}
