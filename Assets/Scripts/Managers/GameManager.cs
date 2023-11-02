using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ScriptableTemplateData gameData;

    public GameObject angrySparrowFlock;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    public GameObject key1;
    public GameObject key2;
    public GameObject key3;

    public GameObject birdFlock;


    private Vector3 flockStartLocation;
    private Vector3 charPos;

    private int tempX = -99;
    private int tempY = -99;
    private int tempZ = -99;

    string charPosString;

    // Start is called before the first frame update
    void Start()
    {       
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 61;
        if (gameData.isPlayed == true)
        { UIManager.Instance.OpenPlayedBefore(); }

        if (gameData.isPlayed == false)
        {
            gameData.distanceWalked = 0;
            gameData.keysCollected = 0;
            gameData.buttonsClicked = 0;
            gameData.birdsFlew = 0;
            gameData.isPlayed = true;
        }

        gameData.lastCharPos = CharacterManager.Instance.GetPosition().transform.position;
        flockStartLocation = angrySparrowFlock.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        gameData.lastCharPos = gameData.lastCharPos = CharacterManager.Instance.GetPosition().transform.position;
    }

    #region Data Functions
    public void SetDistanceCount(int distance)
    {
        gameData.distanceWalked += distance;
    }

    public void SetKeyCount()
    {
        gameData.keysCollected += 1;
    }

    public void SetButtonCount()
    {
        gameData.buttonsClicked += 1;
    }

    public void SetBirdCount(bool isFlock)
    {
        if (isFlock == false)
        { gameData.birdsFlew += 1; }
        else 
        { 
            for (int i = 0; i < birdFlock.transform.childCount; i++)
            {
                gameData.birdsFlew += 1;
            }
        }
    }

    public string GetDistanceCount()
    {
        return gameData.distanceWalked.ToString();
    }

    public string GetKeyCount()
    {
        return gameData.keysCollected.ToString();
    }

    public string GetButtonCount()
    {
        return gameData.buttonsClicked.ToString();
    }

    public string GetBirdCount()
    {
        return gameData.birdsFlew.ToString();
    }
    public string GetCharPos()
    {
        

        charPos = gameData.lastCharPos;

        int x = Mathf.RoundToInt(charPos.x);
        int y = Mathf.RoundToInt(charPos.y);
        int z = Mathf.RoundToInt(charPos.z);

        if(tempX != x && tempX != -99)
        {
            SetDistanceCount(Mathf.Abs(tempX - x));
        }

        if (tempY != y && tempY != -99)
        {
            SetDistanceCount(Mathf.Abs(tempY - y));
        }

        if (tempZ != z && tempZ != -99)
        {
            SetDistanceCount(Mathf.Abs(tempZ - z));
        }

        tempX = x;
        tempY = y;
        tempZ = z;

        charPosString = x.ToString() + ", " + y.ToString() + ", " + z.ToString();
        return charPosString;

    }

    public void ResetData()
    {
        gameData.distanceWalked = 0;
        gameData.keysCollected = 0;
        gameData.buttonsClicked = 0;
        gameData.birdsFlew = 0;
    }

    [ContextMenu("ResetAllData")]
    public void ResetGame()
    {
        gameData.distanceWalked = 0;
        gameData.keysCollected = 0;
        gameData.buttonsClicked = 0;
        gameData.birdsFlew = 0;

        gameData.isPlayed = false;
    }
    #endregion

    #region BirdFlock Functions
    public void FlockAttack(string doorName)
    {
        GameObject tempDoor = null;
        GameObject tempKey = null;
        switch (doorName)
        {
            case "Door1":
                tempDoor = door1;
                tempKey = key1;
                break;
            case "Door2":
                tempDoor = door2;
                tempKey = key2;
                break;
            case "Door3":
                tempDoor = door3;
                tempKey = key3;
                break;
        }
        UIManager.Instance.DisableAllButtons();
        angrySparrowFlock.SetActive(true);
        SoundManager.Instance.PlayMultipleBirdSound();

        SetBirdCount(true);
        StartCoroutine(FlockReset(tempDoor, tempKey));
    }

    IEnumerator FlockReset(GameObject tempDoor, GameObject tempKey)
    {
        yield return new WaitForSecondsRealtime(2f);
        if(tempDoor != null)
        {
            tempDoor.SetActive(false);
            tempKey.SetActive(false);
        }
        yield return new WaitForSecondsRealtime(2f);

        SoundManager.Instance.StopMultipleBirdSound();
        angrySparrowFlock.SetActive(false);
        angrySparrowFlock.transform.position = flockStartLocation;
        UIManager.Instance.EnableAllButtons();
    }
    #endregion
    public bool CheckOtherDoors(string doorName)
    {
        if (doorName == "Door2")
        { 
            if (door2.activeInHierarchy == false)
            {
                return false;
            }
            else return true;
        }
        if (doorName == "Door3")
        {
            if (door3.activeInHierarchy == false)
            {
                return false;
            }
            else return true;
        }

        return true;
    }



}
