using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    public Button charMoveA;
    public Button charMoveB;
    public Button charMoveC;
    public Button charMoveD;
    public Button charMoveE;
    public Button charMoveF;
    public Button charMoveG;
    public Button waveButton;
    public Button pickButton;

    public Button key1;
    public Button key2;
    public Button key3;
    public Button door1;
    public Button door2;
    public Button door3;

    public Button resetButton;
    public Button continueButton;
    public GameObject playedBeforePanel;

    public TMP_Text keyCountText;
    public TMP_Text buttonCountText;
    public TMP_Text birdCountText;
    public TMP_Text distanceCountText;
    public TMP_Text positionText;

    public List<Button> allButtons;
    private List<Button> disabledButtons;

    public bool gotKey;

    void Start()
    {
        disabledButtons = new List<Button>();

        gotKey = false;

        key1.interactable = false;
        key2.interactable = false;
        key3.interactable = false;

        charMoveC.interactable = false;
        charMoveD.interactable = false;
        charMoveE.interactable = false;
        charMoveF.interactable = false;
        charMoveG.interactable = false;
}

    // Update is called once per frame
    void Update()
    {
        if(CharacterManager.Instance.CheckWalking() == false)
        {
            waveButton.interactable = true;
            pickButton.interactable = true;
        }

        else 
        {
            waveButton.interactable = false;
            pickButton.interactable = false;
        }

        keyCountText.text = " " + GameManager.Instance.GetKeyCount() + " Key(s) Collected";
        buttonCountText.text = " " + GameManager.Instance.GetButtonCount() + " Button(s) Pressed";
        birdCountText.text = " " + GameManager.Instance.GetBirdCount() + " Bird(s) Flew";
        distanceCountText.text = " " + GameManager.Instance.GetDistanceCount() + " Meter(s) Ran";
        positionText.text = "Character Position: (" + GameManager.Instance.GetCharPos() + ")";

    }

    #region Button Press
    public void MoveButtonPressed(string buttonName)
    {
        SoundManager.Instance.PlayButtonSound();
        CharacterManager.Instance.MoveToTarget(buttonName);
        GameManager.Instance.SetButtonCount();
    }

    public void ShipButtonPressed()
    {
        SoundManager.Instance.PlayButtonSound();
        EnviromentAnimManager.Instance.SetSail();
        GameManager.Instance.SetButtonCount();
    }

    public void BirdButtonPressed()
    {
        SoundManager.Instance.PlayButtonSound();
        EnviromentAnimManager.Instance.Fly();
        GameManager.Instance.SetButtonCount();
        GameManager.Instance.SetBirdCount(false);
    }

    public void NestButtonPressed()
    {
        SoundManager.Instance.PlayButtonSound();
        EnviromentAnimManager.Instance.NestJump();
        GameManager.Instance.SetButtonCount();
    }

    public void  WaveButtonPressed()
    {
        SoundManager.Instance.PlayButtonSound();
        CharacterManager.Instance.StartWave();
        GameManager.Instance.SetButtonCount();
    }

    public void PickButtonPressed()
    {
        SoundManager.Instance.PlayButtonSound();
        CharacterManager.Instance.StartPick();
        GameManager.Instance.SetButtonCount();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void DestroyDoorButtonPressed(string buttonName)
    {
        SoundManager.Instance.PlayButtonSound();

        switch (buttonName)
        {
            case "Door1":
                door1.interactable = false;
                charMoveC.interactable = true;
                

                if (door2.interactable == false)
                {
                    charMoveD.interactable = true;
                    charMoveE.interactable = true;
                    charMoveF.interactable = true;
                }

                if (door3.interactable == false)
                {
                    charMoveG.interactable = true;
                }

                break;
            case "Door2":                
                door2.interactable = false;

                if (door1.interactable == false)
                {
                    charMoveD.interactable = true;
                    charMoveE.interactable = true;
                    charMoveF.interactable = true;

                    if(door3.interactable == false) { charMoveG.interactable = true; }
                }
                break;
            case "Door3":
                door3.interactable = false;
                
                if(door1.interactable == false && door2.interactable == false)
                {
                    charMoveG.interactable = true;
                }
                break;
        }

        GameManager.Instance.SetButtonCount();
        GameManager.Instance.FlockAttack(buttonName);
    }
    public void KeyPressed(string keyName)
    {
        SoundManager.Instance.PlayButtonSound();

        if (CharacterManager.Instance.CheckDoorUnlock() == true)
        {
            CharacterManager.Instance.UnlockDoor();

            if (keyName == "Key1")
            {
                key1.interactable = false;
                charMoveC.interactable = true;               

                if (GameManager.Instance.CheckOtherDoors("Door2") == false)
                {
                    charMoveD.interactable = true;
                    charMoveE.interactable = true;
                    charMoveF.interactable = true;
                }

                if (GameManager.Instance.CheckOtherDoors("Door3") == false)
                {
                    charMoveG.interactable = true;
                }
            }

            else if (keyName == "Key2")
            {
                key2.interactable = false;
                charMoveD.interactable = true;
                charMoveE.interactable = true;
                charMoveF.interactable = true;

                if(GameManager.Instance.CheckOtherDoors("Door3") == false)
                {
                    charMoveG.interactable = true;
                }
            }

            else
            {
                key3.interactable = false;
                charMoveG.interactable = true;
            }

        }
        GameManager.Instance.SetButtonCount();
    }

    public void RestartButtonPressed()
    {
        GameManager.Instance.ResetData();
        SoundManager.Instance.PlayButtonSound();
        playedBeforePanel.SetActive(false);
    }

    public void ContinueButtonPressed()
    {
        SoundManager.Instance.PlayButtonSound();
        playedBeforePanel.SetActive(false);
    }

    #endregion

    public void OpenPlayedBefore()
    {
        playedBeforePanel.SetActive(true);
    }
    public void GotKey(string keyName)
    {
        switch(keyName)
        {
            case "Key1":
                key1.interactable = true;
                break;
            case "Key2":
                key2.interactable = true;
                break;
            case "Key3":
                key3.interactable = true;
                break;
        }
    }

    #region Highlight
    public void HighlightPickButton()
    {
        StartCoroutine(Highlight(pickButton));
    }

    IEnumerator Highlight(Button bttn)
    {
        bttn.Select();

        yield return new WaitForSeconds(1.5f);

        EventSystem.current.SetSelectedGameObject(null);

        yield return new WaitForSeconds(1.5f);
    }
    #endregion

    #region Enable/Disable Buttons
    public void DisableAllButtons()
    {
        for(int i = 0; i < allButtons.Count; i++)
        {
            if(allButtons[i].interactable == true)
            {
                allButtons[i].interactable = false;
                disabledButtons.Add(allButtons[i]);
            }
        }
    }

    public void EnableAllButtons()
    {
        for (int i = 0; i < disabledButtons.Count; i++)
        {
            disabledButtons[i].interactable = true;
        }

        disabledButtons.Clear();
    }

    public void DisableButton(string buttonName)
    {
        for (int i = 0; i < allButtons.Count; i++)
        {
            if (allButtons[i].name == buttonName + "Bttn")
            {
                allButtons[i].interactable = false;
            }
        }
    }

    public void EnableButton(string buttonName)
    {
        for (int i = 0; i < allButtons.Count; i++)
        {
            if (allButtons[i].name == buttonName + "Bttn")
            {
                allButtons[i].interactable = false;
            }
        }
    }
    #endregion
}
