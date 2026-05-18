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

    public GameObject imatge1CopPrefab;
    public GameObject imatge2CopPrefab;
    public GameObject imatge3CopPrefab;
    public GameObject imatgeStarCopPrefab;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hi ha collisio a contador");
        // Comprovar quina jugadora ha colpejat
        if (collision.gameObject.CompareTag("Arma_1"))
        {
            copsJug1++;
            MostrarImatgeCop();

            Debug.Log(gameObject.name + " rebut cop de J1: " + copsJug1 + "/" + copsNecessaris);

            if ( superstarJug1 ||copsJug1 >= copsNecessaris)
            {
                ActivarTransformacio(collision);
                copsJug1 = 0;
                superstarJug1 = false; // Es gasta el superstar
            }
        }

        else if (collision.gameObject.CompareTag("Arma_2"))
        {
            copsJug2++;
            MostrarImatgeCop();

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
        Debug.Log("Entra en activar transfromacio");
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