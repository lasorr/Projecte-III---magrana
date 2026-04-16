using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Si la jugadora toca el objeto con tag "Star"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Buscar el script Colpejar en la jugadora (o en el padre)
            Colpejar colpejarScript = collision.gameObject.GetComponentInChildren<Colpejar>();
            
            // Si encuentra el script, activar el power-up
            if (colpejarScript != null)
            {
                colpejarScript.ActivarPowerUp();
                Destroy(collision.gameObject); // Destruir el power-up
            }
            else
            {
                Debug.LogWarning("No se encontró el script Colpejar en la jugadora");
            }
        }
    }
}