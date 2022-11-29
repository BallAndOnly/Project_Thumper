using UnityEngine;

public class EntranceDoorControl : MonoBehaviour, IntInteractable
{
    public Animator AnimatorComponent;
    public Vector3 interactPos;

    bool isOpen;

    public string ShowActionText()
    {
        return "";
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
        isOpen = true;
        AnimatorComponent.SetBool("Entrance IsOpen", isOpen);
    }
}
