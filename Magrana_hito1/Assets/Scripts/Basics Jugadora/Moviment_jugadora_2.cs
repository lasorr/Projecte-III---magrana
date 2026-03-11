using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Moviment_jugadora_2 : MonoBehaviour
{
    private Rigidbody rb;
    public float velocitat = 5f;
    
    public InputActionReference move2;
    public InputActionReference attack2;
    
    public Animator animator;
    public GameObject weaponMesh;
    
    private Vector2 moveDirection;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    public float attackCooldown = 0.5f;
    public float weaponActiveTime = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // CORREGIDO: Cambiar 'attack' por 'attack2'
        if (attack2 != null)
            attack2.action.performed += Attack;
            
        if (weaponMesh != null)
            weaponMesh.SetActive(false);
    }

    void OnDestroy()
    {
        // CORREGIDO: Cambiar 'attack' por 'attack2'
        if (attack2 != null)
            attack2.action.performed -= Attack;
    }

    void Update()
    {
        if (move2 != null)
        {
            moveDirection = move2.action.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        Vector3 moviment = new Vector3(moveDirection.x, 0, moveDirection.y) * velocitat;
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