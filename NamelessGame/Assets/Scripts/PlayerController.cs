using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.PlayerActions input;

    AudioSource footsteps;
    public AudioSource audioSource;
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public Transform camRef;

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 100.0f;

    private Vector3 velocity;

    private float xRotation = 0f;

    private CharacterController character;

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    Animator animator;

    //Animations
    public const string IDLE = "Idle";
    public const string ATTACK1 = "Attack 1";

    string currentAnimationState;


    

    private void Awake()
    {
        animator = GetComponent<Animator>();

        playerInput = new PlayerInput();
        input = playerInput.Player;
        AssignInput();
    }
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

        if(input.Attack.IsPressed())
        {
            Attack();
        }

    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRayCast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void AssignInput()
    {
        input.Attack.started += ctx => Attack();
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRayCast()
    {
        if (Physics.Raycast(camRef.transform.position, camRef.transform.forward,
            out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);

            if(hit.transform.TryGetComponent<EnemyAiTutorial>(out EnemyAiTutorial T))
            {
                T.TakeDamage(attackDamage);
            }
        }
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(hitSound);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;

        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }


}
