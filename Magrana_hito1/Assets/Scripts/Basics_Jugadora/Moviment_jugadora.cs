using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    public InputActionReference move;
    public Animator animator;
    public bool potMoure = true;
    
    [SerializeField] private Transform modelTransform;
    [SerializeField] private float rotationSpeed = 8f;
    
    private Vector2 moveDirection;
    private float currentSpeed;
    public bool potMourePerTimer = true;  // ← NOVA VARIABLE

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (potMoure && potMourePerTimer && move != null)  // ← CANVIAT
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
        if (potMoure && potMourePerTimer)  // ← CANVIAT
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arma_1") || collision.gameObject.CompareTag("Arma_2"))
        {
            potMoure = false;
            animator.SetBool("Emputjar", true);
            Invoke("DesactivarEmputjar", 2f);
        }
    }

    void DesactivarEmputjar()
    {
        Debug.Log("Entrar void desactivar emputjar");
        animator.SetBool("Emputjar", false);
        potMoure = true;
    }
    
    // ← AFEGEIX AIXÒ
    public void SetMovimentPerTimer(bool value)
    {
        potMourePerTimer = value;
        Debug.Log($"Timer: potMourePerTimer = {potMourePerTimer}");
    }
}