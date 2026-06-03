using System.Collections;
using UnityEngine;

public class KeyTutorial : MonoBehaviour
{
    public GameObject Cadenas;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arma_1") || collision.gameObject.CompareTag("Arma_2"))
        {
            // Destruir las cadenas inmediatamente
            Destroy(Cadenas);

            // Llamar al método DestruirLlave después de 1 segundo
            Invoke("DestruirLlave", 1f);
        }
    }

    void DestruirLlave()
    {
        Destroy(gameObject);
    }
}