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
    
    // Paràmetres per ESPECIAL
    private GameObject edificiCapitalistaAssociat;
    public GameObject edificiBoAssociat;

    public GameObject imatge1CopPrefab;
    public GameObject imatge2CopPrefab;
    public GameObject imatge3CopPrefab;
    public GameObject imatgeStarCopPrefab;

    public PropietariaEdifici DeQuiEsAquestEdifici; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2

    public TimeManager TimeManager;

    void Awake()
    {
        Transform t = transform;

        while (t.parent != null)
        {
            t = t.parent;
        }

        edificiCapitalistaAssociat = t.gameObject;
    }

    public void RebreCopEdifici(int propietariaArma)
    {
        Debug.Log("L'edifici ha rebut un cop");

        if (propietariaArma == 1)
        {
            copsJug1++;
            MostrarImatgeCop();

            if (superstarJug1 || copsJug1 >= copsNecessaris)
            {
                ActivarTransformacio(propietariaArma);

                copsJug1 = 0;
                superstarJug1 = false;
                
                TimeManager.edificisTransformatJug1++;
            }
        }

        else if (propietariaArma == 2)
        {
            copsJug2++;
            MostrarImatgeCop();

            if (superstarJug2 || copsJug2 >= copsNecessaris)
            {
                ActivarTransformacio(propietariaArma);

                copsJug2 = 0;
                superstarJug2 = false;
                
                TimeManager.edificisTransformatJug2++;
            }
        }
    }
    
    void ActivarTransformacio(int propietaria)
    {
        Debug.Log("Transformant edifici");

        Vector3 pos = edificiCapitalistaAssociat.transform.position;
        Quaternion rot = edificiCapitalistaAssociat.transform.rotation;

        Destroy(edificiCapitalistaAssociat);

        GameObject nouEdifici = Instantiate(
            edificiBoAssociat,
            pos,
            rot
        );

        nouEdifici.GetComponent<PropietariaEdifici>().SetPropietari(propietaria);
    }

    public void ActivarSuperstarJug1()
    {
        superstarJug1 = true;
        Debug.Log("SUPERSTAR activat per J1 a " + gameObject.name);
        Invoke("DesactivarSuperstarJug", 10f);
    }
    
    public void ActivarSuperstarJug2()
    {
        superstarJug2 = true;
        Debug.Log("SUPERSTAR activat per J2 a " + gameObject.name);
        Invoke("DesactivarSuperstarJug", 10f);
    }
    
    void DesactivarSuperstarJug()
    {
        superstarJug1 = false;
        superstarJug2 = false;
        Debug.Log("SUPERSTAR desactivat a " + gameObject.name);
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