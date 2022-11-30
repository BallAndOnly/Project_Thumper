using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traindoor : MonoBehaviour, IntInteractable
{
    public AudioClip enterSound, locked;
    public AudioSource audioSource;
    public Animator AnimatorComponent;
    public TextAnim textAnim;
    public int neededLV = 1;
    private bool isEnd = false;
    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void FixedUpdate()
    {
        AnimatorComponent.SetBool("End", isEnd);
    }

    public string ShowActionText()
    {
        if (playerController.LV < neededLV) return "I need a key for this";
        else return "";
    }
    public bool ShowHand()
    {
        return true;
    }
    public Vector3 HandPos()
    {
        return transform.position;
    }
    public void Interact()
    {
        if (playerController.LV >= neededLV)
        {
            audioSource.PlayOneShot(enterSound);
            audioSource.volume = 0.7f;
            textAnim.EndCheck();
            isEnd = true;
        }
        else
        {
            audioSource.PlayOneShot(locked);
            audioSource.volume = 1f;
        }
    }
}
