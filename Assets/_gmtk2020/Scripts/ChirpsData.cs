using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChirpsData", menuName = "Chirper", order = 1)]
public class ChirpsData : ScriptableObject
{
    public List<Chirp> chirps = new List<Chirp>();
}

[System.Serializable]
public struct Chirp
{
    public string actualChirp;
    public ChirperUser user;
}

[System.Serializable]
public struct ChirperUser
{
    public string username;
    public Sprite profilePic;
}