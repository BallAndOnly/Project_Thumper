using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightflickerEmission : MonoBehaviour
{

    [Header("Flicker")]
    public float lightFlickChance; //Out of 100 percent so i guess 0 will just be none
    public float flickAmount; //Amount of flick from 0 to 100
    public float regenSpeed; //How fast the light will go back to it original intensity

    private float originalIntensity;
    float chance;
    float intensityClamp;

    void Start()
    {
        originalIntensity = this.gameObject.GetComponent<Light>().intensity;
    }

    void Update()
    {
        EmitFlicker();
    }

    private void EmitFlicker()
    {
        chance = Random.Range(0f, 100f);
        if (chance <= lightFlickChance) intensityClamp -= flickAmount;
        else if (intensityClamp < 100f) intensityClamp += regenSpeed;

        this.gameObject.GetComponent<Light>().intensity = originalIntensity * (intensityClamp / 100);
    }
}
