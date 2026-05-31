using UnityEngine;

public class Monjes : MonoBehaviour
{
    //a la monja li es de donar igual l'edifici , mana el valor de derrotades al objecte especial 
    private EdificiEspecialTrans edificiPare; //canviar el nom confon
    //fer public la refrencai a - Objecte especial pare

    public GameObject dragg; //per instanciar

    public GameObject monja; //no cal

    void Start()
    {
        edificiPare = GetComponentInParent<EdificiEspecialTrans>();
    }

    public void Morir()
    {
        if (edificiPare != null)
        {
            edificiPare.RegistrarMonjaDerrotada();
        }

        Vector3 pos = monja.transform.position;
        Quaternion rot = monja.transform.rotation;

        Destroy(monja);

        Instantiate(dragg, pos, rot);
    }
}