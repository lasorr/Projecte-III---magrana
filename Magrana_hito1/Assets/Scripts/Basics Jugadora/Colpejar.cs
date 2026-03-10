using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    // El mínim necessari
    public InputActionReference atac;
    public Animator animador;
    public GameObject colliderAtac;
    public GameObject objecteColor;

    private Renderer renderObjectiu;
    private Color colorOriginal;

    void Start()
    {
        // Guardem color original
        if (objecteColor != null)
        {
            renderObjectiu = objecteColor.GetComponent<Renderer>();
            colorOriginal = renderObjectiu.material.color;
        }
        
        colliderAtac.SetActive(false); // Desactivat al començar
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
        // 1. Animació
        if (animador != null) animador.SetTrigger("Atacar");
        
        // 2. Activar collider
        colliderAtac.SetActive(true);
        
        // 3. Desactivar collider després
        Invoke(nameof(DesactivarAtac), 0.3f);
    }

    void DesactivarAtac()
    {
        colliderAtac.SetActive(false);
    }

    // Quan el collider toca alguna cosa
    void OnTriggerEnter(Collider other)
    {
        // Si el que toca és el nostre objecte
        if (other.gameObject == objecteColor)
        {
            // Canviar color
            renderObjectiu.material.color = Color.green;
            
            // Tornar al color original després
            Invoke(nameof(RestaurarColor), 0.2f);
        }
    }

    void RestaurarColor()
    {
        renderObjectiu.material.color = colorOriginal;
    }
}