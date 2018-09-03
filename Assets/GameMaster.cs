using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    private Character character;
    private float characterHealth;
    private Text healthText;

    private NoteSpawner upNoteSpawner;
    private NoteSpawner leftNoteSpawner;
    private NoteSpawner rightNoteSpawner;

    // Use this for initialization
    void Start () {
        character = GameObject.Find("Character").GetComponent<Character>();
        healthText = GameObject.Find("HealthBar").GetComponent<Text>();
        upNoteSpawner = GameObject.Find("UpSpawner").GetComponent<NoteSpawner>();
        leftNoteSpawner = GameObject.Find("LeftSpawner").GetComponent<NoteSpawner>();
        rightNoteSpawner = GameObject.Find("RightSpawner").GetComponent<NoteSpawner>();

        upNoteSpawner.SpawnDoubleNote();
        leftNoteSpawner.SpawnSingleNote();
        rightNoteSpawner.SpawnSingleNote();
    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = character.characterHealth.ToString();

	}
}
