using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChirpUI : MonoBehaviour
{
    public Image pic;
    public Text username;
    public Text chirp;

    public void Setup(Chirp data)
    {
        pic.sprite = data.user.profilePic;
        username.text = data.user.username;
        chirp.text = data.actualChirp;
    }
}
