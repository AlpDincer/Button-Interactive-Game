using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public GameObject playerChar;
    public GameObject moveTargetA;
    public GameObject moveTargetB;
    public GameObject moveTargetC;
    public GameObject moveTargetD;
    public GameObject moveTargetE;
    public GameObject moveTargetF;
    public GameObject moveTargetG;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CheckWalking()
    {
        return playerChar.GetComponent<MyCharController>().IsWalking();
    }

    public bool CheckDoorUnlock()
    {
        return playerChar.GetComponent<MyCharController>().CanUnlock();
    }

    public void UnlockDoor()
    {
        playerChar.GetComponent<MyCharController>().UnlockDoor();
    }

    public void MoveToTarget(string target) //checks location first,go there if you are not there
    {
        switch (target)
        {
            case "charMoveA":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetA.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetA); }
                break;
            case "charMoveB":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetB.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetB); }
                break;
            case "charMoveC":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetC.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetC); }
                break;
            case "charMoveD":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetD.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetD); }
                break;
            case "charMoveE":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetE.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetE); }
                break;
            case "charMoveF":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetF.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetF); }
                break;
            case "charMoveG":
                if (playerChar.GetComponent<MyCharController>().AtLocation(moveTargetG.transform) == false)
                { playerChar.GetComponent<MyCharController>().Move(moveTargetG); }
                break;
        }

    }

    public void StartWave()
    {
        playerChar.GetComponent<MyCharController>().Wave();
    }

    public void StartPick()
    {
        playerChar.GetComponent<MyCharController>().Pick();
    }

    public Transform GetPosition()
    {
        return playerChar.GetComponent<MyCharController>().Position();
    }
}
