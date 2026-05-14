using UnityEngine;

public class EdificiEspecialTrans : MonoBehaviour
{
    // =========================
    // MONJES DERROTADES
    // =========================

    public static int monjesDerrotades = 0;

    // =========================
    // COPS EDIFICI
    // =========================

    private int copsJug1 = 0;
    private int copsJug2 = 0;

    private const int copsNecessaris = 3;

    // =========================
    // SUPERSTAR
    // =========================

    private bool superstarJug1 = false;
    private bool superstarJug2 = false;

    // =========================
    // REFERÈNCIES
    // =========================

    public GameObject draggAlTransformar;

    public GameObject edificiCapitalistaAssociat;
    public GameObject edificiBoAssociat;

    // IMPORTANT:
    // Marca això a TRUE a les monjes
    // i deixa-ho FALSE a l'edifici especial
    public bool esMonja = false;

    // Per evitar sumar dues vegades
    private bool monjaJaDerrotada = false;

    void OnCollisionEnter(Collision collision)
    {
        // =========================
        // MONJES
        // =========================

        if (esMonja)
        {
            if (collision.gameObject.CompareTag("Arma_1") ||
                collision.gameObject.CompareTag("Arma_2"))
            {
                // Només sumar una vegada
                if (!monjaJaDerrotada)
                {
                    monjaJaDerrotada = true;

                    monjesDerrotades++;

                    Debug.Log("Monja derrotada! Total: " + monjesDerrotades + "/4");

                    // Transformar immediatament amb 1 cop
                    ActivarTransformacio(collision);
                }
            }

            return;
        }

        // =========================
        // EDIFICI ESPECIAL
        // =========================

        if (monjesDerrotades < 4)
        {
            Debug.Log("Encara no s'han derrotat les 4 monjes!");
            return;
        }

        // =========================
        // COPS EDIFICI
        // =========================

        if (collision.gameObject.CompareTag("Arma_1"))
        {
            copsJug1++;

            Debug.Log(gameObject.name + " rebut cop de J1: " + copsJug1 + "/" + copsNecessaris);

            if (superstarJug1 || copsJug1 >= copsNecessaris)
            {
                ActivarTransformacio(collision);

                copsJug1 = 0;
                superstarJug1 = false;
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
                superstarJug2 = false;
            }
        }
    }

    void ActivarTransformacio(Collision collision)
    {
        Debug.Log("Entra en activar transformacio");

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

    // =========================
    // SUPERSTAR
    // =========================

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
    }

    void DesactivarSuperstarJug2()
    {
        superstarJug2 = false;
    }
}