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
    public FXsManager ScriptFXsManager;
    
    [SerializeField] private Transform modelTransform;
    [SerializeField] private float rotationSpeed = 8f;
    
    private Vector2 moveDirection;
    private float currentSpeed;

    void Awake()
    {
        ScriptFXsManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<FXsManager>();
    }
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
    }

    private void FixedUpdate()
    {
        if (stunJug || !potMoure)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
        if (stunJug)
        {
            ScriptFXsManager.PlayStunnedSound();
        }

        else if (potMoure)
        {
            Vector3 moviment = new Vector3(moveDirection.x, 0, moveDirection.y) * velocitat;
            moviment.y = rb.linearVelocity.y;
            rb.linearVelocity = moviment;
            /*if (moviment.x != 0 || moviment.z != 0)
            {
                ScriptFXsManager.PlayWalkSound();
            }
            else
            {
                ScriptFXsManager.StopWalkSound();
            }
            */

            if (moveDirection.magnitude > 0.1f)
            {
                Vector3 direccio = new Vector3(moveDirection.x, 0, moveDirection.y);
                Quaternion desti = Quaternion.LookRotation(direccio);
                modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, desti, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}