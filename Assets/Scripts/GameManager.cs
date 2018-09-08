using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int comboCount = 0;
    public int maxCombo = -1;

    private Character character;
    private Text healthText;
    private Text comboText;

    Recorder upRecord;
    Recorder leftRecord;
    Recorder rightRecord;

    private NoteSpawner upNoteSpawner;
    private NoteSpawner leftNoteSpawner;
    private NoteSpawner rightNoteSpawner;

    private AudioSource music;

    int bpm = 30;

    float songPosition;
    public float songPosInBeats;
    float secPerBeat;
    float dsptimesong;

    int upIndex = 0;
    int leftIndex = 0;
    int rightIndex = 0;
    int upNextIndex = 0;
    int leftNextIndex = 0;
    int rightNextIndex = 0;

    float[] l1UpNotes = { 1f, 15.9f, 16.6f, 18.3f, 18.5f, 19.4f, 20.4f, 23.2f, 24.1f, 25f, 27.1f, 27.6f, 28.6f, 29.7f, 30.7f, 31.2f, 31.7f, 32.1f, 32.6f, 33.1f, 33.5f};
    float[] l1LeftNotes = {1f, 1.6f, 2.5f, 3.2f, 4.2f, 5.1f, 6.1f, 7f, 7.9f, 8.9f, 9.8f, 10.8f, 11.7f, 12.7f, 13.6f, 14.5f, 15.5f, 15.7f, 16.4f, 17.3f, 17.8f, 19.2f, 20.2f, 21.1f, 21.5f, 22.1f, 22.1f, 23.1f, 23.8f, 24.8f, 25.7f, 26.7f, 27.9f, 28.8f, 29.5f};
    float[] l1RightNotes = { 16.8f, 17.5f, 19.6f, 20.6f, 21.3f, 22.2f, 23.4f, 24.3f, 25.3f, 25.9f, 26.9f, 28.1f, 29f};
    float[] l2UpNotes = { };
    float[] l2LeftNotes = { };
    float[] l2RightNotes = { };
    float[] l3UpNotes = { };
    float[] l3LeftNotes = { };
    float[] l3RightNotes = { };


    int index = 0;

    // Use this for initialization
    void Start() {
        PlayerPrefs.SetInt("L1Notes", l1UpNotes.Length + l1LeftNotes.Length + l1RightNotes.Length);
        PlayerPrefs.SetInt("L2Notes", l1UpNotes.Length + l1LeftNotes.Length + l1RightNotes.Length);
        PlayerPrefs.SetInt("L3Notes", l1UpNotes.Length + l1LeftNotes.Length + l1RightNotes.Length);

        maxCombo = -1;
        character = GameObject.Find("Character").GetComponent<Character>();
        healthText = GameObject.Find("HealthBar").GetComponent<Text>();
        comboText = GameObject.Find("ComboBar").GetComponent<Text>();
        upNoteSpawner = GameObject.Find("UpSpawner").GetComponent<NoteSpawner>();
        leftNoteSpawner = GameObject.Find("LeftSpawner").GetComponent<NoteSpawner>();
        rightNoteSpawner = GameObject.Find("RightSpawner").GetComponent<NoteSpawner>();

        upRecord = GameObject.Find("RecorderUp").GetComponent<Recorder>();
        leftRecord = GameObject.Find("RecorderLeft").GetComponent<Recorder>();
        rightRecord = GameObject.Find("RecorderRight").GetComponent<Recorder>();

        music = GameObject.Find("Music").GetComponent<AudioSource>();

        secPerBeat = 60f / bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        Debug.Log("secPerBeat: " + secPerBeat);
        Debug.Log("dsptimesong: " + dsptimesong);
    }

    // Update is called once per frame
    void Update() {
        //if (character.characterHealth <= 0)
        //{
        //    PlayerPrefs.SetInt("Fail", 1);
        //    SceneManager.LoadScene("Result");
        //}

        //if (!music.isPlaying) {
        //    PlayerPrefs.SetInt("Fail", 0);
        //    PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        //    PlayerPrefs.SetString("SongName", music.clip.name);
        //    PlayerPrefs.SetInt("Combo", maxCombo);
            
        //    SceneManager.LoadScene("Result");
        //}


        if (maxCombo <= comboCount)
        {
            maxCombo = comboCount;
        }

        healthText.text = character.characterHealth.ToString();
        comboText.text = comboCount.ToString();

        songPosition = (float)AudioSettings.dspTime - dsptimesong;
        songPosInBeats = (float)Math.Round((songPosition / secPerBeat), 1);


        if (upNextIndex < l1UpNotes.Length && l1UpNotes[upNextIndex] < songPosInBeats)
        {
            if (l1UpNotes[upNextIndex] == l1LeftNotes[leftNextIndex])
            {
                upNoteSpawner.SpawnDoubleNote();
                upNextIndex++;
                leftNoteSpawner.SpawnDoubleNote();
                leftNextIndex++;
            } else if (l1UpNotes[upNextIndex] == l1RightNotes[rightNextIndex]) {
                upNoteSpawner.SpawnDoubleNote();
                upNextIndex++;
                rightNoteSpawner.SpawnDoubleNote();
                rightNextIndex++;
            }
            else {
                upNoteSpawner.SpawnSingleNote();
                upNextIndex++;
            }
        }
        if (leftNextIndex < l1LeftNotes.Length && l1LeftNotes[leftNextIndex] < songPosInBeats)
        {
            if (l1LeftNotes[leftNextIndex] == l1UpNotes[upNextIndex] )
            {
                leftNoteSpawner.SpawnDoubleNote();
                leftNextIndex++;
                upNoteSpawner.SpawnDoubleNote();
                upNextIndex++;
            } else if (l1LeftNotes[upNextIndex] == l1RightNotes[rightNextIndex])
            {
                leftNoteSpawner.SpawnDoubleNote();
                leftNextIndex++;
                rightNoteSpawner.SpawnDoubleNote();
                rightNextIndex++;
            }
            else
            {
                leftNoteSpawner.SpawnSingleNote();
                leftNextIndex++;
            }
        }
        if (rightNextIndex < l1RightNotes.Length && l1RightNotes[rightNextIndex] < songPosInBeats)
        {
            if (l1RightNotes[leftNextIndex] == l1UpNotes[upNextIndex])
            {
                rightNoteSpawner.SpawnDoubleNote();
                rightNextIndex++;
                upNoteSpawner.SpawnDoubleNote();
                upNextIndex++;
            } else if (l1RightNotes[rightNextIndex] == l1LeftNotes[upNextIndex]) {
                rightNoteSpawner.SpawnDoubleNote();
                rightNextIndex++;
                leftNoteSpawner.SpawnDoubleNote();
                leftNextIndex++;
            }
            else
            {
                rightNoteSpawner.SpawnSingleNote();
                rightNextIndex++;
            }
        }

    }

    public void RecordPosition(string name) {
        print(name);

        if (name == "ActivatorUpArrow")
        {
            upRecord.setText(songPosInBeats - 1f);
        }
        else if (name == "ActivatorLeftArrow")
        {
            leftRecord.setText(songPosInBeats - 1f);
        }
        else {
            rightRecord.setText(songPosInBeats - 1f);
        }
    }
}
