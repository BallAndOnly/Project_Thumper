using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    public Animator AnimatorComponent;
    public SlideDoorKeypad Keypad, Keypad2;
    public AudioClip slideDoorOpen, slideDoorForceOpen;
    public AudioSource audioSource;
    public bool isAnimationPlaying;
    public bool isOpen = false;

    private void FixedUpdate()
    {

        AnimatorComponent.SetBool("isOpen", isOpen);
        isAnimationPlaying = (AnimatorComponent.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f);

        if (isAnimationPlaying && (Keypad.doorPlaySound || Keypad2.doorPlaySound)) 
        {
            audioSource.PlayOneShot(slideDoorOpen);
            audioSource.pitch = 0.8f;
            audioSource.volume = 0.7f;
            Keypad.doorPlaySound = false;
            Keypad2.doorPlaySound = false;
        }
    }   
}
