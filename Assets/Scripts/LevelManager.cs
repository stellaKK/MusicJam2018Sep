using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private void Start()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1024, 768, true);
        }
        else {
            Screen.SetResolution(1024, 768, false);
        }
    }

    public void LoadLevel(string name)
    {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
