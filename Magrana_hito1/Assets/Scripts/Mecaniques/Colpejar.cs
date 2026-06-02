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
    public ContadorCops ScriptContador;
    
    private bool jaHaColpejat = false;

    public int propietariaArma;

    public Moviment_jugadora ScriptMoviment1;
    public Moviment_jugadora ScriptMoviment2;

    public IAEnemicPorclicia IAporcilia;

    [Header("AudioClips")]
    public AudioClip swingSound;
    public AudioClip bonkSound;
    public AudioClip porcliciaDamageSound;
    public GestioSo gestioSo;

    public TimeManager TimeManager;

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
            GestioSo.instance.PlaySound(swingSound, transform, 1f);
            StartCoroutine(Atac());
        }
    }

    IEnumerator Atac()
    {
        animator.SetBool("Colpejar", true);

        jaHaColpejat = false;
        ScriptMoviment1.potMoure = false;
        ScriptMoviment2.potMoure = false;
        Arma.enabled = true;

        // temps real d’impacte (AJUSTA A L’ANIMACIÓ)
        yield return new WaitForSeconds(2f);

        Arma.enabled = false;

        animator.SetBool("Colpejar", false);
        ScriptMoviment1.potMoure = true;
        ScriptMoviment2.potMoure = true;
        jaHaColpejat = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (jaHaColpejat) return;
        if (!Arma.enabled) return;

        if (other.CompareTag("Especial")) //envia a contador cops
        {
            jaHaColpejat = true;

            ContadorCops contadorScript = other.GetComponent<ContadorCops>();

            if (contadorScript != null)
            {
                contadorScript.RebreCopEdifici(propietariaArma, 1); //augmenta la variable perque es faci el canvi
            }
            else
            {
                Debug.Log("Desde colpejar no reb contador cops script");
            }
            ColpejarObjecte(
                other.gameObject,
                null,   // edificiCapitalista (mateix objecte si no tens jerarquia separada)
                null,
                null,
                0
            );
        }

        else if (other.CompareTag("EspecialDesnon")) //tag del objecte especial desnonament
        {
            jaHaColpejat = true;

            EdificiEspecialDesnon desnonScript = other.GetComponent<EdificiEspecialDesnon>();

            if (desnonScript != null)
            {
                desnonScript.RebreCopEdificiEspecial(propietariaArma, 3);
            }
            else
            {
                Debug.Log("Desde colpejar no reb Edifici calvo");
            }
        }

        else if (other.CompareTag("EspecialMonja")) //tag del objecte especial cole catolic
        {
            jaHaColpejat = true;

            EdificiEspecialTrans transScript = other.GetComponent<EdificiEspecialTrans>();

            if (transScript != null)  //
            {
                transScript.RebreCopEdificiEspecial(propietariaArma, 3);
            }
            else
            {
                Debug.Log("Desde colpejar no reb contador cops script MONJAX");
            }
        }

        else if (other.CompareTag("Monja")) 
        {
            jaHaColpejat = true;
  
            other.GetComponent<Monjes>().Morir();
        }

        else if (other.CompareTag("porclicia_desnon")) //tag porclicies per desbloquejar
        {
            jaHaColpejat = true;

            other.GetComponent<PorcliciaDesnon>().Derrotada();
        }

        else if (other.CompareTag("Porclicia")) //porcs ai per evitar transform
        {
            jaHaColpejat = true;

            IAporcilia.copsRebuts++;

            GestioSo.instance.PlaySound(porcliciaDamageSound, transform, 1f); // SO DANY PORCILIA

            Debug.Log("Porcilia ha rebut un cop! Cops rebuts: " + IAporcilia.copsRebuts);
            ColpejarObjecte(
                other.gameObject,
                null,
                null,
                ScriptContador.getdragg(), // assigna el prefab de Dragg a l'inspectors
                0
            );
        }

        else if (other.CompareTag("Player1"))
        {
            jaHaColpejat = true;

            Debug.Log("Collision player 1");

            if (propietariaArma == 2)
            {
                ScriptMoviment1.stunJug = true;

                Debug.Log("Stun player 1");

                GestioSo.instance.PlaySound(bonkSound, transform, 1f);
                
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

                GestioSo.instance.PlaySound(bonkSound, transform, 1f);
            
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
    public void ColpejarObjecte(GameObject objecteColpejat, GameObject edificiCapitalista, GameObject edificiComunista, GameObject dragg, int propietaria)
    {
        Debug.Log("Colpejant: " + objecteColpejat.name);
        
        // Cas ESPECIAL
        if (objecteColpejat.CompareTag("Especial"))
        {
            if (edificiCapitalista != null && edificiComunista != null)
            {
                Vector3 pos = edificiCapitalista.transform.position;
                Quaternion rot = edificiCapitalista.transform.rotation;
                Destroy(edificiCapitalista);
                Destroy(objecteColpejat);
                GameObject nouEdifici = Instantiate(edificiComunista, pos, rot);

                if (gameObject.CompareTag("Arma_1"))
                {
                    TimeManager.Instance.edificisTransformatJug1++;
                }
                else if (gameObject.CompareTag("Arma_2"))
                {
                    TimeManager.Instance.edificisTransformatJug2++;
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
                ScriptMoviment1.stunJug = true;

                Debug.Log("Stun player 1");
                
                Invoke("DesactivarStun", 2f);
                ScriptMoviment1.animator.SetBool("Emputjar", true);
            }
        }

        else if (objecteColpejat.name == "Jugadora 2")
        {
            Debug.Log("Find player 2");
            if (propietariaArma == 1)
            {
                ScriptMoviment2.stunJug = true;

                Debug.Log("Stun player 2");
            
                Invoke("DesactivarStun", 2f);
                ScriptMoviment2.animator.SetBool("Emputjar", true);
            }
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