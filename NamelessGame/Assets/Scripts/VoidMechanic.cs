using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoidMechanic : MonoBehaviour
{
    public AudioSource voidAmbience;

    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot voidSnapshot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voidSnapshot.TransitionTo(7f);
            voidAmbience.Play();
            Debug.Log("player entered into void");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            normalSnapshot.TransitionTo(7f);
            Debug.Log("player exited out of void");
        }
    }

}
