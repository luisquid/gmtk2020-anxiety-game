using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public static GameLoop instance;

    public Menus menu;
    public KeyController keyManager;
    public MotionController motionController;
    public RagdollManager ragManager;
    public ParticleSystem dropsParticles;
    public ParticleSystem smokeParticles;
    public Cues cuesManager;
    public Chirper chirpsManager;
    public Image[] hpSprites;

    public float timeSurvived;
    public Text score;
    public bool isPlaying;

    [Header("Game Over")]
    public Text finalScore;
    public Text chirpsRead;
    public Text bestScore;

    private int currentHP;
    public AnimationCurve difficultyCurve;

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

            var main = dropsParticles.main;
            main.simulationSpeed = timeSurvived * 0.05f;
        }
    }

    public void StartLoop()
    {
        timeSurvived = 0;
        currentHP = hpSprites.Length;
        smokeParticles.Stop();
        for (int i = 0; i < hpSprites.Length; i++)
        {
            hpSprites[i].gameObject.SetActive(currentHP >= i);
        }

        isPlaying = true;
        motionController.master.pinWeight = 1f;
        motionController.master.muscleWeight = 1f;
        motionController.KillRagdoll(false);
        keyManager.StartSpawning();
        chirpsManager.StartChirps();
    }

    public void Damage()
    {
        currentHP--;

        ragManager.Attack();
        motionController.SetRagdollWeight(0.3f);
        motionController.SetMuscleWeight(0.3f);
        cuesManager.DamageCue();
        //motionController.SetRagdollWeight();
        for (int i = 0; i < hpSprites.Length; i++)
        {
            hpSprites[i].gameObject.SetActive(currentHP-1 >= i);
        }

        if (currentHP < 1)
            GameOver();
        else
            keyManager.ClearAllKeys();
    }

    public void GameOver()
    {
        var main = dropsParticles.main;
        main.simulationSpeed = 0f;
        smokeParticles.Play();

        isPlaying = false;
        keyManager.StopSpawning();
        chirpsManager.StopChirps();
        motionController.KillRagdoll(true);
        finalScore.text = score.text;
        chirpsRead.text = $"and read {chirpsManager.totalChirpsRead} chirps";

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

    //Difficulty
    public float TimeBetweenSpawns{ get{ return Mathf.Lerp(5, 1, difficultyCurve.Evaluate(timeSurvived / 70f));}}
    public float TimeBeforeKeyExplodes { get{ return Mathf.Lerp(10, 5, difficultyCurve.Evaluate(timeSurvived / 70f)); }}
}
