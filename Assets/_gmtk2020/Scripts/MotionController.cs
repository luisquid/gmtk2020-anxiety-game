using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;
public class MotionController : MonoBehaviour
{
    public PuppetMaster master;
    public bool dead = false;

    public float weight = 1f;
    public float muscle = 1f;

    public void KillRagdoll(bool state)
    {
        master.state = state ? PuppetMaster.State.Dead : PuppetMaster.State.Alive;
    }

    public void SetRagdollWeight(float _value)
    {
        master.pinWeight -= _value;
    }

    public void SetMuscleWeight(float _value)
    {
        master.muscleWeight -= _value;
    }
}
