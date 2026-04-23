using UnityEngine;

public class ContadorCops : MonoBehaviour
{
    // Contadors separats per cada jugadora
    private int copsJug1 = 0;
    private int copsJug2 = 0;
    private const int copsNecessaris = 3;

    // SUPERSTAR per cada jugadora (ARA AQUÍ!)
    private bool superstarJug1 = false;
    private bool superstarJug2 = false;
    
    // Paràmetres per MONJA
    public GameObject draggAlTransformar;
    
    // Paràmetres per ESPECIAL
    public GameObject edificiCapitalistaAssociat;
    public GameObject edificiBoAssociat;
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HI ha collisio");
        // Comprovar quina jugadora ha colpejat
        if (collision.gameObject.CompareTag("Arma_1"))
        {
            copsJug1++;
            Debug.Log(gameObject.name + " rebut cop de J1: " + copsJug1 + "/" + copsNecessaris);
            
            if (superstarJug1 || copsJug1 >= copsNecessaris)
            {
                ActivarTransformacio(collision); 
                copsJug1 = 0;
                superstarJug1 = false; // Es gasta el superstar
            }
        }
        else if (collision.gameObject.CompareTag("Arma_2"))
        {
            copsJug2++;
            Debug.Log(gameObject.name + " rebut cop de J2: " + copsJug2 + "/" + copsNecessaris);
            
            if (superstarJug2 || copsJug2 >= copsNecessaris)
            {
                ActivarTransformacio(collision); 
                copsJug2 = 0;
                superstarJug2 = false; // es gasta el superstar
            }
        }
    }
    
    void ActivarTransformacio(Collision collision)
    {
        Colpejar colpejarScript = collision.gameObject.GetComponentInParent<Colpejar>();
        
        if (colpejarScript != null)
        {
            colpejarScript.ColpejarObjecte(
                this.gameObject,
                edificiCapitalistaAssociat,
                edificiBoAssociat,
                draggAlTransformar
            );
        }
        else
        {
            Debug.Log("Desde contadors cops no reb colpejar script");
        }
    }

    public void ActivarSuperstarJug1()
    {
        superstarJug1 = true;
        Debug.Log("SUPERSTAR activat per J1 a " + gameObject.name);
        Invoke("DesactivarSuperstarJug1", 10f);
    }
    
    public void ActivarSuperstarJug2()
    {
        superstarJug2 = true;
        Debug.Log("SUPERSTAR activat per J2 a " + gameObject.name);
        Invoke("DesactivarSuperstarJug2", 10f);
    }
    
    void DesactivarSuperstarJug1()
    {
        superstarJug1 = false;
        Debug.Log("SUPERSTAR desactivat per J1 a " + gameObject.name);
    }
    
    void DesactivarSuperstarJug2()
    {
        superstarJug2 = false;
        Debug.Log("SUPERSTAR desactivat per J2 a " + gameObject.name);
    }
}