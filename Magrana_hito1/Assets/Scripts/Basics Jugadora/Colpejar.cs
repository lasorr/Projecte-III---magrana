using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Jugadora;
    public BoxCollider ObjecteEspecial;
    public GameObject Edifici;
    public GameObject Monja;
    public Material colorVerd;
    
        private Renderer rendererEdifici;
    private Renderer rendererMonja;

    void Start()
    {
        if (Edifici != null)
            rendererEdifici = Edifici.GetComponent<Renderer>();
            
        if (Monja != null)
            rendererMonja = Monja.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con ObjecteEspecial, cambiar color del Edifici
        if (ObjecteEspecial != null && other == ObjecteEspecial)
        {
            Debug.Log("¡Golpeado ObjecteEspecial!");
            if (rendererEdifici != null && colorVerd != null)
                rendererEdifici.material = colorVerd;
        }

        // Si colisiona con Monja, cambiar color de la Monja
        if (Monja != null)
        {
            Collider colliderMonja = Monja.GetComponent<Collider>();
            if (other == colliderMonja)
            {
                Debug.Log("¡Yassificada!");
                if (rendererMonja != null && colorVerd != null)
                    rendererMonja.material = colorVerd;
            }
        }
    }
}