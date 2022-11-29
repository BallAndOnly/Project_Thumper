using UnityEngine;

public class Keycard : MonoBehaviour, IntInteractable
{
    public int keycardLV = 1;
    public PlayerController playerController;

    public string ShowActionText()
    {
        return "";
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
        if (keycardLV == 1) { playerController.LV1 = true; }
        else if (keycardLV == 2) { playerController.LV2 = true; }
        else if (keycardLV == 3) { playerController.LV3 = true; }

        Destroy(transform.gameObject);
    }
}
