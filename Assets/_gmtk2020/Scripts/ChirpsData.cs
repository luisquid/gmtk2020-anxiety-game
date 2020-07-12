using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChirpsData", menuName = "Chirper", order = 1)]
public class ChirpsData : ScriptableObject
{
    public string[] chirps;
    public ChirperUser[] users;
}

[System.Serializable]
public struct ChirperUser
{
    public string username;
    public Sprite[] profilePic;
}