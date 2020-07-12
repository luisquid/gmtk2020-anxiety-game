using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chirper : MonoBehaviour
{
    public GameObject chirpPrefab;
    public Text newChirps;
    public Animator chirpsAlert;
    public Animation phone;
    public ChirpsData allChirps;

    private ScrollRect scroll;

    private int newSpawnedChirps;
    public int totalChirpsRead;


    private void Awake()
    {
        scroll = GetComponentInChildren<ScrollRect>();
        Setup();
    }

    public void Evaluate(Vector2 v)
    {
        if(scroll.verticalNormalizedPosition >= 1)
        {
            chirpsAlert.SetBool("show", false);
            totalChirpsRead += newSpawnedChirps;
            newSpawnedChirps = 0;
        }
    }

    public void Setup()
    {
        //SETUP
        foreach(Transform ch in scroll.content)
        {
            if(!ch.name.Contains("Welcome"))
                Destroy(ch.gameObject);
        }

        scroll.verticalNormalizedPosition = 0;
        totalChirpsRead = 0;
        newSpawnedChirps = 0;

        newChirps.text = "no new chirps available!";
    }

    public void StartChirps()=> StartCoroutine("SpawnChirps");
    public void StopChirps() => StopCoroutine("SpawnChirps");
    IEnumerator SpawnChirps()
    {

        while(true)
        {
            var ch = Instantiate(chirpPrefab, scroll.content);
            ch.transform.SetAsFirstSibling();

            AudioManager.instance.NewChirp();
            chirpsAlert.SetBool("show", true);
            phone.PlayNormal(3);

            newSpawnedChirps++;
            newChirps.text = $"{newSpawnedChirps} new chirps!";
            yield return new WaitForSeconds(Random.Range(0.3f,3));
        }
    }
}
