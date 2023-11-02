using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource singleBirdSound;
    public AudioSource multipleBirdSound;
    public AudioSource shipSound;
    public AudioSource eggSound;
    public AudioSource doorSound;
    public AudioSource keySound;
    public AudioSource walkSound;
    public AudioSource helloSound;
    public AudioSource buttonSound;
    public AudioSource musicSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Play Sound
    public void PlaySingleBirdSound()
    { singleBirdSound.Play(); }
    public void PlayMultipleBirdSound()
    { multipleBirdSound.Play(); }
    public void StopMultipleBirdSound()
    { multipleBirdSound.Stop(); }
    public void PlayShipSound()
    { shipSound.Play(); }
    public void PlayEggSound()
    { eggSound.Play(); }
    public void PlayDoorSound()
    { doorSound.Play(); }
    public void PlayKeySound()
    { keySound.Play(); }
    public void PlayWalkSound()
    { walkSound.Play(); }
    public void StopWalkSound()
    { walkSound.Stop(); }
    public void PlayHelloSound()
    { helloSound.Play(); }
    public void PlayButtonSound()
    { buttonSound.Play(); }
    public void PlayMusicSound()
    { musicSound.Play(); }

    #endregion
}
