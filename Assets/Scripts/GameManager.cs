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
    private Slider healthBar;
    private Image healthBarFill;
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

    float[] l1UpNotes = {14.05f, 16.25f, 24.4f, 26.2f };
    float[] l1LeftNotes = { 3.5f, 5.7f, 8f, 9.1f, 10.2f, 11.3f, 12.4f, 13.5f, 14.6f, 15.7f, 16.8f, 17.9f, 18.95f, 19.8f, 20.8f, 21.7f, 22.6f, 23.5f, 24.4f, 25.3f, 26.2f, 27.1f, 28f, 28.9f, 29.8f, 30.7f, 31.6f };
    float[] l1RightNotes = { 4.6f, 6.8f, 8.55f, 9.65f, 10.75f, 11.85f, 12.95f, 14.05f, 15.15f, 16.25f, 17.35f, 18.40f, 19.45f, 20.35f, 21.25f, 22.15f, 23.05f, 23.95f, 24.85f, 25.75f, 26.65f, 27.55f, 28.45f, 29.35f, 30.25f, 31.15f, 32.05f };
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

        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBarFill = GameObject.Find("HealthBarFill").GetComponent<Image>();

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

        // handle Combo Number
        if (maxCombo <= comboCount)
        {
            maxCombo = comboCount;
        }
        comboText.text = comboCount.ToString();

        // Handle Player Health
        healthBar.value = character.characterHealth / character.characterMaxHealth;
        if (healthBar.value > 0.3)
        {
            healthBarFill.color = Color.green;
        }
        else {
            healthBarFill.color = Color.red;
        }


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
