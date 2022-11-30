using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSwap : MonoBehaviour
{
    public AudioClip newTrack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ambientmanager.instance.SwapTrack(newTrack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ambientmanager.instance.ReturnToDefault();
        }
    }
}
