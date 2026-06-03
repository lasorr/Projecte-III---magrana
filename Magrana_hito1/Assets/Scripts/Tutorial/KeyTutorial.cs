using System.Collections;
using UnityEngine;

public class KeyTutorial : MonoBehaviour
{
    public GameObject Cadenas;
    public GameObject pOscuras;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arma_1") || collision.gameObject.CompareTag("Arma_2"))
        {
            // Destruir las cadenas inmediatamente
            Destroy(Cadenas);

            // Llamar al método DestruirLlave después de 1 segundo
            Invoke("DestruirLlave", 0.5f);
        }
    }

    void DestruirLlave()
    {
        Instantiate(pOscuras, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}