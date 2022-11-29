using UnityEngine;

public interface IntInteractable
{
    string ShowActionText();
    bool ShowHand();
    Vector3 HandPos();
    void Interact();
}
