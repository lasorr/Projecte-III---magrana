using UnityEngine;
using System.Collections.Generic;

public class Monjes : MonoBehaviour
{
    //a la monja li es de donar igual l'edifici , mana el valor de derrotades al objecte especial 
    private EdificiEspecialTrans objecteEspecialTrans;
    public GameObject dragg;
    public GameObject edificiCapitalistaAssociat; //referencia al prefab per instanciar prefab

    public AudioClip[] monjaSounds;

    void Start()
    {
        objecteEspecialTrans = GetComponentInParent<EdificiEspecialTrans>();
    }

    public void Morir()
    {
        if (objecteEspecialTrans != null)
        {
            objecteEspecialTrans.RegistrarMonjaDerrotada();
            GestioSo.instance.PlayRandomSound(monjaSounds, transform, 1f); // SO MONJA MORINT
        }

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        GameObject novaDragg = Instantiate(dragg, pos, rot);
        novaDragg.transform.SetParent(edificiCapitalistaAssociat.transform);

        Destroy(gameObject);
    }
}