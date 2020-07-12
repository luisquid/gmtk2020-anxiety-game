using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Cues : MonoBehaviour
{
    public UnityEngine.Rendering.VolumeProfile volumeProfile;

    public UnityEngine.Rendering.Universal.ChromaticAberration chromaticProperty;

    public float downSpeed;

    private float chromaticLevel;

    private void Start()
    {
        volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;

        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out chromaticProperty)) throw new System.NullReferenceException(nameof(chromaticProperty));

        //chromaticProperty.intensity.Override(0.5f);
    }

    public void DamageCue() => StartCoroutine(CallAberration());

    IEnumerator CallAberration()
    {
        chromaticLevel = 1f;
        while(chromaticLevel > 0f)
        {
            chromaticLevel -= Time.deltaTime * downSpeed;
            chromaticProperty.intensity.Override(chromaticLevel);
            yield return null;
        }
    }
}
