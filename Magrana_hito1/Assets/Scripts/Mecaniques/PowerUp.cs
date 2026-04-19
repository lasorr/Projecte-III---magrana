using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject Power;
    void OnCollisionEnter(Collision collision)
    {
        // Comprovar quina jugadora toca el power-up
        if (collision.gameObject.CompareTag("Player1"))
        {
            // Activar superstar a TOTS els objectes 
            ContadorCops[] totsElsContadors = FindObjectsByType<ContadorCops>(FindObjectsSortMode.None);
            foreach (ContadorCops contador in totsElsContadors)
            {
                contador.ActivarSuperstarJug1();
            }
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            ContadorCops[] totsElsContadors = FindObjectsByType<ContadorCops>(FindObjectsSortMode.None);
            foreach (ContadorCops contador in totsElsContadors)
            {
                contador.ActivarSuperstarJug2();
            }
            Destroy(Power);
        }
    }
}