using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour
{
    public Behaviour audioComponent;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
      if(audioComponent != null)
        {
            audioComponent.enabled = !audioComponent.enabled;
        }
    }
}
