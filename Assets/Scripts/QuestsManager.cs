using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestsManager : MonoBehaviour {

    GameObject quest1;
    GameObject quest2;
    GameObject quest3;

    Button quest1Button;
    Button quest2Button;
    Button quest3Button;

    // Use this for initialization
    void Start () {
        quest1Button = GameObject.Find("Quest1").GetComponent<Button>();
        quest2Button = GameObject.Find("Quest2").GetComponent<Button>();
        quest3Button = GameObject.Find("Quest3").GetComponent<Button>();
        quest1 = GameObject.Find("Quest1Information");
        quest2 = GameObject.Find("Quest2Information");
        quest3 = GameObject.Find("Quest3Information");

        quest1.SetActive(false);
        quest2.SetActive(false);
        quest3.SetActive(false);

        DisableButtons();
        EnableButtons();
        

        
    }

    public void DisableButtons() {
        quest1Button.interactable = false;
        quest2Button.interactable = false;
        quest3Button.interactable = false;
    }

    public void EnableButtons() {
        quest1Button.interactable = true;

        if (PlayerPrefs.GetInt("Quest2Unlocked") == 1)
        {
            quest2Button.interactable = true;
        }

        if (PlayerPrefs.GetInt("Quest3Unlocked") == 1)
        {
            quest2Button.interactable = true;
        }
    }

    public void SelectQuest1() {
        quest1.SetActive(true);
        DisableButtons();
    }

    public void SelectQuest2() {
        quest2.SetActive(true);
        DisableButtons();
    }

    public void SelectQuest3()
    {
        quest3.SetActive(true);
        DisableButtons();
    }

    public void AcceptQuest(string questName) {
        EnableButtons();
        quest1.SetActive(false);
        quest2.SetActive(false);
        quest3.SetActive(false);
        SceneManager.LoadScene(questName);
    }

    public void DeclineQuest() {
        quest1.SetActive(false);
        quest2.SetActive(false);
        quest3.SetActive(false);
        EnableButtons();
    }

}
