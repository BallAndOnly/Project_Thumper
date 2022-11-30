using UnityEngine;

public class Keycard : MonoBehaviour, IntInteractable
{
    public AudioClip pickupSound;
    protected AudioSource audioSource;
    public int keycardLV = 1;
    public PlayerController playerController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        audioSource.clip = pickupSound;
        playerController.LV = keycardLV;
        Destroy(transform.gameObject);
    }
}
