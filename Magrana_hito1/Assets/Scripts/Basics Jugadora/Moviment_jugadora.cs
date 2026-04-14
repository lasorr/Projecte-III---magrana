using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    public InputActionReference move;
    public Animator animator;
    
    [SerializeField] private Transform modelTransform; // arrossega el empty object que contengui els objectes player
    [SerializeField] private float rotationSpeed = 8f; // velocitat de gir
    
    private Vector2 moveDirection;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (move != null)
        {
            moveDirection = move.action.ReadValue<Vector2>();
        }

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        currentSpeed = horizontalVelocity.magnitude / velocitat; // Normalizado entre 0 y 1
        animator.SetFloat("Moviment", currentSpeed);
            
    }

    private void FixedUpdate()
    {
        // Moviment
        Vector3 moviment = new Vector3(moveDirection.x, 0, moveDirection.y) * velocitat;
        moviment.y = rb.linearVelocity.y;
        rb.linearVelocity = moviment;

        // Gir visual progressiu (només el model)
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 direccio = new Vector3(moveDirection.x, 0, moveDirection.y);
            Quaternion desti = Quaternion.LookRotation(direccio);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, desti, rotationSpeed * Time.fixedDeltaTime);
        }
    }
    
}