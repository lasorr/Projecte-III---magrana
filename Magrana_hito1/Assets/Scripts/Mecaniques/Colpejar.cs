using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Arma; 
    public InputActionReference cop;
    public Animator animator;

    public PropietariaEdifici ScriptPropietaria;

    public GameObject dragg;
    
    private bool jaHaColpejat = false;

    public  int propietariaArma;

    public Moviment_jugadora ScriptMoviment1;
    public Moviment_jugadora ScriptMoviment2;

    public EdificiEspecialTrans EdificiDragg;
    public EdificiEspecialDesnon EdificiCalvo;

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

            ContadorCops contadorScript = other.GetComponent<ContadorCops>();

            if (contadorScript != null)
            {
                contadorScript.RebreCopEdifici(propietariaArma, 1);
            }
            else
            {
                Debug.Log("Desde colpejar no reb contador cops script");
            }
        }

        else if (other.CompareTag("EspecialDesnon"))
        {
            jaHaColpejat = true;

            ContadorCops contadorScript = other.GetComponent<ContadorCops>();

            if (EdificiCalvo != null)
            {
                EdificiCalvo.RebreCopEdificiEspecial(propietariaArma, 3);
            }
            else
            {
                Debug.Log("Desde colpejar no reb contador cops script");
            }
        }

        else if (other.CompareTag("EspecialMonja"))
        {
            jaHaColpejat = true;

            ContadorCops contadorScript = other.GetComponent<ContadorCops>();

            if (EdificiDragg != null)
            {
                EdificiDragg.RebreCopEdificiEspecial(propietariaArma, 3);
            }
            else
            {
                Debug.Log("Desde colpejar no reb contador cops script");
            }
        }

        else if (other.CompareTag("Monja"))
        {
            jaHaColpejat = true;

            ColpejarObjecte(
                other.gameObject,
                null,
                null,
                dragg,
                0
            );

            EdificiDragg.monjesDerrotades++;
        }

        else if (other.CompareTag("porclicia_desnon"))
        {
            jaHaColpejat = true;

            Destroy(other.gameObject);

            EdificiCalvo.polisDerrotats++;
        }

        else if (other.CompareTag("Player1"))
        {
            jaHaColpejat = true;

            Debug.Log("Collision player 1");

            if (propietariaArma == 2)
            {
                ScriptMoviment1.stunJug = true;

                Debug.Log("Stun player 1");
                
                Invoke("DesactivarStun", 2f);
                ScriptMoviment1.animator.SetBool("Emputjar", true);
            }
        }

        else if (other.CompareTag("Player2"))
        {
            jaHaColpejat = true;


            Debug.Log("Collision player 2");
            if (propietariaArma == 1)
            {
                ScriptMoviment2.stunJug = true;

                Debug.Log("Stun player 2");
            
                Invoke("DesactivarStun", 2f);
                ScriptMoviment2.animator.SetBool("Emputjar", true);
            }
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
            Vector3 pos = objecteColpejat.transform.position;
            Quaternion rot = objecteColpejat.transform.rotation;

            Destroy(objecteColpejat);

            Instantiate(dragg, pos, rot);
        }
        
        // Cas ESPECIAL
        else if (objecteColpejat.CompareTag("Especial"))
        {
            Vector3 pos = edificiCapitalista.transform.position;
            Quaternion rot = edificiCapitalista.transform.rotation;
            Destroy(edificiCapitalista);
            Destroy(objecteColpejat);
            GameObject nouEdifici = Instantiate(edificiBo, pos, rot);
        }
    }

    public void DesactivarStun()
    {
        if (ScriptMoviment1.stunJug)
        {
            ScriptMoviment1.stunJug = false;
            Debug.Log("Desactivar stun player 1");

            Debug.Log("Entrar void desactivar emputjar (animacio)");
            ScriptMoviment1.animator.SetBool("Emputjar", false);
        }

        if (ScriptMoviment2.stunJug)        
        {
            ScriptMoviment2.stunJug = false;
            Debug.Log("Desactivar stun player 2");

            Debug.Log("Entrar void desactivar emputjar (animacio)");
            ScriptMoviment2.animator.SetBool("Emputjar", false);
        }

        Debug.Log("Desactivar stun");
    }
}