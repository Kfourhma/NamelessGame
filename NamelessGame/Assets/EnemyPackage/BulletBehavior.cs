using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public AudioSource playerGrunt;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the "Player" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player hit");
            playerGrunt.pitch = Random.Range(0.8f,1f);    
            playerGrunt.Play();
            StartCoroutine(DestroyAfterSound());
            
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(playerGrunt.clip.length);

        Destroy(gameObject); // Destroy the bullet
    }
}
