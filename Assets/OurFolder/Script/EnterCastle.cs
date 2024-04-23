using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnterCastle : NetworkBehaviour
{

    public Transform spawnEntranceCastle;
    public AudioSource audioSource;
    public AudioClip openDoorClip;
    public bool hasPlayed;
    public WeatherScript weatherScript;

    public GameObject IntroEntrance;

    private void Start()
    {
        hasPlayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human") && !hasPlayed)
        {
            Debug.Log("Player entered the castle");
          //  other.transform.position = spawnEntranceCastle.position;
            audioSource.clip= openDoorClip;
            audioSource.Play();
            hasPlayed = true;
            StartCoroutine(WaitToEnterAfterKnock(other));
            StartCoroutine(ResetHasPlayed());
        }
    }

    IEnumerator ResetHasPlayed()
    {
      
        yield return new WaitForSeconds(2);
        hasPlayed = false;
    
    }
    public void DisableIntroGameObjects()
    {
        // Make sure this only executes if both players have entered the castle.

        weatherScript.playWeatherEffects = false;
        IntroEntrance.SetActive(false);
    }

    IEnumerator WaitToEnterAfterKnock(Collider other)
    {
        
        yield return new WaitForSeconds(1.5f);

        other.transform.position = spawnEntranceCastle.position;

    }
}
