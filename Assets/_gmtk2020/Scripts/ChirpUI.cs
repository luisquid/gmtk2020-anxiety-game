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
        chirp.text = "";

        string[] split = data.actualChirp.Split(' ');
        foreach(string s in split)
        {

            if(s.Contains("@") || s.Contains("#"))
                chirp.text += $"<color=#F70058>{s}</color> ";
            else
                chirp.text += s + " ";
        }
    }
}
