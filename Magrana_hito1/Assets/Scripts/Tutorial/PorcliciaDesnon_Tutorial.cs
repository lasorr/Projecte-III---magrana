using UnityEngine;

public class PorcliciaDesnon_Tutorial : MonoBehaviour
{
    //a la monja li es de donar igual l'edifici , mana el valor de derrotades al objecte especial 
    private EdificiEspecialDesnon_Tutorial objecteEspecialDesnon;

    public GameObject particulasPorclicia;

    void Start()
    {
        objecteEspecialDesnon = GetComponentInParent<EdificiEspecialDesnon_Tutorial>();
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