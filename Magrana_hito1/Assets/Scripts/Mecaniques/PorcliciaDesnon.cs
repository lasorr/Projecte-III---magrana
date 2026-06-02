using UnityEngine;

public class PorcliciaDesnon : MonoBehaviour
{
    //a la monja li es de donar igual l'edifici , mana el valor de derrotades al objecte especial 
    private EdificiEspecialDesnon objecteEspecialDesnon;

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

        Destroy(gameObject);
    }
}