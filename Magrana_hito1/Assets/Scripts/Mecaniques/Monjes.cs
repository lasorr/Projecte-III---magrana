using UnityEngine;

public class Monjes : MonoBehaviour
{
    private EdificiEspecialTrans edificiPare;

    public GameObject dragg;

    public GameObject monja;

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