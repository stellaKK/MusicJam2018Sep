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

    float[] l1UpNotesList = {14.05f, 16.25f, 24.4f, 26.2f };
    float[] l1LeftNotesList = { 3.5f, 5.7f, 8f, 9.1f, 10.2f, 11.3f, 12.4f, 13.5f, 14.6f, 15.7f, 16.8f, 17.9f, 18.95f, 19.8f, 20.8f, 21.7f, 22.6f, 23.5f, 24.4f, 25.3f, 26.2f, 27.1f, 28f, 28.9f, 29.8f, 30.7f, 31.6f };
    float[] l1RightNotesList = { 4.6f, 6.8f, 8.55f, 9.65f, 10.75f, 11.85f, 12.95f, 14.05f, 15.15f, 16.25f, 17.35f, 18.40f, 19.45f, 20.35f, 21.25f, 22.15f, 23.05f, 23.95f, 24.85f, 25.75f, 26.65f, 27.55f, 28.45f, 29.35f, 30.25f, 31.15f, 32.05f };

    float[] l2UpNotesList = { 6.5f, 7.1f, 7.6f, 8.2f, 11f, 11.5f, 12.1f, 12.6f, 15.4f, 16f, 16.5f, 17.1f, 19.9f, 20.4f, 21f, 21.5f, 27.4f, 29f, 30.4f, 31.6f, 32.4f, 34.9f, 35.8f, 38.2f, 39.1f, 40.9f, 41.3f, 41.7f, 41.9f, 44.9f, 45.7f, 46.5f, 47.4f, 48.2f, 49.1f, 49.9f, 50.6f, 50.7f, 51.5f, 52.8f, 54.4f, 55.8f, 57.2f };
    float[] l2LeftNotesList = { 4f, 4.6f, 5.1f, 5.7f, 6.3f, 6.8f, 7.4f, 7.9f, 8.5f, 9f, 9.6f, 10.1f, 10.7f, 11.2f, 11.8f, 12.4f, 12.9f, 13.6f, 14f, 14.7f, 15.1f, 15.8f, 16.2f, 16.9f, 17.6f, 18.2f, 18.8f, 19.3f, 21.8f, 22f, 22.2f, 22.3f, 23.5f, 24f, 24.3f, 24.4f, 24.5f, 25.7f, 26.3f, 27.9f, 29.6f, 30.6f, 30.7f, 32.4f, 32.5f, 34.4f, 35.7f, 35.9f, 37.7f, 39f, 41f, 41.4f, 41.8f, 42.2f, 44.5f, 46.1f, 47.8f, 49.4f, 50.4f, 51.1f, 52.4f, 54.9f, 56.2f, 57.7f };
    float[] l2RightNotesList = { 4.3f, 4.9f, 5.4f, 6f, 8.7f, 9.3f, 9.9f, 10.4f, 13.2f, 13.8f, 14.3f, 14.9f, 17.3f, 18.1f, 18.5f, 19.2f, 19.6f, 20.3f, 20.7f, 21.4f, 22.4f, 22.9f, 23.1f, 23.3f, 23.4f, 24.6f, 25.1f, 25.4f, 25.5f, 25.6f, 26.8f, 28.5f, 30.2f, 31.1f, 32f, 35.3f, 38.6f, 39.2f, 40.7f, 41.1f, 41.5f, 42.1f, 42.3f, 44f, 45.3f, 48.6f, 51.9f, 53.2f, 54f, 55.3f, 56.7f };

    float[] l3UpNotesList = { };
    float[] l3LeftNotesList = { };
    float[] l3RightNotesList = { };

    private bool upEnd = false;
    private bool leftEnd = false;
    private bool rightEnd = false;

    int index = 0;

    // Use this for initialization
    void Start() {
        PlayerPrefs.SetInt("L1Notes", l1UpNotesList.Length + l1LeftNotesList.Length + l1RightNotesList.Length);
        PlayerPrefs.SetInt("L2Notes", l1UpNotesList.Length + l1LeftNotesList.Length + l1RightNotesList.Length);
        PlayerPrefs.SetInt("L3Notes", l1UpNotesList.Length + l1LeftNotesList.Length + l1RightNotesList.Length);

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

        upIndex = 0;
        leftIndex = 0;
        rightIndex = 0;
        upNextIndex = 0;
        leftNextIndex = 0;
        rightNextIndex = 0;
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
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SpawningNotes(l1UpNotesList, l1LeftNotesList, l1RightNotesList);
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SpawningNotes(l2UpNotesList, l2LeftNotesList, l2RightNotesList);
        }
        else if (SceneManager.GetActiveScene().name == "Level3"){
            SpawningNotes(l3UpNotesList, l3LeftNotesList, l3RightNotesList);
        }

        

    }

    void SpawningNotes(float[] upNotes, float[] leftNotes, float[] rightNotes) {
        if (upNextIndex >= upNotes.Length) {
            upEnd = true;
        }
        if (leftNextIndex >= leftNotes.Length) {
            leftEnd = true;
        }
        if (rightIndex >= rightNotes.Length) {
            rightEnd = true;
        }

        if (upNextIndex < upNotes.Length && upNotes[upNextIndex] < songPosInBeats)
        {
            if (!leftEnd || !rightEnd)
            {
                if (!leftEnd && upNotes[upNextIndex] == leftNotes[leftNextIndex])
                {
                    upNoteSpawner.SpawnDoubleNote();
                    upNextIndex++;
                    leftNoteSpawner.SpawnDoubleNote();
                    leftNextIndex++;
                }
                else if (!rightEnd && upNotes[upNextIndex] == rightNotes[rightNextIndex])
                {
                    upNoteSpawner.SpawnDoubleNote();
                    upNextIndex++;
                    rightNoteSpawner.SpawnDoubleNote();
                    rightNextIndex++;
                }
                else
                {
                    upNoteSpawner.SpawnSingleNote();
                    upNextIndex++;
                }
            }
            else
            {
                upNoteSpawner.SpawnSingleNote();
                upNextIndex++;
            }
        }
        if (leftNextIndex < leftNotes.Length && leftNotes[leftNextIndex] < songPosInBeats)
        {
            if (upNextIndex < upNotes.Length || rightNextIndex < rightNotes.Length)
            {
                if (!upEnd && leftNotes[leftNextIndex] == upNotes[upNextIndex])
                {
                    leftNoteSpawner.SpawnDoubleNote();
                    leftNextIndex++;
                    upNoteSpawner.SpawnDoubleNote();
                    upNextIndex++;
                }
                else if (!rightEnd && leftNotes[upNextIndex] == rightNotes[rightNextIndex])
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
            else
            {
                leftNoteSpawner.SpawnSingleNote();
                leftNextIndex++;
            }


        }
        if (rightNextIndex < rightNotes.Length && rightNotes[rightNextIndex] < songPosInBeats)
        {
            if (upNextIndex < upNotes.Length || leftNextIndex < leftNotes.Length)
            {
                if (!upEnd && rightNotes[leftNextIndex] == upNotes[upNextIndex])
                {
                    rightNoteSpawner.SpawnDoubleNote();
                    rightNextIndex++;
                    upNoteSpawner.SpawnDoubleNote();
                    upNextIndex++;
                }
                else if (!leftEnd && rightNotes[rightNextIndex] == leftNotes[upNextIndex])
                {
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
