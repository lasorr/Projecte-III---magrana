using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Moviment_jugadora_1 : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    
    
    public InputActionReference move;
    public InputActionReference attack;

    public Animator animator; // Arrastra aquí el Animator
    public GameObject weaponMesh; // Arrastra aquí el mesh del arma
    
    private Vector2 moveDirection;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    public float attackCooldown = 0.5f;
    public float weaponActiveTime = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

                if (attack != null)
            attack.action.performed += Attack;
            
        if (weaponMesh != null)
            weaponMesh.SetActive(false);
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

    private void Attack(InputAction.CallbackContext obj)
    {
        if (Time.time < nextAttackTime) return;
        
        Debug.Log("Atacando");
        nextAttackTime = Time.time + attackCooldown;
        
        if (animator != null)
            animator.SetTrigger("Attack");
        
        if (weaponMesh != null)
            StartCoroutine(ActivateWeapon());
    }
    
    private IEnumerator ActivateWeapon()
    {
        isAttacking = true;
        weaponMesh.SetActive(true);
        yield return new WaitForSeconds(weaponActiveTime);
        weaponMesh.SetActive(false);
        isAttacking = false;
    }
}