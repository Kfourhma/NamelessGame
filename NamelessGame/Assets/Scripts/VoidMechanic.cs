using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoidMechanic : MonoBehaviour
{

    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot voidSnapshot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voidSnapshot.TransitionTo(3f);
            Debug.Log("player entered into void");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            normalSnapshot.TransitionTo(3f);
            Debug.Log("player exited out of void");
        }
    }

}
