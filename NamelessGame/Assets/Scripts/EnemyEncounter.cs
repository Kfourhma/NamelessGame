using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyEncounter : MonoBehaviour
{
    public AudioSource audioSource;
    AudioMixer audioMixer;
    GameObject enemy;

    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot combatSnapshot;


    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Trigger entered");
        if (other.CompareTag("Player"))
        {
            combatSnapshot.TransitionTo(1.5f);
            Debug.Log("player entered");
            if (audioSource != null && !audioSource.isPlaying)
            {
                Debug.Log("audio playing");
                audioSource.Play();
                audioSource.loop = true;

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy exited");
            normalSnapshot.TransitionTo(2.0f);
            StartCoroutine(StopAudioWDelay(2.0f));

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
