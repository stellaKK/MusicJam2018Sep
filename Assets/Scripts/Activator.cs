using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public KeyCode key;
    public GameObject note;

    public bool createMode;

    private bool active = false; // check if note is inside activator
    private bool hasPressed = false; // check if use has pressed key
    private bool dodgeCooler = false;
    private float pressDelay = 0.1f; // how long before Player can press again
    private bool isDoubleClicking = false;

    public AudioClip notesDestroyedSound;
    public AudioClip dodgeSound;
    private AudioSource audioSource;

    private GameObject character;
    private Character characterScript;
    private SpriteRenderer spriteRenderer;
    private Color color;

    private float lastTime = -1.0f;

    GameManager gameManager;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        character = GameObject.Find("Character");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterScript = character.GetComponent<Character>();
        audioSource = GetComponent<AudioSource>();
        color = spriteRenderer.color;
    } 
	
	// Update is called once per frame
	void Update () {
        // Get User Input on every frame update
        if (Input.GetKeyDown(key) && !hasPressed)
        {
            if (createMode)
            {
                gameManager.RecordPosition(this.transform.name);
            }
            else {
                Pressed();
                // Condition when User wants to move character left or right
                if (key == KeyCode.A && !dodgeCooler)
                {
                    characterScript.isFacingRight = false;
                    characterScript.isDodging = true;
                    dodgeCooler = true;
                    audioSource.clip = dodgeSound;
                    audioSource.Play();
                    character.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * characterScript.dodgeSpeed);
                    Invoke("ResetDodge", 1f);
                }
                else if (key == KeyCode.D && !dodgeCooler)
                {
                    characterScript.isFacingRight = true;
                    characterScript.isDodging = true;
                    dodgeCooler = true;
                    audioSource.clip = dodgeSound;
                    audioSource.Play();
                    character.GetComponent<Rigidbody2D>().AddForce(Vector2.right * characterScript.dodgeSpeed);
                    Invoke("ResetDodge", 1f);
                }
                // Condition when User wants to press Activator to destroy notes
                else
                {
                    if (Time.time - lastTime < 0.2f)
                    {
                        lastTime = Time.time;
                        characterScript.isAttackingDouble = true;
                        print("double type");
                        isDoubleClicking = true;
                    }
                    else
                    {
                        lastTime = Time.time;
                        characterScript.isAttackingSingle = true;
                        print("single type");
                        isDoubleClicking = false;
                    }

                    if (active && note != null)
                    {
                        if (note.layer != 8)
                        {
                            gameManager.comboCount += 1;
                            Destroy(note);
                            audioSource.clip = notesDestroyedSound;
                            audioSource.Play();
                            active = false;
                        }
                        else
                        {
                            if (isDoubleClicking)
                            {
                                gameManager.comboCount += 1;
                                Destroy(note);
                                audioSource.clip = notesDestroyedSound;
                                audioSource.Play();
                                active = false;
                            } 
                        }

                        
                    }
                }
                // Reset Player press action after delay
                Invoke("ResetPressed", pressDelay);
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // As note enters Trigger zone of Activator, Set NoteVariable to this note
        // Then set active as true
        active = true;
        if (collision.GetComponent<Note>())
        {
            note = collision.gameObject;
        }
    }

    void Pressed()
    {
        hasPressed = true;
        spriteRenderer.color = Color.black;
    }

    void ResetPressed()
    {
        hasPressed = false;
        characterScript.isDodging = false;
        spriteRenderer.color = color;
        isDoubleClicking = false;
    }

    void ResetDodge()
    {
        dodgeCooler = false;
    }
}
