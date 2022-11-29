using UnityEngine;

public class EntranceDoorControl : MonoBehaviour
{
    public Animator AnimatorComponent;
    public Vector3 interactPos;

    Vector3 doorTransform;
    bool isOpen;

    private void Update()
    {
        doorTransform = transform.position + interactPos;
    }

    public string ShowActionText()
    {
        if (!isOpen) return "";
        else return "Door jammed";
    }

    public bool ShowHand()
    {
        return !isOpen;
    }
    public Vector3 HandPos()
    {
        return doorTransform;
    }

    public void Interact()
    {
        isOpen = true;
        AnimatorComponent.SetBool("Entrance IsOpen", isOpen);
    }
}
