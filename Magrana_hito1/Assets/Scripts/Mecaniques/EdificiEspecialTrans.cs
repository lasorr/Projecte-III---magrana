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

    private GameObject edificiCapitalistaAssociat;
    public GameObject edificiBoAssociat;
    public GameObject cadenesBloqueig;

    public GameObject imatge1CopPrefab;
    public GameObject imatge2CopPrefab;
    public GameObject imatge3CopPrefab;
    public GameObject imatgeStarCopPrefab;

    public PropietariaEdifici DeQuiEsAquestEdifici; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2

    public Colpejar ScriptCop1;
    public Colpejar ScriptCop2;

    // IMPORTANT:
    // Marca això a TRUE a les monjes
    // i deixa-ho FALSE a l'edifici especial
    public bool esMonja = false;

    // Per evitar sumar dues vegades
    private bool monjaJaDerrotada = false;

    void Awake()
    {
        Transform t = transform;

        while (t.parent != null)
        {
            t = t.parent;
        }

        edificiCapitalistaAssociat = t.gameObject;
    }

    void Update(){
        if (monjesDerrotades >= 4)
        {
            Destroy(cadenesBloqueig);
        }
    }

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

                    // Transformar immediatament amb 1 cop
                    ActivarTransformacio(collision, 0);
                }
            }

            return;
        }

        // =========================
        // EDIFICI ESPECIAL
        // =========================

        else if (monjesDerrotades >= 4)
        {
            // =========================
            // COPS EDIFICI
            // =========================

            if (collision.gameObject.CompareTag("Arma_1"))
            {
                copsJug1++;
                MostrarImatgeCop();

                Debug.Log(gameObject.name + " rebut cop de J1: " + copsJug1 + "/" + copsNecessaris);

                if ( superstarJug1 ||copsJug1 >= copsNecessaris)
                {
                    ActivarTransformacio(collision, 1);
                    copsJug1 = 0;
                    monjesDerrotades = 0;
                    superstarJug1 = false; // Es gasta el superstar
                    ScriptCop1.edificisTransformatJug1++;
                }
            }

            else if (collision.gameObject.CompareTag("Arma_2"))
            {
                copsJug2++;
                MostrarImatgeCop();

                Debug.Log(gameObject.name + " rebut cop de J2: " + copsJug2 + "/" + copsNecessaris);
                
                if (superstarJug2 || copsJug2 >= copsNecessaris)
                {
                    ActivarTransformacio(collision, 2);
                    copsJug2 = 0;
                    monjesDerrotades = 0;
                    superstarJug2 = false; // es gasta el superstar
                    ScriptCop2.edificisTransformatJug2++;
                }
            }
        }
    }

    void ActivarTransformacio(Collision collision, int propietaria)
    {
        Debug.Log("Entra en activar transformacio");

        Colpejar colpejarScript = collision.gameObject.GetComponentInParent<Colpejar>();

        if (colpejarScript != null)
        {
            colpejarScript.ColpejarObjecte(
                this.gameObject,
                edificiCapitalistaAssociat,
                edificiBoAssociat,
                draggAlTransformar,
                DeQuiEsAquestEdifici.Propietaria = propietaria
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

    void MostrarImatgeStar()
    {
        Debug.Log("Mostrar imatge cop star");

        Vector3 posicio = transform.position + new Vector3(0, 3f, 0);

        // Crear la imatge
        GameObject img = UnityEngine.Object.Instantiate(
            imatgeStarCopPrefab,
            posicio,
            Quaternion.identity
        );

        // Destruir-la després de 1 segon
        Destroy(img, 1f);
    }

    void MostrarImatgeCop()
    {
        if(superstarJug1 || superstarJug2){
            MostrarImatgeStar();
            return;
        }
        else if(copsJug1 == 1 || copsJug2 == 1){
            Debug.Log("Mostrar imatge cop 1");

            // Posició una mica per sobre de l'objecte                
            Vector3 posicio = transform.position + new Vector3(0, 3f, 0);

            // Crear la imatge
            GameObject img = UnityEngine.Object.Instantiate(
                imatge1CopPrefab,
                posicio,
                Quaternion.identity
            );

            // Destruir-la després de 1 segon
            Destroy(img, 1f);

        }

        else if(copsJug1 == 2 || copsJug2 == 2){
            Debug.Log("Mostrar imatge cop 2");

            // Posició una mica per sobre de l'objecte
            Vector3 posicio = transform.position + new Vector3(0, 3f, 0);

            // Crear la imatge
            GameObject img = UnityEngine.Object.Instantiate(
                imatge2CopPrefab,
                posicio,
                Quaternion.identity
            );

            // Destruir-la després de 1 segon
            Destroy(img, 1f);

        }

        else if(copsJug1 == 3 || copsJug2 == 3){
            Debug.Log("Mostrar imatge cop 3");

            // Posició una mica per sobre de l'objecte
            Vector3 posicio = transform.position + new Vector3(0, 3f, 0);

            // Crear la imatge
            GameObject img = UnityEngine.Object.Instantiate(
                imatge3CopPrefab,
                posicio,
                Quaternion.identity
            );

            // Destruir-la després de 1 segon
            Destroy(img, 1f);
        }
    }
}