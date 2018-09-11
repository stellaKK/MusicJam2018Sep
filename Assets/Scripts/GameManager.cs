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

    float[] l3UpNotesList = { 5.88f, 6.64f, 7.38f, 8.13f, 8.86f, 9.62f, 10.36f, 11.14f, 11.69f, 12.08f, 12.43f, 12.83f, 13.21f, 13.59f, 13.94f, 14.33f, 14.67f, 15.05f, 15.44f, 15.81f, 16.18f, 16.57f, 16.94f, 17.31f, 17.68f, 18.07f, 18.45f, 18.81f, 19.19f, 19.58f, 19.96f, 20.33f, 20.71f, 21.09f, 21.45f, 21.82f, 22.2f, 22.57f, 22.94f, 23.29f, 23.51f, 23.69f, 23.89f, 24.08f, 25.74f, 25.92f, 26.14f, 26.32f, 26.51f, 26.7f, 26.88f, 27.06f, 31.76f, 31.95f, 32.13f, 32.32f, 34.75f, 34.95f, 35.13f, 35.31f, 37.75f, 38.14f, 40.76f, 41.13f, 43.34f, 44.33f, 45.15f, 46.38f, 47.32f, 47.59f, 47.96f, 48.8f, 49.09f, 49.46f, 50.3f, 50.58f, 50.97f, 51.81f, 52.09f, 52.48f, 53.29f, 53.59f, 53.99f, 54.81f, 55.11f, 55.48f, 56.31f, 56.61f, 56.98f, 57.79f, 58.07f, 58.45f, 59.13f, 59.32f, 59.51f, 59.68f, 61.38f, 61.56f, 61.75f, 61.94f, 62.86f, 63.04f, 63.23f, 63.42f, 65.89f, 66.26f, 68.89f, 69.25f, 71.86f, 72.07f, 72.25f, 72.43f, 74.87f, 75.06f, 75.25f, 75.45f };
    float[] l3LeftNotesList = { 5.51f, 7.04f, 8.5f, 10.01f, 11.49f, 12.26f, 13.02f, 13.78f, 14.51f, 15.25f, 15.98f, 16.75f, 17.88f, 18.62f, 19.37f, 20.13f, 20.9f, 21.62f, 22.39f, 23.11f, 25.02f, 25.2f, 25.37f, 25.56f, 27.99f, 28.18f, 28.35f, 28.58f, 31f, 31.19f, 31.38f, 31.55f, 34.01f, 34.18f, 34.38f, 34.56f, 37.01f, 37.38f, 40f, 40.39f, 42.98f, 43.99f, 44.9f, 45.63f, 47.12f, 47.76f, 48.14f, 48.32f, 48.63f, 49.27f, 49.66f, 49.82f, 50.14f, 50.78f, 51.17f, 51.35f, 51.64f, 52.29f, 52.66f, 52.86f, 53.12f, 53.78f, 54.16f, 54.35f, 54.64f, 55.31f, 55.67f, 55.85f, 56.13f, 56.79f, 57.17f, 57.36f, 57.61f, 58.27f, 58.65f, 58.84f, 60.62f, 60.81f, 60.99f, 61.19f, 62.14f, 62.3f, 62.5f, 62.68f, 65.13f, 65.53f, 68.12f, 68.51f, 71.13f, 71.32f, 71.5f, 71.69f, 74.14f, 74.31f, 74.5f, 74.68f };
    float[] l3RightNotesList = { 6.27f, 7.77f, 9.26f, 10.75f, 11.87f, 12.63f, 13.38f, 14.15f, 14.87f, 15.61f, 16.38f, 17.14f, 17.5f, 18.25f, 19.01f, 19.75f, 20.5f, 21.27f, 22f, 22.77f, 24.26f, 24.45f, 24.63f, 24.8f, 27.25f, 27.44f, 27.64f, 27.8f, 28.75f, 28.94f, 29.13f, 29.31f, 29.47f, 32.5f, 32.69f, 32.89f, 33.07f, 33.26f, 33.45f, 33.63f, 33.82f, 35.49f, 35.7f, 35.89f, 36.08f, 36.27f, 36.45f, 36.62f, 36.8f, 38.51f, 38.9f, 39.26f, 39.63f, 41.51f, 41.88f, 43.6f, 44.65f, 45.42f, 47.42f, 48.9f, 50.4f, 51.92f, 53.39f, 54.91f, 56.41f, 57.89f, 59.86f, 60.05f, 60.25f, 60.43f, 63.6f, 63.79f, 63.98f, 64.18f, 64.35f, 64.57f, 64.76f, 64.93f, 66.64f, 67f, 67.38f, 67.74f, 69.63f, 70.01f, 70.37f, 70.73f, 72.61f, 72.8f, 72.99f, 73.19f, 73.38f, 73.57f, 73.75f, 73.93f, 75.64f, 75.81f, 76f, 76.17f, 76.37f, 76.56f, 76.74f, 76.93f };
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
