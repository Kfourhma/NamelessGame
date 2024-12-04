using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sunLight;
    public float timeOfDay = 120f;

    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float rotation = (time / timeOfDay) * 360f;

        sunLight.transform.rotation = Quaternion.Euler(rotation, 0, 0);

        if(time > timeOfDay)
        {
            time = 0;
        }
    }
}
