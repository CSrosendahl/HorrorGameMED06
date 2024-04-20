using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ThrowObjectScript : NetworkBehaviour
{
  
    public AudioClip hitSoundClip; // Assign your hit sound in the Inspector
    bool playSound;
    private void Start()
    {
        playSound = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object hits something
        if (collision.gameObject.CompareTag("Ground")) // Assuming the ground has the tag "Ground"
        {
            // Play the hit sound
            if (hitSoundClip != null && playSound) 
            {
                AudioSource.PlayClipAtPoint(hitSoundClip, collision.contacts[0].point);
                playSound = false;
                StartCoroutine(WaitToDestroy());
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
            
        yield return new WaitForSeconds(5f);
        // Destroy the object
        Destroy(gameObject);
    }

}
