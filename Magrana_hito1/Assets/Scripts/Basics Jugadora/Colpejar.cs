using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Jugadora;
    public BoxCollider ObjecteEspecial;
    public GameObject Edifici;
    public Material colorVerd;
    
    private Renderer rendererObjectiu;
    private Material colorOriginal;

    void Start()
    {
        if (Edifici != null)
        {
            rendererObjectiu = Edifici.GetComponent<Renderer>();
            if (rendererObjectiu != null)
                colorOriginal = rendererObjectiu.material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == ObjecteEspecial)
        {
            Debug.Log("¡Golpeado!");
            if (rendererObjectiu != null && colorVerd != null)
                rendererObjectiu.material = colorVerd;
        }
    }
}