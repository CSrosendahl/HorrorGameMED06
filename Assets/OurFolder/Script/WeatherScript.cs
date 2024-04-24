using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class WeatherScript : NetworkBehaviour
{
    public Light directionalLight;
    public float flashDuration = 0.1f;
    public float flashIntensity = 100f;
    public AudioSource thunderAudioSource;

    public AudioSource rainAudioSource;

    public AudioClip rainClip;
    public AudioClip thunderClip;
 
    public bool thunderPlayed;

    public bool playWeatherEffects;
    private void Start()
    {
        rainAudioSource.clip= rainClip;
        rainAudioSource.Play();

        playWeatherEffects = true; // Set this to false when to disable weathereffects
        thunderPlayed = false;
        StartCoroutine(RandomLightningStrike());
    }

    public void LightningStrike()
    {
        StartCoroutine(FlashLight());
    }

    IEnumerator FlashLight()
    {
        directionalLight.intensity = flashIntensity;

        if(!thunderPlayed)
        {
            thunderAudioSource.clip = thunderClip;
            thunderAudioSource.Play();
        }
        
        yield return new WaitForSeconds(flashDuration);
        directionalLight.intensity = 1;
    }

    IEnumerator RandomLightningStrike()
    {
    
        while (playWeatherEffects)
        {
            // Wait for a random interval between 5 and 20 seconds
            float randomInterval = Random.Range(6f, 20f);
            yield return new WaitForSeconds(randomInterval);

            // Perform a lightning strike with two quick flashes
            LightningStrike();
            thunderPlayed = true;
            yield return new WaitForSeconds(1);
            LightningStrike();
            thunderPlayed = false;
        }
    }
}
