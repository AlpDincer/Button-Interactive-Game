using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentAnimManager : Singleton<EnviromentAnimManager>
{
    private Animator shipAnim;
    private Animator birdAnim;
    private Animator nestAnim;

    public GameObject ship;
    public GameObject bird;
    public GameObject nest;

    // Start is called before the first frame update
    void Start()
    {
        shipAnim = ship.GetComponent<Animator>();

        birdAnim = bird.GetComponent<Animator>();

        nestAnim = nest.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSail()
    {
        shipAnim.Play("Sail");
        SoundManager.Instance.PlayShipSound();
    }

    public void Fly()
    {
        birdAnim.Play("Fly");
        birdAnim.Play("Eyes_Shrink");
        birdAnim.Play("FlyAround");

        SoundManager.Instance.PlaySingleBirdSound();
    }

    public void NestJump()
    {
        nestAnim.Play("NestAnim");
        StartCoroutine(EggSoundWait());
    }

    IEnumerator EggSoundWait()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.PlayEggSound();
    }
}
