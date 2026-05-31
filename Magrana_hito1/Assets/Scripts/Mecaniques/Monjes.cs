using UnityEngine;

public class Monjes : MonoBehaviour
{
    //a la monja li es de donar igual l'edifici , mana el valor de derrotades al objecte especial 
    private EdificiEspecialTrans objecteEspecialTrans;
    public GameObject dragg;

    void Start()
    {
        objecteEspecialTrans = GetComponentInParent<EdificiEspecialTrans>();
    }

    public void Morir()
    {
        if (objecteEspecialTrans != null)
        {
            objecteEspecialTrans.RegistrarMonjaDerrotada();
        }

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Instantiate(dragg, pos, rot);

        Destroy(gameObject);
    }
}