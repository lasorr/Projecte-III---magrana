using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora_2 : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    
    public InputActionReference move2;
    public InputActionReference attack2;
    
    // Variable per guardar la direcció
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Llegim el valor de l'acció
        if (move2 != null)
        {
            moveDirection = move2.action.ReadValue<Vector2>();
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