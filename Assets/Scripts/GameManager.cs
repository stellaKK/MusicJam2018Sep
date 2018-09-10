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

    int upNextIndex = 0;
    int leftNextIndex = 0;
    int rightNextIndex = 0;

    float[] l1UpNotesList = {14.05f, 16.25f, 24.4f, 26.2f };
    float[] l1LeftNotesList = { 3.5f, 5.7f, 8f, 9.1f, 10.2f, 11.3f, 12.4f, 13.5f, 14.6f, 15.7f, 16.8f, 17.9f, 18.95f, 19.8f, 20.8f, 21.7f, 22.6f, 23.5f, 24.4f, 25.3f, 26.2f, 27.1f, 28f, 28.9f, 29.8f, 30.7f, 31.6f };
    float[] l1RightNotesList = { 4.6f, 6.8f, 8.55f, 9.65f, 10.75f, 11.85f, 12.95f, 14.05f, 15.15f, 16.25f, 17.35f, 18.40f, 19.45f, 20.35f, 21.25f, 22.15f, 23.05f, 23.95f, 24.85f, 25.75f, 26.65f, 27.55f, 28.45f, 29.35f, 30.25f, 31.15f, 32.05f };

    float[] l2UpNotesList = { 6.5f, 7.1f, 7.6f, 8.2f, 11f, 11.5f, 12.1f, 12.6f, 15.4f, 16f, 16.5f, 17.1f, 19.9f, 20.4f, 21f, 21.5f, 27.4f, 29f, 30.4f, 31.6f, 32.4f, 34.9f, 35.8f, 38.2f, 39.1f, 40.9f, 41.3f, 41.7f, 41.9f, 44.9f, 45.7f, 46.5f, 47.4f, 48.2f, 49.1f, 49.9f, 50.6f, 50.7f, 51.5f, 52.8f, 54.4f, 55.8f, 57.2f };
    float[] l2LeftNotesList = { 4f, 4.6f, 5.1f, 5.7f, 6.3f, 6.8f, 7.4f, 7.9f, 8.5f, 9f, 9.6f, 10.1f, 10.7f, 11.2f, 11.8f, 12.4f, 12.9f, 13.6f, 14f, 14.7f, 15.1f, 15.8f, 16.2f, 16.9f, 17.6f, 18.2f, 18.8f, 19.3f, 21.8f, 22f, 22.2f, 22.3f, 23.5f, 24f, 24.3f, 24.4f, 24.5f, 25.7f, 26.3f, 27.9f, 29.6f, 30.6f, 30.7f, 32.4f, 32.5f, 34.4f, 35.7f, 35.9f, 37.7f, 39f, 41f, 41.4f, 41.8f, 42.2f, 44.5f, 46.1f, 47.8f, 49.4f, 50.4f, 51.1f, 52.4f, 54.9f, 56.2f, 57.7f };
    float[] l2RightNotesList = { 4.3f, 4.9f, 5.4f, 6f, 8.7f, 9.3f, 9.9f, 10.4f, 13.2f, 13.8f, 14.3f, 14.9f, 17.3f, 18.1f, 18.5f, 19.2f, 19.6f, 20.3f, 20.7f, 21.4f, 22.4f, 22.9f, 23.1f, 23.3f, 23.4f, 24.6f, 25.1f, 25.4f, 25.5f, 25.6f, 26.8f, 28.5f, 30.2f, 31.1f, 32f, 35.3f, 38.6f, 39.2f, 40.7f, 41.1f, 41.5f, 42.1f, 42.3f, 44f, 45.3f, 48.6f, 51.9f, 53.2f, 54f, 55.3f, 56.7f };

    float[] l3UpNotesList = { 8.5f, 9f, 9.4f, 9.9f, 10.3f, 10.8f, 11.3f, 11.8f, 12.2f, 12.7f, 13.2f, 13.6f, 14.1f, 14.6f, 15f, 15.5f, 15.8f, 16.5f, 18.5f, 19.5f, 20.2f, 21.1f, 22.3f, 23.3f, 23.9f, 26.1f, 27f, 27.7f, 28.6f, 29.8f, 30.3f, 30.7f, 31.2f, 31.7f, 32.1f, 32.6f, 33.1f, 33.5f, 34f, 34.5f, 34.6f, 34.7f, 34.9f, 35f, 35.1f, 35.2f, 35.3f, 35.5f, 35.6f, 35.7f, 35.8f, 35.9f, 36f, 36.1f, 36.3f, 36.4f, 36.5f, 36.6f, 36.7f, 36.9f, 37f, 37.1f, 37.2f, 37.3f, 37.4f, 37.5f, 37.7f, 37.8f, 37.9f, 38f, 38.5f, 38.9f, 39.4f, 39.9f, 40.4f, 40.8f, 41.3f, 41.8f, 42.2f, 42.7f, 43.2f, 43.6f, 44.1f, 44.6f, 45f, 45.5f, 53.4f, 54.1f, 55.1f, 55.4f, 56f, 56.3f, 57f, 57.2f, 57.9f, 58.2f, 58.9f, 59.1f, 59.8f, 60f };
    float[] l3LeftNotesList = { 0.9f, 1.2f, 1.7f, 2.2f, 2.6f, 3.1f, 3.6f, 4.1f, 4.5f, 5f, 5.4f, 5.9f, 6.4f, 6.9f, 7.3f, 7.8f, 8.2f, 9.2f, 10.1f, 11.1f, 12f, 13f, 13.9f, 14.8f, 16f, 16.9f, 17.8f, 19.3f, 19.8f, 20.4f, 21.6f, 23f, 23.5f, 24.4f, 25.4f, 26.8f, 27.3f, 27.9f, 29.1f, 38.2f, 39.2f, 40.1f, 41.1f, 42f, 42.9f, 43.9f, 44.8f, 45.7f, 45.9f, 46.2f, 46.3f, 46.7f, 46.8f, 47.2f, 47.3f, 47.6f, 47.7f, 48.1f, 48.2f, 48.6f, 48.7f, 49f, 49.1f, 49.5f, 49.6f, 50f, 50.1f, 50.4f, 50.6f, 50.9f, 51f, 51.4f, 51.5f, 51.8f, 52f, 52.3f, 52.4f, 52.8f, 52.9f, 54.4f, 54.6f, 54.9f, 56.5f, 56.7f, 58.4f, 58.6f, 60.3f, 60.5f };
    float[] l3RightNotesList = { 1f, 1.5f, 1.9f, 2.4f, 2.9f, 3.3f, 3.8f, 4.3f, 4.7f, 5.2f, 5.7f, 6.1f, 6.6f, 7.1f, 7.5f, 8f, 8.7f, 9.7f, 10.6f, 11.5f, 12.5f, 13.4f, 14.3f, 15.3f, 16.7f, 17.4f, 17.6f, 18.3f, 20.7f, 21.4f, 22.1f, 24.2f, 24.9f, 25.1f, 25.8f, 28.2f, 28.9f, 29.6f, 38.7f, 39.7f, 40.6f, 41.5f, 42.5f, 43.4f, 44.3f, 45.3f, 46f, 46.1f, 46.5f, 46.6f, 46.9f, 47f, 47.4f, 47.5f, 47.9f, 48f, 48.3f, 48.4f, 48.8f, 48.9f, 49.3f, 49.4f, 49.7f, 49.8f, 50.2f, 50.3f, 50.7f, 50.8f, 51.1f, 51.2f, 51.6f, 51.7f, 52.1f, 52.2f, 52.5f, 52.6f, 53f, 53.1f, 53.6f, 53.8f, 54f, 55.6f, 55.8f, 57.5f, 57.7f, 59.4f, 59.6f };


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
        //print("Right :" + rightNextIndex + " , " + rightNotes.Length);
        //print("Up :" + upNextIndex + " , " + upNotes.Length);
        //print("Left :" + leftNextIndex + " , " +leftNotes.Length);

        if (upNextIndex >= upNotes.Length) {
            upEnd = true;
            print("up ended.");
        }
        if (leftNextIndex >= leftNotes.Length) {
            leftEnd = true;
            print("left ended.");
        }
        if (rightNextIndex >= rightNotes.Length) {
            rightEnd = true;
            print("right ended.");
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
                else if (!rightEnd && leftNotes[leftNextIndex] == rightNotes[rightNextIndex])
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
                if (!upEnd)
                {
                    if (rightNotes[rightNextIndex] == upNotes[upNextIndex])
                    {
                        rightNoteSpawner.SpawnDoubleNote();
                        rightNextIndex++;
                        upNoteSpawner.SpawnDoubleNote();
                        upNextIndex++;
                    } else {
                         rightNoteSpawner.SpawnSingleNote();
                         rightNextIndex++;
                    }
                }
                else if (!leftEnd)
                {
                    if (rightNotes[rightNextIndex] == leftNotes[leftNextIndex])
                    {
                        rightNoteSpawner.SpawnDoubleNote();
                        rightNextIndex++;
                        leftNoteSpawner.SpawnDoubleNote();
                        leftNextIndex++;
                    } else {
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
