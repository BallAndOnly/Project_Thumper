using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorKeypad : MonoBehaviour, IntInteractable
{
    PlayerController playerController;
    public AudioClip access, denied;
    protected AudioSource audioSource;
    public SlideDoor door;
    public string interactText;
    public int neededLV = 1;
    public bool doorPlaySound = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    public string ShowActionText()
    {
        if (playerController.LV < neededLV) return interactText;
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
        if (door.isAnimationPlaying && playerController.LV >= neededLV && doorPlaySound == false)
        {
            doorPlaySound = true;
            audioSource.PlayOneShot(access);
            audioSource.volume = 0.7f;
            door.isOpen = !door.isOpen;
        }
        else 
        {
            audioSource.PlayOneShot(denied);
            audioSource.volume = 0.6f;
            doorPlaySound = false;
        }
    }
}
