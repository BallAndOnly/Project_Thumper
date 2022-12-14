using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightflicker : MonoBehaviour
{

    [Header("Flicker")]
    public float lightFlickChance; //Out of 100 percent so i guess 0 will just be none
    public float flickAmount; //Amount of flick from 0 to 100
    public float regenSpeed; //How fast the light will go back to it original intensity
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
        LightFlicker();
    }

    private void LightFlicker()
    {
        intensityClamp = Mathf.Clamp(intensityClamp, 0f, 100f);
        chance = Random.Range(0f, 100f);
        if (chance <= lightFlickChance) intensityClamp -= flickAmount;
        else if (intensityClamp < 100f) intensityClamp += regenSpeed;

        this.gameObject.GetComponent<Light>().intensity = originalIntensity * (intensityClamp / 100);
        audioSource.volume = 1.75f - (intensityClamp/100);
    }
}
