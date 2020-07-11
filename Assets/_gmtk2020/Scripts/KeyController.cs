﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int maxKeys = 5;
    public Transform spawnArea;
    public AnxietyKey keyPrefab;
    public ParticleSystem splush;
    public CameraShake shakeCam;

    public List<AnxietyKey> spawnedKeys = new List<AnxietyKey>();
    private List<Transform> availableSpaces = new List<Transform>();
    private List<KeyCode> allowedKeys = new List<KeyCode>() { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.I, KeyCode.O, KeyCode.P };

    private void Start()
    {
        for (int i = 0; i < spawnArea.childCount; i++)
        {
            availableSpaces.Add(spawnArea.GetChild(i));
        }
    }

    private void Update()
    {
        if (!GameLoop.instance.isPlaying) return;

        for (int i = 0; i < spawnedKeys.Count; i++)
        {
            if (spawnedKeys[i].IsCompleted())
            {
                
                DespawnKey(spawnedKeys[i]);
            }
        }
    }

    void SpawnKey()
    {
        if (spawnedKeys.Count < maxKeys)
        {
            var k = Instantiate(keyPrefab, availableSpaces[Random.Range(0, availableSpaces.Count)]);
            availableSpaces.Remove(k.transform.parent);

            k.Setup(allowedKeys[Random.Range(0, allowedKeys.Count)], Random.Range(0f, 1f));
            allowedKeys.Remove(k.kcode);
            spawnedKeys.Add(k);
        }
    }

    public void DespawnKey(AnxietyKey k)
    {
        splush.transform.position = k.transform.position;
        splush.Play();

        shakeCam.SetCameraShake(0.25f);

        allowedKeys.Add(k.kcode);
        availableSpaces.Add(k.transform.parent);
        spawnedKeys.Remove(k);

        Destroy(k.gameObject);
    }

    public void StartSpawning() => StartCoroutine("SpawnRoutine");
    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            if(GameLoop.instance.isPlaying)
                SpawnKey();

            yield return new WaitForSeconds(3);
        }
    }

    public void StopSpawning() => StartCoroutine("DestroyAllKeys");
    IEnumerator DestroyAllKeys()
    {
        StopCoroutine("SpawnRoutine");
        for (int i = 0; i < spawnedKeys.Count; i++)
        {
            yield return new WaitForSeconds(0.25f);

            splush.transform.position = spawnedKeys[i].transform.position;
            splush.Play();

            yield return new WaitForEndOfFrame();

            shakeCam.AddCameraShake(0.33f);
            Destroy(spawnedKeys[i].gameObject);
        }

        spawnedKeys.Clear();
    }
}