using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    private AudioSource soundPlayer;

    public AudioClip buttonClick;

    private void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void OnButtonClick() {
        soundPlayer.clip = buttonClick;
        soundPlayer.Play();
    }
}
