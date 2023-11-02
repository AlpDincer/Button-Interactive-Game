using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class ScriptableTemplateData : ScriptableObject
{
    public int distanceWalked;
    public int keysCollected;
    public int buttonsClicked;
    public int birdsFlew;

    public Vector3 lastCharPos;

    public int distanceWalkedHighScore;
    public int buttonsClickedHighScore;
    public int birdsFlewHighScore;

    public bool isPlayed;
}
