using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyEncounter : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioManager audioManager;
    public GameObject enemy;

    private bool inCombat = false;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Trigger entered");
        if (other.CompareTag("Player"))
        {
            if (audioManager.GetCurrentSnapshot() == audioManager.voidSnapshot)
            {
                return;
            }

            if(!inCombat)
            { 
                audioManager.TransitionToSnapshot(audioManager.combatSnapshot, 7f);
                inCombat = true;
                
                if (audioSource != null && !audioSource.isPlaying)
                {
                    Debug.Log("audio playing");
                    audioSource.Play();
                    audioSource.loop = true;

                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (audioManager.GetCurrentSnapshot() == audioManager.voidSnapshot)
            {
                return;
            }

            if (inCombat)
            {
                Debug.Log("Enemy exited");
                audioManager.TransitionToSnapshot(audioManager.normalSnapshot, 7f);
                inCombat = false;
                StartCoroutine(StopAudioWDelay(2.0f));
            }
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Enemy destroyed");
        if (inCombat)
        {
            // Transition to the normal snapshot
            audioManager.TransitionToSnapshot(audioManager.normalSnapshot, 7f);
            inCombat = false;

            // Stop and cleanup audio
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.loop = false;
                Debug.Log("Audio stopped");
            }
        }
    }

    private IEnumerator StopAudioWDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
            Debug.Log("Audio stopped");
        }
    }
}
