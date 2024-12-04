using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered");
            if (audioSource != null)
            {
                Debug.Log("audio playing");
                audioSource.Play();
                Destroy(this);
            }
        }
    }
}
