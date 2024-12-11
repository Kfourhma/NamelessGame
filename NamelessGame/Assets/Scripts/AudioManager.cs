using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot combatSnapshot;
    public AudioMixerSnapshot voidSnapshot;

    private AudioMixerSnapshot currentSnapshot;

    void Start()
    {
        currentSnapshot = normalSnapshot;
        currentSnapshot.TransitionTo(0f);
    }

    public void TransitionToSnapshot(AudioMixerSnapshot newSnapshot, float transitionTime)
    {
        if (currentSnapshot != newSnapshot)
        {
            newSnapshot.TransitionTo(transitionTime); currentSnapshot = newSnapshot;
            currentSnapshot = newSnapshot;
        }
    }

    public AudioMixerSnapshot GetCurrentSnapshot()
    {
        return currentSnapshot;
    }
}
