using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public Rigidbody [] rb;
    public float constanceStruggle;
    public float timeTillAttack;
    public GameLoop gL;

    float minTime = 2f;
    float maxTime = 10f;
    private void Start()
    {
        gL = FindObjectOfType<GameLoop>();
        StartCoroutine(attackPlayer());
    }

    private void Update()
    {
        timeTillAttack = Mathf.Lerp(maxTime, minTime, gL.timeSurvived / 70f);
    }

    IEnumerator attackPlayer()
    {
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].AddExplosionForce(constanceStruggle, Vector3.up, 2f);
        }
        yield return new WaitForSeconds(timeTillAttack);
        StartCoroutine(attackPlayer());
    }

    public void Attack()
    {
        for(int i = 0; i < rb.Length; i++)
        {
            rb[i].AddExplosionForce(5000f, Vector3.up, 2f);
        }
    }
}
