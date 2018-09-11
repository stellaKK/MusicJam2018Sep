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

    int upNextIndex = 0;
    int leftNextIndex = 0;
    int rightNextIndex = 0;

    float[] l1UpNotesList = {14.05f, 16.25f, 24.4f, 26.2f };
    float[] l1LeftNotesList = { 3.5f, 5.7f, 8f, 9.1f, 10.2f, 11.3f, 12.4f, 13.5f, 14.6f, 15.7f, 16.8f, 17.9f, 18.95f, 19.8f, 20.8f, 21.7f, 22.6f, 23.5f, 24.4f, 25.3f, 26.2f, 27.1f, 28f, 28.9f, 29.8f, 30.7f, 31.6f };
    float[] l1RightNotesList = { 4.6f, 6.8f, 8.55f, 9.65f, 10.75f, 11.85f, 12.95f, 14.05f, 15.15f, 16.25f, 17.35f, 18.40f, 19.45f, 20.35f, 21.25f, 22.15f, 23.05f, 23.95f, 24.85f, 25.75f, 26.65f, 27.55f, 28.45f, 29.35f, 30.25f, 31.15f, 32.05f };

    float[] l2UpNotesList = { 6.52f, 7.1f, 7.63f, 8.17f, 10.95f, 11.51f, 12.07f, 12.61f, 15.41f, 15.97f, 16.5f, 17.06f, 17.34f, 18.50f, 19.6f, 22.37f, 23.47f, 24.57f, 25.71f, 26.81f, 27.91f, 29.05f, 30.15f, 31.12f, 31.96f, 32.77f, 33.6f, 34.46f, 35.29f, 36.11f, 36.96f, 37.78f, 38.62f, 39.48f, 40.3f, 41.11f, 41.94f, 42.78f, 43.64f, 44.03f, 44.89f, 45.69f, 46.5f, 47.37f, 48.19f, 49.04f, 49.89f, 50.7f, 51.53f, 52.78f, 54.47f, 55.78f, 57.19f };
    float[] l2LeftNotesList = { 4.01f, 4.59f, 5.14f, 5.7f, 6.23f, 6.81f, 7.35f, 7.92f, 8.45f, 9.03f, 9.56f, 10.15f, 10.66f, 11.23f, 11.8f, 12.34f, 12.91f, 13.46f, 14.02f, 14.56f, 15.13f, 15.68f, 16.23f, 16.77f, 17.34f, 18.50f, 19.6f, 20.7f, 21.81f, 24.03f, 26.25f, 28.47f, 30.7f, 32.37f, 34.04f, 35.7f, 37.37f, 39.05f, 40.72f, 42.35f, 44.46f, 46.09f, 47.8f, 49.45f, 51.13f, 54.9f, 56.25f, 57.68f };
    float[] l2RightNotesList = { 4.32f, 4.88f, 5.42f, 5.97f, 8.75f, 9.28f, 9.85f, 10.41f, 13.19f, 13.75f, 14.31f, 14.85f, 17.34f, 19.6f, 20.7f, 22.94f, 25.17f, 27.38f, 29.59f, 31.55f, 33.19f, 34.86f, 36.55f, 38.21f, 39.87f, 41.53f, 43.22f, 45.29f, 48.64f, 51.96f, 52.32f, 53.2f, 54.03f, 55.32f, 56.72f };

    float[] l3UpNotesList = { 5.7f, 6f, 6.4f, 7.2f, 7.5f, 7.8f, 8.7f, 9f, 9.3f, 10.2f, 10.5f, 10.9f, 11.7f, 12f, 12.4f, 13.2f, 13.5f, 13.8f, 14.7f, 15f, 15.3f, 16.2f, 16.5f, 16.8f, 17.7f, 18f, 18.3f, 19.2f, 19.5f, 19.9f, 20.7f, 21f, 21.4f, 22.2f, 22.5f, 22.9f, 23.7f, 24.1f, 24.5f, 24.8f, 25.2f, 25.6f, 25.9f, 26.3f, 26.7f, 27.1f, 27.4f, 27.8f, 28.2f, 28.6f, 28.9f, 29.3f, 29.9f, 31.2f, 31.6f, 32f, 32.3f, 32.7f, 33.1f, 33.4f, 33.8f, 34.2f, 34.6f, 34.9f, 35.3f, 35.7f, 36.1f, 36.4f, 36.8f, 37.2f, 37.6f, 37.9f, 38.3f, 38.6f, 38.8f, 39f, 39.2f, 39.3f, 39.5f, 39.7f, 39.9f, 40.2f, 40.6f, 40.9f, 41.3f, 41.6f, 41.8f, 42f, 42.2f, 42.3f, 42.5f, 42.7f, 42.9f, 43.4f, 44f, 44.7f, 46f, 46.7f, 47.5f, 48.2f, 49f, 49.7f, 50.5f, 51.3f, 52f, 52.7f, 53.5f, 54.3f, 55f, 55.7f, 56.5f, 57.3f, 58f, 58.7f, 62.5f, 63.3f, 63.9f, 64.6f, 65f, 65.8f, 66f, 66.1f, 66.4f, 67.3f, 67.5f, 67.7f, 67.9f, 68.8f, 69f, 69.2f, 69.4f, 71.5f, 72.2f, 73f, 73.7f, 74.5f, 75.3f, 75.9f, 76.5f, 77f };
    float[] l3LeftNotesList = { 5.5f, 6.2f, 6.6f, 6.7f, 7f, 7.7f, 8f, 8.2f, 8.5f, 9.1f, 9.5f, 9.7f, 10f, 10.7f, 11f, 11.2f, 11.8f, 13.3f, 14.8f, 16.3f, 17.5f, 18.2f, 18.5f, 18.7f, 19f, 19.7f, 20.1f, 20.2f, 20.5f, 21.2f, 21.5f, 21.7f, 22f, 22.7f, 23f, 23.2f, 23.5f, 24.3f, 25f, 25.8f, 26.5f, 27.3f, 28f, 28.8f, 29.5f, 31f, 31.8f, 32.5f, 33.3f, 34.4f, 35.1f, 35.9f, 36.6f, 37f, 37.8f, 38.5f, 38.9f, 39.3f, 39.6f, 40f, 40.7f, 41.5f, 41.9f, 42.3f, 42.6f, 43f, 44.3f, 45.6f, 47.1f, 48.6f, 50.1f, 51.6f, 53.9f, 55.4f, 57f, 58.4f, 59.1f, 60f, 60.4f, 60.6f, 61.5f, 61.9f, 62.1f, 63.1f, 63.4f, 63.6f, 64.2f, 64.8f, 65.1f, 65.5f, 66.6f, 66.9f, 68.5f, 69.2f, 70f, 70.7f, 71.1f, 72.6f, 74.1f, 75.6f, 76.8f, 77.1f };
    float[] l3RightNotesList = { 5.8f, 7.3f, 8.8f, 10.3f, 11.5f, 12.2f, 12.5f, 12.7f, 13f, 13.7f, 14f, 14.2f, 14.5f, 15.1f, 15.5f, 15.7f, 16f, 16.7f, 17f, 17.2f, 17.8f, 19.3f, 20.8f, 22.3f, 23.9f, 24.6f, 25.4f, 26.1f, 26.9f, 27.6f, 28.4f, 29.1f, 30.3f, 31.4f, 32.2f, 32.9f, 33.6f, 34f, 34.8f, 35.5f, 36.3f, 37.4f, 38.1f, 38.7f, 39.1f, 39.4f, 39.8f, 40.4f, 41.1f, 41.7f, 42.1f, 42.4f, 42.8f, 43.7f, 45f, 45.3f, 45.5f, 46.4f, 47.9f, 49.4f, 50.9f, 52.4f, 53.1f, 54.6f, 56.1f, 57.6f, 59.5f, 60.2f, 61f, 61.8f, 64.4f, 65.5f, 66.2f, 67f, 67.7f, 68.1f, 68.4f, 69.6f, 69.9f, 70.2f, 70.5f, 70.6f, 70.9f, 71.9f, 73.4f, 74.9f, 76.2f };



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
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
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
        maxCombo = 0;
    }

    // Update is called once per frame
    void Update() {
        // if health point reaches 0 then make the game fail
        if (character.characterHealth <= 0)
        {
            PlayerPrefs.SetInt("Fail", 1);
            PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetString("SongName", music.clip.name);
            PlayerPrefs.SetInt("Combo", maxCombo);
            SceneManager.LoadScene("Result");
        }


        if (!music.isPlaying) {
            PlayerPrefs.SetInt("Fail", 0);
            PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetString("SongName", music.clip.name);
            PlayerPrefs.SetInt("Combo", maxCombo);

            SceneManager.LoadScene("Result");
        }

        // handle Combo Number
        if (maxCombo <= comboCount)
        {
            maxCombo = comboCount;
        }
        comboText.text = comboCount.ToString();

        // Handle Player Health
        healthText.text = character.characterHealth.ToString() + "/"
            + character.characterMaxHealth.ToString();
        healthBar.value = character.characterHealth / character.characterMaxHealth;
        if (healthBar.value > 0.3)
        {
            healthBarFill.color = Color.green;
        }
        else {
            healthBarFill.color = Color.red;
        }


        songPosition = (float)AudioSettings.dspTime - dsptimesong;
        songPosInBeats = (float)Math.Round((songPosition / secPerBeat), 2);
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
