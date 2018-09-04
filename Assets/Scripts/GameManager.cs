﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

    public int comboCount = 0;
    public int maxCombo = -1;

    private Character character;
    private float characterHealth;
    private Text healthText;
    private Text comboText;

    Recorder upRecord;
    Recorder leftRecord;
    Recorder rightRecord;

    private NoteSpawner upNoteSpawner;
    private NoteSpawner leftNoteSpawner;
    private NoteSpawner rightNoteSpawner;

    int bpm = 30;

    float songPosition;
    float songPosInBeats;
    float secPerBeat;
    float dsptimesong;

    int upIndex = 0;
    int leftIndex = 0;
    int rightIndex = 0;
    int upNextIndex = 0;
    int leftNextIndex = 0;
    int rightNextIndex = 0;

<<<<<<< HEAD
    float[] upNotes = {2.5f, 4.3f, 7.2f, 8.1f, 9f, 9.9f, 10.9f, 15.6f, 17.5f, 18.4f, 19.4f, 20.3f, 21f, 21.2f, 22.2f, 23.1f, 24f, 24.7f, 25f, 25.9f, 26.8f, 27.7f, 28.5f, 28.7f};
    float[] leftNotes = {3.4f, 5.3f, 11.8f, 13.7f, 15.8f, 17.2f, 18.3f, 19.2f, 20.1f, 22f, 22.9f, 23.8f, 25.7f, 26.6f, 27.5f};
    float[] rightNotes = { 1.6f, 6.2f, 12.8f, 14.6f, 16.5f, 16.8f, 18.6f, 19.6f, 20.5f, 22.4f, 23.3f, 24.2f, 26.1f, 27f, 28f};
=======
    float[] notes = { 2.6f, 3.6f, 4.5f, 5.4f, 6.3f, 7.3f, 8.2f, 9.2f, 10.1f, 11f, 12f, 12.9f, 13.8f,
        14.8f, 15.7f, 16.7f, 16.9f, 17.6f, 17.8f, 18.5f, 18.8f, 19.5f, 19.7f, 20.7f, 21.1f, 21.6f, 22f,
        22.5f, 23f, 23.2f, 24.2f, 24.6f, 25.1f, 25.6f, 26.1f, 26.5f, 27f, 27.5f, 27.9f, 28.4f, 28.9f,
        29.1f, 29.8f, 30.3f, 30.7f, 31.7f, 32.6f, 33.5f, 34.5f, 35.4f, 39.1f, 39.6f, 40.1f, 40.6f, 41f,
        41.5f, 42f, 42.5f, 42.9f, 43.8f, 44.8f, 45.7f };
>>>>>>> 8dbbed43557118ea8d136b1f1463dc345a94ed21

    int index = 0;


    // Use this for initialization
    void Start() {
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

        secPerBeat = 60f / bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        Debug.Log("secPerBeat: " + secPerBeat);
        Debug.Log("dsptimesong: " + dsptimesong);
    }

    // Update is called once per frame
    void Update() {
        if (maxCombo <= comboCount)
        {
            maxCombo = comboCount;
        }

        healthText.text = character.characterHealth.ToString();
        comboText.text = comboCount.ToString();

        songPosition = (float)AudioSettings.dspTime - dsptimesong;
        songPosInBeats = (float)Math.Round((songPosition / secPerBeat), 1);

<<<<<<< HEAD
=======
        if (songPosInBeats == notes[index]) {
            SpawnNote();
            if (index + 1 < notes.Length)  {
                index++;
            }
        }
        
    }
>>>>>>> 8dbbed43557118ea8d136b1f1463dc345a94ed21

        if (upNextIndex < upNotes.Length && upNotes[upNextIndex] < songPosInBeats)
        {
            upNoteSpawner.SpawnDoubleNote();
            upNextIndex++;
        }
        if (leftNextIndex < leftNotes.Length && leftNotes[leftNextIndex] < songPosInBeats)
        {
            leftNoteSpawner.SpawnSingleNote();
            leftNextIndex++;
        }
        if (rightNextIndex < rightNotes.Length && rightNotes[rightNextIndex] < songPosInBeats)
        {
            rightNoteSpawner.SpawnSingleNote();
            rightNextIndex++;
        }

    }

    public void RecordPosition(string name) {
        print(name);

        if (name == "ActivatorUpArrow")
        {
            upRecord.setText(songPosInBeats);
        }
        else if (name == "ActivatorLeftArrow")
        {
            leftRecord.setText(songPosInBeats);
        }
        else {
            rightRecord.setText(songPosInBeats);
        }
    }
}
