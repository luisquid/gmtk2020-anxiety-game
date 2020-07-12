using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyKey : MonoBehaviour
{
    public Text keyText;

    public KeyCode kcode;
    public float initialSize;
    public float life;

    private float timeLeft, duration;
    public Animation anim;

    public void Setup(KeyCode newCode, float size = 0)
    {
        kcode = newCode;
        keyText.text = kcode.ToString();

        duration = Mathf.Lerp(1, 3, size);
        timeLeft = duration;

        life = GameLoop.instance.TimeBeforeKeyExplodes;
        initialSize =  1f + size*1.25f;
        transform.localScale = Vector3.one * initialSize;

        transform.Rotate(Vector3.forward, Random.Range(-3f,3f));

        StartCoroutine(LifeTimer());
    }

    public bool IsCompleted()
    {
        if(Input.GetKey(kcode))
        {
            timeLeft -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * initialSize, timeLeft/duration);
  
            if(transform.localScale.magnitude <= 0.3f)
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator LifeTimer()
    {
        do
        {
            anim.SetSpeed(Mathf.Lerp(3,0.25f,life/10));
            life -= Time.deltaTime;

            do
            {
                yield return null;
            } while (Input.GetKey(kcode));

        }
        while (life > 0);

        yield return new WaitForEndOfFrame();

        GameLoop.instance.keyManager.DespawnKey(this, false);
        GameLoop.instance.Damage();
    }
}
