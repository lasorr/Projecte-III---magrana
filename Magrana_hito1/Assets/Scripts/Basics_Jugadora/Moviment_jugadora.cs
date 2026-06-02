using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    public InputActionReference move;
    public Animator animator;
    public bool potMoure = true;

    public bool stunJug = false;
    
    [SerializeField] private Transform modelTransform;
    [SerializeField] private float rotationSpeed = 8f;
    
    private Vector2 moveDirection;
    private float currentSpeed;

    [Header("AudioClips")]
    public AudioClip stunSound;
    public AudioClip walkingSound;
    private bool isWalkingSound = false;
    private bool stunSoundPlayed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (potMoure && move != null)
        {
            moveDirection = move.action.ReadValue<Vector2>();
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        currentSpeed = horizontalVelocity.magnitude / velocitat;
        animator.SetFloat("Moviment", currentSpeed);

        // SO PASSES
        bool isMoving = moveDirection.magnitude > 0.1f && potMoure && !stunJug; // Condicions per a determinar si la jugadora està movent-se
        if (isMoving && !isWalkingSound)
        {
            // GestioSo.instance.PlaySound(walkingSound, transform, 1f);
            isWalkingSound = true;
        }
        else if (!isMoving)
        {
            isWalkingSound = false;
        }

        // SO STUN
        if (stunJug && !stunSoundPlayed)
        {
            GestioSo.instance.PlaySound(stunSound, transform, 1f);
            stunSoundPlayed = true;
        }
        else if (!stunJug)
        {
            stunSoundPlayed = false; // Reiniciar para el próximo stun
        }
    }

    private void FixedUpdate()
    {
        if (stunJug || !potMoure)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        else if (potMoure)
        {
            Vector3 moviment = new Vector3(moveDirection.x, 0, moveDirection.y) * velocitat;
            moviment.y = rb.linearVelocity.y;
            rb.linearVelocity = moviment;

            if (moveDirection.magnitude > 0.1f)
            {
                Vector3 direccio = new Vector3(moveDirection.x, 0, moveDirection.y);
                Quaternion desti = Quaternion.LookRotation(direccio);
                modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, desti, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}