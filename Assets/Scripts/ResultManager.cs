using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

    Text songName;
    Text combo;
    Text questStatus;
    Text rank;

    private AudioSource audioSourcer;
    public AudioClip successSFX;
    public AudioClip failureSFX;

    int score;

    // Use this for initialization
    void Start() {
        songName = GameObject.Find("SongName").GetComponent<Text>();
        combo = GameObject.Find("Combo").GetComponent<Text>();
        questStatus = GameObject.Find("QuestStatus").GetComponent<Text>();
        rank = GameObject.Find("Rank").GetComponent<Text>();
        audioSourcer = GetComponent<AudioSource>();

        songName.text = "Song: " + PlayerPrefs.GetString("SongName");

        if (PlayerPrefs.GetString("CurrentScene") == "Level1")
        {
            combo.text = "Max Combo: " + PlayerPrefs.GetInt("Combo").ToString() + " / "
            + PlayerPrefs.GetInt("L1Notes").ToString();
        }
        else if (PlayerPrefs.GetString("CurrentScene") == "Level2")
        {
            combo.text = "Max Combo: " + PlayerPrefs.GetInt("Combo").ToString() + " / "
            + PlayerPrefs.GetInt("L2Notes").ToString();
        }
        else if (PlayerPrefs.GetString("CurrentScene") == "Level3")
        {
            combo.text = "Max Combo: " + PlayerPrefs.GetInt("Combo").ToString() + " / "
            + PlayerPrefs.GetInt("L3Notes").ToString();
        }

        if (PlayerPrefs.GetInt("Fail") == 1)
        {
            // If player fails the last scene, set rank to F
            rank.text = "F";
            questStatus.text = "Quest Status: Failed";
            audioSourcer.clip = failureSFX;
            audioSourcer.Play();
        }
        else {
            CalculateRank();
            questStatus.text = "Quest Status: Completed";
            audioSourcer.clip = successSFX;
            audioSourcer.Play();

            if (PlayerPrefs.GetString("CurrentScene") == "Level1")
            {
                PlayerPrefs.SetInt("Quest2Unlocked", 1);
            }
            else if (PlayerPrefs.GetString("CurrentScene") == "Level2")
            {
                PlayerPrefs.SetInt("Quest3Unlocked", 1);
            }
            else if (PlayerPrefs.GetString("CurrentScene") == "Level3") {
                PlayerPrefs.SetInt("Quest3Completed", 1);
            }
        }
    }


    private void CalculateRank() {
        print(PlayerPrefs.GetInt("Combo"));
        print(PlayerPrefs.GetInt("L1Notes"));

        if (PlayerPrefs.GetString("CurrentScene") == "Level1")
        {
            score = PlayerPrefs.GetInt("Combo") / PlayerPrefs.GetInt("L1Notes");
        }
        else if (PlayerPrefs.GetString("CurrentScene") == "Level2")
        {
            score = PlayerPrefs.GetInt("Combo") / PlayerPrefs.GetInt("L2Notes");
        }
        else if (PlayerPrefs.GetString("CurrentScene") == "Level3")
        {
            score = PlayerPrefs.GetInt("Combo") / PlayerPrefs.GetInt("L3Notes");
        }
        else {
            score = 0;
        }
         
        if (score == 1)
        {
            rank.text = "SS";
            rank.color = new Color32(255, 245, 141, 255);
        }
        else if (score < 1 && score >= 0.9)
        {
            rank.text = "S";
            rank.color = new Color32(255, 245, 141, 255);
        }
        else if (score < 0.9 && score >= 0.60)
        {
            rank.text = "A";
            rank.color = new Color32(255, 114, 91, 255);
        }
        else if (score < 0.60 && score >= 0.30)
        {
            rank.text = "B";
            rank.color = new Color32(255, 114, 91, 255);
        }
        else
        {
            rank.text = "C";
            rank.color = new Color32(137, 164, 231, 255);
        }
    }
}
