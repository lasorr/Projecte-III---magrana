using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Arma; 
    public InputActionReference cop;
    public Animator animator;

    public Moviment_jugadora ScriptMovimentJug1;
    public Moviment_jugadora ScriptMovimentJug2;
    public int edificisTransformatJug1 = 0;
    public int edificisTransformatJug2 = 0;

    public PropietariaEdifici ScriptPropietaria;
    
    private bool jaHaColpejat = false;

    public  int propietariaArma;

    void Start()
    {
        Arma.enabled = false;
        if (gameObject.CompareTag("Arma_1"))
        {
            propietariaArma = 1;
        }
        else if (gameObject.CompareTag("Arma_2"))
        {
            propietariaArma = 2;
        }

        Debug.Log("Propietaria de l'arma: " + propietariaArma);
    }

    void Update()
    {
        if (cop.action.WasPressedThisFrame())
        {
            StartCoroutine(Atac());
        }
    }

    IEnumerator Atac()
    {
        animator.SetBool("Colpejar", true);

        jaHaColpejat = false;
        Arma.enabled = true;

        // temps real d’impacte (AJUSTA A L’ANIMACIÓ)
        yield return new WaitForSeconds(0.15f);

        Arma.enabled = false;

        animator.SetBool("Colpejar", false);
        jaHaColpejat = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (jaHaColpejat) return;
        if (!Arma.enabled) return;

        if (other.CompareTag("Especial"))
        {
            jaHaColpejat = true;

            // EXEMPLE SIMPLE: agafem components del mateix objecte
            GameObject obj = other.gameObject;

            ColpejarObjecte(
                obj,
                obj,   // edificiCapitalista (mateix objecte si no tens jerarquia separada)
                null,
                null,
                0
            );
        }

        else if (other.CompareTag("Monja"))
        {
            jaHaColpejat = true;

            ColpejarObjecte(
                other.gameObject,
                null,
                null,
                null,
                0
            );
        }

        else if (other.CompareTag("Player1"))
        {
            jaHaColpejat = true;

            ColpejarObjecte(
                other.gameObject,
                null,
                null,
                null,
                0
            );
        }

        else if (other.CompareTag("Player2"))
        {
            jaHaColpejat = true;

            ColpejarObjecte(
                other.gameObject,
                null,
                null,
                null,
                0
            );
        }
    }

    void DesactivarArma()
    {
        Arma.enabled = false;
        animator.SetBool("Colpejar", false);
    }

    // Funció amb parametres de ContadorCops
    public void ColpejarObjecte(GameObject objecteColpejat, GameObject edificiCapitalista, GameObject edificiBo, GameObject dragg, int propietaria)
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
                GameObject nouEdifici = Instantiate(edificiBo, pos, rot);

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

        else if (objecteColpejat.name == "Jugadora 1")
        {
            Debug.Log("Find player 1");
            if (propietariaArma == 2)
            {
                ScriptMovimentJug1.stunJug = true;

                Debug.Log("Stun player 1");
                
                Invoke("DesactivarStun", 2f);
                ScriptMovimentJug1.animator.SetBool("Emputjar", true);
            }
        }

        else if (objecteColpejat.name == "Jugadora 2")
        {
            Debug.Log("Find player 2");
            if (propietariaArma == 1)
            {
                ScriptMovimentJug2.stunJug = true;

                Debug.Log("Stun player 2");
            
                Invoke("DesactivarStun", 2f);
                ScriptMovimentJug2.animator.SetBool("Emputjar", true);
            }
        }
    }

    public void DesactivarStun()
    {
        if (ScriptMovimentJug1.stunJug)
        {
            ScriptMovimentJug1.stunJug = false;
            Debug.Log("Desactivar stun player 1");

            Debug.Log("Entrar void desactivar emputjar (animacio)");
            ScriptMovimentJug1.animator.SetBool("Emputjar", false);
        }

        if (ScriptMovimentJug2.stunJug)        
        {
            ScriptMovimentJug2.stunJug = false;
            Debug.Log("Desactivar stun player 2");

            Debug.Log("Entrar void desactivar emputjar (animacio)");
            ScriptMovimentJug2.animator.SetBool("Emputjar", false);
        }

        Debug.Log("Desactivar stun");
    }
}