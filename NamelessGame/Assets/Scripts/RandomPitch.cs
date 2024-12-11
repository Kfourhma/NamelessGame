using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioSource voidSource;
    public AudioManager AudioManager;

    public int arraySize = 500; // Size of the array
    public int minValue = 1;   // Minimum random value
    public int maxValue = 500; // Maximum random value
    private int[] randomNumbers;

    private float currentTime = 0;

    float timer;

    private void Start()
    {
        randomNumbers = new int[arraySize];

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            randomNumbers[i] = Random.Range(minValue, maxValue);
        }

        

    }
    // Update is called once per frame
    void Update()
    {
        var myIndex = Random.Range(0, arraySize);

        currentTime += Time.deltaTime;



        if (currentTime >= myIndex)
        {
            if (AudioManager.GetCurrentSnapshot() == AudioManager.voidSnapshot)
            {
                voidSource.pitch = Random.Range(0f, 1f);
                voidSource.Play();
                currentTime = 0;
                myIndex = Random.Range(0, arraySize);
            }
            else
            {
                AudioSource.pitch = Random.Range(0f, 1f);
                AudioSource.Play();
                currentTime = 0;
                myIndex = Random.Range(0, arraySize);
            }
        }
    }

    /*
     * A timer will count to targetTime
     * each time it reaches targetTime it will reset to zero, and targetTime will be randomized via array
     */
}
