using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    AudioSource footsteps;
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public Transform camRef;

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 100.0f;

    private Vector3 velocity;

    private float xRotation = 0f;

    private CharacterController character;

    private 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        footsteps = GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
    }
    void Update()
    {
        // Handles Mouse Look
        var lookInput = lookAction.action.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        camRef.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        // Handles Movement
        var direction = moveAction.action.ReadValue<Vector2>();
        Vector3 move = (camRef.forward * direction.y + camRef.right * direction.x).normalized;

        move.y = 0f;

        character.Move(move * movementSpeed * Time.deltaTime);

        if(direction != Vector2.zero && !footsteps.isPlaying)
        {
            footsteps.pitch = Random.Range(0.8f, 1f);
            footsteps.panStereo = Random.Range(-0.15f, 0.15f);   
            footsteps.Play();
        }
        else if (direction ==  Vector2.zero && footsteps.isPlaying)
        {
            footsteps.Stop();
        }
    }
}
