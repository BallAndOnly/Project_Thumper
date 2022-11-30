using UnityEngine;

public class Keycard : MonoBehaviour, IntInteractable
{
    public AudioClip pickupSound;
    public AudioSource audioSource;
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
        audioSource.PlayOneShot(audioSource.clip);
        audioSource.volume = 0.7f;
        playerController.LV = keycardLV;
        transform.gameObject.SetActive(false);
    }
}
