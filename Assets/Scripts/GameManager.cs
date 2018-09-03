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

    private NoteSpawner upNoteSpawner;
    private NoteSpawner leftNoteSpawner;
    private NoteSpawner rightNoteSpawner;

    int bpm = 30;

    float songPosition;
    float songPosInBeats;
    float secPerBeat;
    float dsptimesong;
    float fixX = -146f;

    float[] notes2 = { 2.6f, 3.6f, 4.5f, 5.4f, 6.3f, 7.3f, 8.2f, 9.2f, 10.1f, 11f, 12f, 12.9f, 13.8f, 14.8f, 15.7f, 16.7f, 16.9f, 17.6f, 17.8f, 18.5f, 18.8f, 19.5f, 19.7f, 20.7f, 21.1f, 21.6f, 22f, 22.5f, 23f, 23.2f, 24.2f, 24.6f, 25.1f, 25.6f, 26.1f, 26.5f, 27f, 27.5f, 27.9f, 28.4f, 28.9f, 29.1f, 29.8f, 30.3f, 30.7f, 31.7f, 32.6f, 33.5f, 34.5f, 35.4f, 39.1f, 39.6f, 40.1f, 40.6f, 41f, 41.5f, 42f, 42.5f, 42.9f, 43.8f, 44.8f, 45.7f };
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
        comboText.text = maxCombo.ToString();

        songPosition = (float)AudioSettings.dspTime + 2f - dsptimesong;
        songPosInBeats = (float)Math.Round((songPosition / secPerBeat), 1);

        if (songPosInBeats == notes2[index])
        {
            SpawnNote();
            Debug.Log(index + " beat!");
            index++;
        }
        
    }

    void SpawnNote() {
        int number = UnityEngine.Random.Range(0, 3);
        if (number == 1)
        {
            leftNoteSpawner.SpawnSingleNote();
        }
        else if (number == 2)
        {
            upNoteSpawner.SpawnSingleNote();
        }
        else {
            rightNoteSpawner.SpawnSingleNote();
        }
    }
}
