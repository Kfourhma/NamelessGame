using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the "Player" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
