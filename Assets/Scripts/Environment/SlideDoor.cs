using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    public Animator AnimatorComponent;
    public AudioClip slideDoorOpen, slideDoorForceOpen;
    protected AudioSource audioSource;
    public bool isAnimationPlaying;
    public bool isOpen = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.clip = slideDoorOpen;
        AnimatorComponent.SetBool("isOpen", isOpen);
        isAnimationPlaying = (AnimatorComponent.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f);
    }
}
