using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyCharController : MonoBehaviour
{
    [SerializeField] private GameObject walkTarget;

    [SerializeField] private string isKeyNear = null;
    [SerializeField] private bool pickingUp = false;
    [SerializeField] private bool canUnlock = false;
    [SerializeField] private bool doorOpen = false;
    [SerializeField] private bool uiTimer;

    [SerializeField] private GameObject nearDoor;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }

        uiTimer = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Key" && pickingUp == false)
        {
            if (uiTimer == true)
            {
                UIManager.Instance.HighlightPickButton();
                isKeyNear = col.gameObject.name;

                StartCoroutine(Wait());
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {       

        if (col.gameObject.tag == "Key" && pickingUp == true)
        {
            isKeyNear = null;
            col.gameObject.SetActive(false);
            pickingUp = false;
            GameManager.Instance.SetKeyCount();
            SoundManager.Instance.PlayKeySound();
            UIManager.Instance.GotKey(col.gameObject.name);
        }

        if (col.gameObject.name == "DoorFrame" && isKeyNear != null)
        {
            canUnlock = true;
            nearDoor = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.name == "DoorFrame")
        {
            canUnlock = false;
            isKeyNear = null;
        }
    }

    private void Update()
    {
        if(m_animator.GetBool("Walk") == true)
        {
            agent.destination = walkTarget.transform.position;           
            if (transform.position == new Vector3(walkTarget.transform.position.x, transform.position.y, walkTarget.transform.position.z))
            {
                SoundManager.Instance.StopWalkSound();
                m_animator.SetBool("Walk", false);
            }
        }

        
    }

    public Transform Position()
    {
        return gameObject.transform;
    }

    #region Animations
    public bool IsWalking()
    {
        return m_animator.GetBool("Walk");
    }

    public void Move(GameObject target)
    {
        walkTarget = target;
        transform.LookAt(walkTarget.transform);
        SoundManager.Instance.PlayWalkSound();
        m_animator.SetBool("Walk", true);       
    }

    public bool AtLocation(Transform targetLocation)
    {
        if (targetLocation.position.x == gameObject.transform.position.x && targetLocation.position.z == gameObject.transform.position.z)
        {
            return true;
        }

        else return false;
    }

    public void Wave()
    {
        SoundManager.Instance.PlayHelloSound();
        m_animator.Play("Wave");
    }

    public void Pick()
    {
        m_animator.Play("Pickup");
        pickingUp = true;
    }
    #endregion

    #region Door&Key
    public bool CanUnlock()
    {
        return canUnlock;
    }

    public void UnlockDoor()
    {
        nearDoor.transform.GetChild(0).GetComponent<Animator>().SetBool("isUnlocked", true);
        nearDoor.transform.GetChild(0).GetComponent<Animator>().Play("DoorAnim");

        SoundManager.Instance.PlayDoorSound();

        UIManager.Instance.DisableButton(nearDoor.transform.parent.name);

        nearDoor.GetComponent<BoxCollider>().enabled = false;
        nearDoor = null;
        isKeyNear = null;

    }

    public void KeyAlert()
    {
        if (uiTimer == true && isKeyNear != null)
        {
            UIManager.Instance.HighlightPickButton();
            uiTimer = false;
            StartCoroutine(Wait());
        }
    }
    #endregion
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f); //5f
        uiTimer = true;
        KeyAlert();
    }

}
