using UnityEngine;

public class EdificiEspecialTrans : MonoBehaviour
{   //aquest script es troba a l'bjecte especial de Edifici cole catolic
    private int copsJug1 = 0;
    private int copsJug2 = 0;

    private const int copsNecessaris = 3;

    private bool superstarJug1 = false;
    private bool superstarJug2 = false;

    public GameObject edificiCapitalistaAssociat; //aixo ha de ser el edifici capitalista on sigui el obj especial
    public GameObject edificiBoAssociat; //prefab edifici bo xd
    public GameObject cadenesBloqueig; //ref 

    public GameObject imatge1CopPrefab;
    public GameObject imatge2CopPrefab;
    public GameObject imatge3CopPrefab;
    public GameObject imatgeStarCopPrefab;

    //public PropietariaEdifici DeQuiEsAquestEdifici; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2 
    //fa refererncia a una varible d'un altre script que es fara servir
    //crec que no fa servir en cap moment la varible perque quan la referencia entra a un script i el valor ve per parametre

    public TimeManager TimeManager;

    public int monjesDerrotades = 0; //CONTADOR MONJES DERROTADES


    void Awake()
    {
        GameObject pare = transform.parent.gameObject; //pare hauria de ser el edifici capitaliste
        if (TimeManager == null)
            TimeManager = FindObjectOfType<TimeManager>();
    }

    public void RegistrarMonjaDerrotada()
    {
        monjesDerrotades++;

        Debug.Log("Monjes derrotades: " + monjesDerrotades);

        if (monjesDerrotades >= 4)
        {
            Destroy(cadenesBloqueig);
        }
    }

    public void RebreCopEdificiEspecial(int propietariaArma, int punts)
    {
        if (propietariaArma == 1)
        {
            copsJug1++;
            MostrarImatgeCop();

            Debug.Log(gameObject.name + " rebut cop de J1: " + copsJug1 + "/" + copsNecessaris);

            if ( superstarJug1 ||copsJug1 >= copsNecessaris)
            {
                ActivarTransformacio(1, punts);
                copsJug1 = 0;
                monjesDerrotades = 0;
                superstarJug1 = false; // Es gasta el superstar

                TimeManager.edificisTransformatJug1+=3;
            }
        }

        else if (propietariaArma == 2)
        {
            copsJug2++;
            MostrarImatgeCop();

            Debug.Log(gameObject.name + " rebut cop de J2: " + copsJug2 + "/" + copsNecessaris);
            
            if (superstarJug2 || copsJug2 >= copsNecessaris)
            {
                ActivarTransformacio(2, 3);
                copsJug2 = 0;
                monjesDerrotades = 0;
                superstarJug2 = false; // es gasta el superstar
                
                TimeManager.edificisTransformatJug2+=3;
            }
        }
    }

    void ActivarTransformacio(int propietaria, int punts)
    {
        Debug.Log("Entra en activar transformacio edifici especial");

        Vector3 pos = edificiCapitalistaAssociat.transform.position; // nom de la referencia 
        Quaternion rot = edificiCapitalistaAssociat.transform.rotation;

        GameObject nouEdifici = Instantiate(
            edificiBoAssociat,
            pos,
            rot
        );
        //aqui entra a al script del edifici bo i seteja propietaria etc
        nouEdifici.GetComponent<PropietariaEdifici>().SetPropietari(propietaria, punts);

        Destroy(edificiCapitalistaAssociat); //aixo hauria de funcionar perf 
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