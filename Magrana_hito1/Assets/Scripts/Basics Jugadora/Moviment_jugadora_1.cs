using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment_jugadora_1 : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    public float velocitatGir = 100f;
    
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
        // 1. GIRAR amb A/D (moveDirection.x)
        float gir = moveDirection.x * velocitatGir * Time.fixedDeltaTime;
        rb.rotation *= Quaternion.Euler(0, gir, 0);
        
        // 2. MOVIMENT endavant/endarrere amb W/S (moveDirection.y)
        float movimentEndavant = moveDirection.y * velocitat * Time.fixedDeltaTime;
        
        // Moure en la direcció on està mirant el personatge (transform.forward)
        Vector3 desplacament = transform.forward * movimentEndavant;
        
        // Aplicar moviment mantenint la gravetat
        Vector3 novaVelocitat = new Vector3(desplacament.x, rb.linearVelocity.y, desplacament.z);
        rb.linearVelocity = novaVelocitat;
    }


    private void Attack (InputAction.CallbackContext obj)
    {
        Debug.Log("Atacando");
        //llamar a la animacion atacar
        //on off el mesh de l'arma?? per quan colisioni amb el objecte
        //no sigui per estar al costat sino per haver atacat?
        
    }
}