using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{

    [Header("Flicker")]
    public float sparkChance; //Out of 100 percent so i guess 0 will just be none
    public float sparkAmount; //Spark power
    public float dieSpeed; //How fast the light will die out
    public AudioSource audioSource;

    private float originalIntensity;
    float chance;
    float intensityClamp;

    void Start()
    {
        originalIntensity = this.gameObject.GetComponent<Light>().intensity;
    }

    void Update()
    {
        LightSpark();
    }

    private void LightSpark()
    {
        intensityClamp = Mathf.Clamp(intensityClamp, 0f, 5f);
        chance = Random.Range(0f, 100f);
        if (chance <= sparkChance) 
        {
            intensityClamp += sparkAmount;
            audioSource.PlayOneShot(audioSource.clip);
        }
        else if (intensityClamp < 100f) intensityClamp -= dieSpeed;

        this.gameObject.GetComponent<Light>().intensity = (intensityClamp);
        audioSource.volume = (intensityClamp / 100);
    }
}
