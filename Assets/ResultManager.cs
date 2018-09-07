using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

    Text songName;
    Text combo;
    Text questStatus;
    Text questUnlocked;
    Text rank;

    int score;

    // Use this for initialization
    void Start() {
        songName = GameObject.Find("SongName").GetComponent<Text>();
        combo = GameObject.Find("Combo").GetComponent<Text>();
        questStatus = GameObject.Find("QuestStatus").GetComponent<Text>();
        questUnlocked = GameObject.Find("QuestUnlocked").GetComponent<Text>();
        rank = GameObject.Find("Rank").GetComponent<Text>();


        songName.text = PlayerPrefs.GetString("SongName");
        combo.text = PlayerPrefs.GetInt("Combo").ToString();

        if (PlayerPrefs.GetInt("Fail") == 1)
        {
            rank.text = "F";
        }
        else {
            CalculateRank();        }

        
    }


    private void CalculateRank() {
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
        }
        else if (score < 1 && score >= 0.95)
        {
            rank.text = "S";
        }
        else if (score < 0.95 && score >= 0.80)
        {
            rank.text = "A";
        }
        else if (score < 0.80 && score >= 0.60)
        {
            rank.text = "B";
        }
        else if (score < 0.60 && score >= 0.40)
        {
            rank.text = "C";
        }
        else
        {
            rank.text = "D";
        }
    }
}
