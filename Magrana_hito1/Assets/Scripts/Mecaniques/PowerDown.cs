using UnityEngine;

public class PowerDown : MonoBehaviour
{
    public GameObject Jug1;
    public GameObject Jug2;
    public GameObject Power;
    private Moviment_jugadora moviment;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            moviment = Jug1.GetComponent<Moviment_jugadora>();
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            moviment = Jug2.GetComponent<Moviment_jugadora>();
        }
        
        if (moviment != null)
        {
            moviment.potMoure = false;  // Accedeix directament al bool
            Invoke("ActivarMoviment", 3f);
        }
        
        
    }
    void ActivarMoviment()
    {
        if (moviment != null) 
        {
            Debug.Log("Entra al void activar moviment");
            moviment.potMoure = true;
        }
        else
        {
            Debug.Log("No troba el script moviment_jug");
        }
        Destroy(Power);
    }
    
}