using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SencillColpejar : MonoBehaviour
{
    public BoxCollider Arma;
    public InputActionReference cop;
    public Animator animator;

    private bool jaHaColpejat = false;

    public int propietariaArma;

    public Moviment_jugadora ScriptMoviment1;
    public Moviment_jugadora ScriptMoviment2;

    [Header("AudioClips")]
    public AudioClip swingSound;
    public AudioClip bonkSound;
    public GestioSo gestioSo;


    void Start()
    {
        Arma.enabled = false;

        // Aseguramos que el collider NO es trigger
        Arma.isTrigger = false;

        if (gameObject.CompareTag("Arma_1"))
            propietariaArma = 1;
        else if (gameObject.CompareTag("Arma_2"))
            propietariaArma = 2;

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

        yield return new WaitForSeconds(2f);

        Arma.enabled = false;
        animator.SetBool("Colpejar", false);
        ScriptMoviment1.potMoure = true;
        ScriptMoviment2.potMoure = true;
        jaHaColpejat = false;
    }

    void OnCollisionEnter(Collision other)
    {
        // Evitar múltiples impactos en el mismo ataque
        if (jaHaColpejat) return;

        if (other.gameObject.CompareTag("Player1") && propietariaArma == 2)
        {
            jaHaColpejat = true;
            ScriptMoviment1.stunJug = true;
            Debug.Log("Stun player 1");
            GestioSo.instance.PlaySound(bonkSound, transform, 1f);
            Invoke("DesactivarStun", 2f);
            ScriptMoviment1.animator.SetBool("Emputjar", true);
        }
        else if (other.gameObject.CompareTag("Player2") && propietariaArma == 1)
        {
            jaHaColpejat = true;
            ScriptMoviment2.stunJug = true;
            Debug.Log("Stun player 2");
            GestioSo.instance.PlaySound(bonkSound, transform, 1f);
            Invoke("DesactivarStun", 2f);
            ScriptMoviment2.animator.SetBool("Emputjar", true);
        }
    }

    void DesactivarArma()
    {
        Arma.enabled = false;
        animator.SetBool("Colpejar", false);
    }

    public void ColpejarObjecte(GameObject objecteColpejat, GameObject edificiCapitalista, GameObject edificiComunista, GameObject dragg, int propietaria)
    {
        if (objecteColpejat.name == "Jugadora 1" && propietariaArma == 2)
        {
            ScriptMoviment1.stunJug = true;
            Debug.Log("Stun player 1");
            Invoke("DesactivarStun", 2f);
            ScriptMoviment1.animator.SetBool("Emputjar", true);
        }
        else if (objecteColpejat.name == "Jugadora 2" && propietariaArma == 1)
        {
            ScriptMoviment2.stunJug = true;
            Debug.Log("Stun player 2");
            Invoke("DesactivarStun", 2f);
            ScriptMoviment2.animator.SetBool("Emputjar", true);
        }
    }

    public void DesactivarStun()
    {
        if (ScriptMoviment1.stunJug)
        {
            ScriptMoviment1.stunJug = false;
            Debug.Log("Desactivar stun player 1");
            ScriptMoviment1.animator.SetBool("Emputjar", false);
        }

        if (ScriptMoviment2.stunJug)
        {
            ScriptMoviment2.stunJug = false;
            Debug.Log("Desactivar stun player 2");
            ScriptMoviment2.animator.SetBool("Emputjar", false);
        }
    }
}