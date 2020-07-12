using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Chirper : MonoBehaviour
{
    public ChirpUI chirpPrefab;
    public Text newChirps;
    public Animator chirpsAlert;
    public Animation phone;
    public ChirpsData allChirps;

    private List<Chirp> chirpsToSpawn;

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

    int chirpIndex;
    public void Setup()
    {
        chirpIndex = 0;
        chirpsToSpawn = allChirps.chirps.OrderBy(x => Random.value).ToList();

        //SETUP
        foreach (Transform ch in scroll.content)
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
            
            ch.Setup(chirpsToSpawn[chirpIndex]);
            //Debug.Log(chirpsToSpawn[chirpIndex].actualChirp);
            if(chirpIndex+1 < chirpsToSpawn.Count)
            {
                chirpIndex++;
            }
            else
            {
                chirpIndex = 0;
                chirpsToSpawn = allChirps.chirps.OrderBy(x => Random.value).ToList();
            }

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
