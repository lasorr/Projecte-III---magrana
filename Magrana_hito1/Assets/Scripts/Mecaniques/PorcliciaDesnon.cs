using UnityEngine;

public class PorcliciaDesnon : MonoBehaviour
{
    //a la monja li es de donar igual l'edifici , mana el valor de derrotades al objecte especial 
    private EdificiEspecialDesnon objecteEspecialDesnon;

    public GameObject particulasPorclicia;

    void Start()
    {
        objecteEspecialDesnon = GetComponentInParent<EdificiEspecialDesnon>();
    }

    public void Derrotada()
    {
        if (objecteEspecialDesnon != null)
        {
            objecteEspecialDesnon.RegistrarPorcDerrotat();
        }
    
        GameObject particulasOscuras = Instantiate(particulasPorclicia, transform.position, Quaternion.identity);
        Destroy(particulasOscuras, 2f);

        Destroy(gameObject);
    }
}