using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DayNightCycle : MonoBehaviour
{
    public Light sunLight;
    public float timeOfDay = 120f;
    public AudioManager manager;

    public AudioSource voidWarning;
    public AudioSource voidAmbience;

    private bool inVoid = false;

    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float rotation = (time / timeOfDay) * 360f;

        sunLight.transform.rotation = Quaternion.Euler(rotation, 0, 0);

        if(!inVoid && time >= 45f)
        {
            voidWarning.Play();
        }

        if(!inVoid && time >= 60f)
        {
            
            EnterVoid();
            Debug.Log("player entered into void");

            if(!manager.voidSnapshot)
            {
                EnterVoid();
            }
        }

        

        if(inVoid && time > timeOfDay)
        {
            ExitVoid();
            Debug.Log("player exited out of void");
        }

        void EnterVoid()
        {
            inVoid = true;
            manager.TransitionToSnapshot(manager.voidSnapshot, 6.5f);
        }

        void ExitVoid()
        {
            inVoid = false;
            manager.TransitionToSnapshot(manager.normalSnapshot, 6.5f);
            time = 0;
        }
    }
}
