using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    public InputActionReference atac;
    public Animator animador;
    public GameObject objecteAmbCollider;  // 👈 Ara és GameObject
    public GameObject objecteColor;

    private Collider colliderAtac;          // 👈 El guardem aquí dins
    private Renderer renderObjectiu;
    private Color colorOriginal;

    void Start()
    {
        // Agafem el collider de l'objecte
        if (objecteAmbCollider != null)
        {
            colliderAtac = objecteAmbCollider.GetComponent<Collider>();
            colliderAtac.enabled = false; // Desactivat al començar
        }
        
        if (objecteColor != null)
        {
            renderObjectiu = objecteColor.GetComponent<Renderer>();
            colorOriginal = renderObjectiu.material.color;
        }
    }

    void OnEnable()
    {
        atac.action.performed += PremF;
        atac.action.Enable();
    }

    void OnDisable()
    {
        atac.action.performed -= PremF;
    }

    void PremF(InputAction.CallbackContext context)
    {
        if (animador != null) animador.SetTrigger("attack_jug1");
        
        if (colliderAtac != null)
        {
            colliderAtac.enabled = true;           // 👈 Activar collider
            Invoke(nameof(DesactivarAtac), 0.3f);
        }
    }

    void DesactivarAtac()
    {
        if (colliderAtac != null)
            colliderAtac.enabled = false;          // 👈 Desactivar collider
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objecteColor)
        {
            renderObjectiu.material.color = Color.red;
            Invoke(nameof(RestaurarColor), 0.2f);
        }
    }

    void RestaurarColor()
    {
        renderObjectiu.material.color = colorOriginal;
    }
}