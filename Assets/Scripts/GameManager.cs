using System.Collections;
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
    public float songPosInBeats;
    float secPerBeat;
    float dsptimesong;

    int upIndex = 0;
    int leftIndex = 0;
    int rightIndex = 0;
    int upNextIndex = 0;
    int leftNextIndex = 0;
    int rightNextIndex = 0;

    float[] upNotes = { 15.9f, 16.6f, 18.3f, 18.5f, 19.4f, 20.4f, 23.2f, 24.1f, 25f, 27.1f, 27.6f, 28.6f, 29.7f, 30.7f, 31.2f, 31.7f, 32.1f, 32.6f, 33.1f, 33.5f};
    float[] leftNotes = { 1.6f, 2.5f, 3.2f, 4.2f, 5.1f, 6.1f, 7f, 7.9f, 8.9f, 9.8f, 10.8f, 11.7f, 12.7f, 13.6f, 14.5f, 15.5f, 15.7f, 16.4f, 17.3f, 17.8f, 19.2f, 20.2f, 21.1f, 21.5f, 22.1f, 22.1f, 23.1f, 23.8f, 24.8f, 25.7f, 26.7f, 27.9f, 28.8f, 29.5f};
    float[] rightNotes = { 16.8f, 17.5f, 19.6f, 20.6f, 21.3f, 22.2f, 23.4f, 24.3f, 25.3f, 25.9f, 26.9f, 28.1f, 29f};

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


        if (upNextIndex < upNotes.Length && upNotes[upNextIndex] < songPosInBeats)
        {
            upNoteSpawner.SpawnSingleNote();
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
