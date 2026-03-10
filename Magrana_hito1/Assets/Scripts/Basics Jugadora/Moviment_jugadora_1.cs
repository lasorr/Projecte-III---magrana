using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora_1 : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    
    public InputActionReference move;
    public InputActionReference attack;
    
    // Variable per guardar la direcció
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Llegim el valor de l'acció
        if (move != null)
        {
            moveDirection = move.action.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        // CORRECCIÓ: Utilitzem moveDirection (Vector2) correctament
        Vector3 moviment = new Vector3(moveDirection.x, 0, moveDirection.y) * velocitat;
        
        // Mantenim la velocitat vertical actual (per gravetat)
        moviment.y = rb.linearVelocity.y;
        
        rb.linearVelocity = moviment;
    }
}