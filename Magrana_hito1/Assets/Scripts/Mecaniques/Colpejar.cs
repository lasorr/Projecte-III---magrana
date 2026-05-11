using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Arma;
    public BoxCollider JugaBox; 
    public InputActionReference cop;
    public Animator animator;

    public int edificisTransformatJug1 = 0;
    public int edificisTransformatJug2 = 0;

    void Start()
    {
        Arma.enabled = false;
    }

    void Update()
    {
        if (cop.action.WasPressedThisFrame())
        {
            animator.SetBool("Colpejar", true);
            Arma.enabled = true;
            JugaBox.enabled = false;
            Invoke("DesactivarArma", 1.0f);
        }
    }

    void DesactivarArma()
    {
        Arma.enabled = false;
        animator.SetBool("Colpejar", false);
        JugaBox.enabled = true;
    }

    // Funció amb parametres de ContadorCops
    public void ColpejarObjecte(GameObject objecteColpejat, GameObject edificiCapitalista, GameObject edificiBo, GameObject dragg)
    {
        Debug.Log("Colpejant: " + objecteColpejat.name);
        
        // Cas MONJA
        if (objecteColpejat.CompareTag("Monja"))
        {
            if (dragg != null)
            {
                Vector3 pos = objecteColpejat.transform.position;
                Quaternion rot = objecteColpejat.transform.rotation;
                Destroy(objecteColpejat);
                Instantiate(dragg, pos, rot);
            }
            else
            {
                Debug.LogError("La monja no té Dragg assignat!");
            }
        }
        
        // Cas ESPECIAL
        else if (objecteColpejat.CompareTag("Especial"))
        {
            if (edificiCapitalista != null && edificiBo != null)
            {
                Vector3 pos = edificiCapitalista.transform.position;
                Quaternion rot = edificiCapitalista.transform.rotation;
                Destroy(edificiCapitalista);
                Destroy(objecteColpejat);
                Instantiate(edificiBo, pos, rot);

                // Incrementar el comptador d'edificis transformats
                if (gameObject.CompareTag("Arma_1"))
                {
                    edificisTransformatJug1++;
                }
                else if (gameObject.CompareTag("Arma_2"))
                {
                    edificisTransformatJug2++;
                }
            }
            else
            {
                Debug.LogError("L'objecte especial no té edificis assignats!");
            }
        }
    }
}