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
    private Animator comboAnimator;

    int bpm = 30;

    float songPosition;
    public float songPosInBeats;
    float secPerBeat;
    float dsptimesong;

    int upNextIndex = 0;
    int leftNextIndex = 0;
    int rightNextIndex = 0;

    float[] l1UpNotesList = {14.05f, 16.25f, 24.4f, 26.2f };
    float[] l1LeftNotesList = { 3.5f, 5.7f, 8f, 9.1f, 10.2f, 11.3f, 12.4f, 13.5f, 14.6f, 15.7f, 16.8f, 17.9f, 18.95f, 19.9f, 20.8f, 21.7f, 22.6f, 23.5f, 24.4f, 25.3f, 26.2f, 27.1f, 28f, 28.9f, 29.8f, 30.7f, 31.6f, 32.5f, 33.4f, 34.3f };
    float[] l1RightNotesList = { 4.6f, 6.8f, 8.55f, 9.65f, 10.75f, 11.85f, 12.95f, 14.05f, 15.15f, 16.25f, 17.35f, 18.45f, 19.40f, 20.35f, 21.25f, 22.15f, 23.05f, 23.95f, 24.85f, 25.75f, 26.65f, 27.55f, 28.45f, 29.35f, 30.25f, 31.15f, 32.05f, 32.95f, 33.85f, 34.75f };

    float[] l2UpNotesList = { 6.52f, 7.1f, 7.63f, 8.17f, 10.95f, 11.51f, 12.07f, 12.61f, 15.41f, 15.97f, 16.5f, 17.06f, 17.34f, 18.50f, 19.6f, 22.37f, 23.47f, 24.57f, 25.71f, 26.81f, 27.91f, 29.05f, 30.15f, 31.12f, 31.96f, 32.77f, 33.6f, 34.46f, 35.29f, 36.11f, 36.96f, 37.78f, 38.62f, 39.48f, 40.3f, 41.11f, 41.94f, 42.78f, 43.64f, 44.03f, 44.89f, 45.69f, 46.5f, 47.37f, 48.19f, 49.04f, 49.89f, 50.7f, 51.53f, 52.78f, 54.47f, 55.78f, 57.19f };
    float[] l2LeftNotesList = { 4.01f, 4.59f, 5.14f, 5.7f, 6.23f, 6.81f, 7.35f, 7.92f, 8.45f, 9.03f, 9.56f, 10.15f, 10.66f, 11.23f, 11.8f, 12.34f, 12.91f, 13.46f, 14.02f, 14.56f, 15.13f, 15.68f, 16.23f, 16.77f, 17.34f, 18.50f, 19.6f, 20.7f, 21.81f, 24.03f, 26.25f, 28.47f, 30.7f, 32.37f, 34.04f, 35.7f, 37.37f, 39.05f, 40.72f, 42.35f, 44.46f, 46.09f, 47.8f, 49.45f, 51.13f, 54.9f, 56.25f, 57.68f };
    float[] l2RightNotesList = { 4.32f, 4.88f, 5.42f, 5.97f, 8.75f, 9.28f, 9.85f, 10.41f, 13.19f, 13.75f, 14.31f, 14.85f, 17.34f, 19.6f, 20.7f, 22.94f, 25.17f, 27.38f, 29.59f, 31.55f, 33.19f, 34.86f, 36.55f, 38.21f, 39.87f, 41.53f, 43.22f, 45.29f, 48.64f, 51.96f, 52.32f, 53.2f, 54.03f, 55.32f, 56.72f };

    float[] l3UpNotesList = { 5.68f, 5.95f, 6.33f, 7.17f, 7.45f, 7.83f, 8.66f, 8.95f, 9.34f, 10.17f, 10.47f, 10.83f, 11.67f, 11.96f, 12.33f, 13.17f, 13.45f, 13.84f, 14.66f, 14.97f, 15.32f, 16.15f, 16.45f, 16.82f, 17.67f, 17.94f, 18.31f, 19.17f, 19.46f, 19.82f, 20.66f, 20.95f, 21.33f, 22.17f, 22.47f, 22.83f, 23.49f, 23.67f, 23.85f, 24.05f, 25.74f, 25.92f, 26.11f, 26.29f, 26.49f, 26.68f, 26.86f, 27.05f, 31.75f, 31.94f, 32.13f, 32.32f, 34.74f, 34.94f, 35.12f, 35.31f, 37.73f, 37.92f, 38.11f, 38.3f, 40.74f, 40.92f, 41.1f, 41.29f, 43.31f, 44.28f, 45.11f, 46.34f, 47.31f, 47.6f, 47.96f, 48.79f, 49.08f, 49.47f, 50.3f, 50.58f, 50.97f, 51.78f, 52.08f, 52.46f, 53.29f, 53.57f, 53.95f, 54.8f, 55.09f, 55.48f, 56.27f, 56.58f, 56.95f, 57.78f, 58.07f, 58.47f, 59.12f, 59.3f, 59.49f, 59.67f, 61.38f, 61.56f, 61.75f, 61.93f, 62.86f, 63.04f, 63.25f, 63.43f, 65.87f, 66.06f, 66.24f, 66.42f, 68.88f, 69.06f, 69.25f, 69.43f, 71.84f, 72.02f, 72.21f, 72.41f, 74.87f, 75.04f, 75.23f, 75.42f };
    float[] l3LeftNotesList = { 5.5f, 6.14f, 6.51f, 6.69f, 6.98f, 7.64f, 8.02f, 8.21f, 8.49f, 9.14f, 9.52f, 9.71f, 9.99f, 10.66f, 11.01f, 11.21f, 11.48f, 12.15f, 12.53f, 12.72f, 12.98f, 13.66f, 14.02f, 14.21f, 14.48f, 15.14f, 15.51f, 15.69f, 15.99f, 16.63f, 17.01f, 17.2f, 17.49f, 18.13f, 18.51f, 18.7f, 18.99f, 19.64f, 20.01f, 20.21f, 20.48f, 21.13f, 21.51f, 21.69f, 21.98f, 22.63f, 23.01f, 23.19f, 24.99f, 25.18f, 25.37f, 25.54f, 27.99f, 28.17f, 28.38f, 28.56f, 30.99f, 31.18f, 31.37f, 31.57f, 33.99f, 34.18f, 34.36f, 34.54f, 36.98f, 37.17f, 37.36f, 37.54f, 39.99f, 40.18f, 40.37f, 40.55f, 42.99f, 43.94f, 44.87f, 45.61f, 47.12f, 47.78f, 48.14f, 48.32f, 48.62f, 49.27f, 49.65f, 49.84f, 50.13f, 50.78f, 51.14f, 51.33f, 51.61f, 52.27f, 52.64f, 52.82f, 53.12f, 53.76f, 54.16f, 54.34f, 54.62f, 55.28f, 55.66f, 55.85f, 56.11f, 56.76f, 57.14f, 57.34f, 57.61f, 58.26f, 58.64f, 58.83f, 60.61f, 60.8f, 60.99f, 61.19f, 62.13f, 62.31f, 62.49f, 62.68f, 65.11f, 65.3f, 65.5f, 65.69f, 68.13f, 68.31f, 68.48f, 68.67f, 71.11f, 71.29f, 71.47f, 71.66f, 74.11f, 74.3f, 74.5f, 74.68f };
    float[] l3RightNotesList = { 5.76f, 7.26f, 8.77f, 10.27f, 11.77f, 13.26f, 14.75f, 16.25f, 17.75f, 19.27f, 20.77f, 22.27f, 24.23f, 24.42f, 24.61f, 24.79f, 27.23f, 27.42f, 27.61f, 27.81f, 28.74f, 28.93f, 29.1f, 29.29f, 29.5f, 32.49f, 32.7f, 32.89f, 33.08f, 33.26f, 33.42f, 33.61f, 33.81f, 35.5f, 35.68f, 35.87f, 36.07f, 36.25f, 36.41f, 36.6f, 36.79f, 38.49f, 38.68f, 38.86f, 39.03f, 39.23f, 39.43f, 39.61f, 39.8f, 41.49f, 41.65f, 41.86f, 42.03f, 42.23f, 43.61f, 44.62f, 45.36f, 47.41f, 48.89f, 50.39f, 51.9f, 53.38f, 54.91f, 56.4f, 57.88f, 59.87f, 60.06f, 60.25f, 60.43f, 63.61f, 63.8f, 63.99f, 64.18f, 64.37f, 64.56f, 64.73f, 64.91f, 66.62f, 66.8f, 66.98f, 67.19f, 67.36f, 67.55f, 67.75f, 67.93f, 69.62f, 69.83f, 70f, 70.18f, 70.37f, 70.55f, 70.74f, 70.91f, 72.59f, 72.78f, 72.98f, 73.18f, 73.37f, 73.55f, 73.74f, 73.92f, 75.61f, 75.79f, 75.96f, 76.16f, 76.35f, 76.55f, 76.73f, 76.92f };
   
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
        comboAnimator = GameObject.Find("ComboBar").GetComponent<Animator>();
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

            if (maxCombo % 10 == 0 && maxCombo != 0) {
                comboAnimator.SetTrigger("ComboTrigger");
            }
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
        songPosInBeats = (float)Math.Round((songPosition / secPerBeat), 3);
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
