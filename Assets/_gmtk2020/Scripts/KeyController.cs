using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int maxKeys = 5;
    public Transform spawnArea;
    public AnxietyKey keyPrefab;

    public List<AnxietyKey> spawnedKeys = new List<AnxietyKey>();
    private List<Transform> availableSpaces = new List<Transform>();
    private List<KeyCode> allowedKeys = new List<KeyCode>() { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Z, KeyCode.X, KeyCode.C,KeyCode.B, KeyCode.N,KeyCode.M,KeyCode.J, KeyCode.K, KeyCode.L,KeyCode.I,KeyCode.O,KeyCode.P };

    private void Start()
    {
        for (int i = 0; i < spawnArea.childCount; i++)
        {
            availableSpaces.Add(spawnArea.GetChild(i));
        }

        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        for (int i = 0; i < spawnedKeys.Count; i++)
        {
            if(spawnedKeys[i].IsCompleted())
            {
                DespawnKey(spawnedKeys[i]);
            }
        }
    }

    void SpawnKey()
    {
        if(spawnedKeys.Count < maxKeys)
        {
            var k = Instantiate(keyPrefab,availableSpaces[Random.Range(0,availableSpaces.Count)]);
            availableSpaces.Remove(k.transform.parent);

            //Randomize keycode
            k.kcode = allowedKeys[Random.Range(0, allowedKeys.Count)];
            k.keyText.text = k.kcode.ToString();
            allowedKeys.Remove(k.kcode);

            spawnedKeys.Add(k);
        }
    }

    void DespawnKey(AnxietyKey k)
    {
        allowedKeys.Add(k.kcode);
        availableSpaces.Add(k.transform.parent);
        spawnedKeys.Remove(k);

        Destroy(k.gameObject);
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            SpawnKey();

            yield return new WaitForSeconds(3);
        }
    }
}