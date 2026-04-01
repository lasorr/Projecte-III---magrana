using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    
    public InputActionReference move;
    
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Llegim el valor de l'acció de moviment
        if (move != null)
        {
            moveDirection = move.action.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        // Construïm el vector de moviment en funció de l'entrada
        Vector3 moviment = new Vector3(moveDirection.x, 0, moveDirection.y) * velocitat;
        
        // Mantenim la velocitat vertical actual (per si hi ha salts o gravetat)
        moviment.y = rb.linearVelocity.y;
        
        // Apliquem la velocitat al Rigidbody
        rb.linearVelocity = moviment;
    }
}