using UnityEngine;

public class EntranceDoorControl : MonoBehaviour, IntInteractable
{
    public Animator AnimatorComponent;
    public Vector3 interactPos;
    public string interactText;
    public float Pitch;
    public AudioSource DoorSource;
    public AudioClip DoorSound;

    bool isOpen;

    public string ShowActionText()
    {
        if (isOpen) return interactText;
        else return "";
    }

    public bool ShowHand()
    {
        return !isOpen;
    }
    public Vector3 HandPos()
    {
        return transform.position + interactPos;
    }

    public void Interact()
    {
        if (!isOpen) 
        {
            DoorSource.PlayOneShot(DoorSource.clip);
            DoorSource.pitch = Pitch;
            DoorSource.volume = 1f;
        }

        isOpen = true;
        AnimatorComponent.SetBool("Entrance IsOpen", isOpen);      
    }
}
