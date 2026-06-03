using UnityEngine;

public class EdificiEspecialDesnon : MonoBehaviour
{
    //aquest script esta al objecte especial del edifici desnonador

    private int copsJug1 = 0;
    private int copsJug2 = 0;

    private const int copsNecessaris = 3;

    private bool superstarJug1 = false;
    private bool superstarJug2 = false;

    public GameObject edificiCapitalistaAssociat; //aquest es la referencia del edifici on esta l'objecte per destruir
    public GameObject edificiBoAssociat; //referencia al prefab per instanciar prefab
    public GameObject cadenesBloqueig; //ref al objecte

    public GameObject imatge1CopPrefab;
    public GameObject imatge2CopPrefab;
    public GameObject imatge3CopPrefab;
    public GameObject imatgeStarCopPrefab;

    //public PropietariaEdifici DeQuiEsAquestEdifici; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2
    //AQUI com a cole catolic tampoc cal aquesta referncia perque ho pasa al instanciar el edifici

    public TimeManager TimeManager;

    public int polisDerrotats = 0; //CONTADOR POLIS DERROTATS

    public AudioClip transformClip;
    public AudioClip desnonamentSound;
    public AudioSource desnonamentAudioSource;
    private bool soEntradaReproduit = false;
    public AudioClip cadenesSound; //so trencar cadenes
    public AudioClip aviaTristaSound;
    public AudioClip iaiaContentaSound;

    void Awake()
    {
        GameObject pare = transform.parent.gameObject; //pare hauria de ser el edifici capitaliste
        if (TimeManager == null)
            TimeManager = FindFirstObjectByType<TimeManager>();
    }

    // SO DESNONAMENT
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {        
            // ✅ Ambiente en loop mientras esté dentro
            if (desnonamentAudioSource == null && desnonamentSound != null)
            {
                desnonamentAudioSource = GestioSo.instance.PlaySoundPersistent(desnonamentSound, transform, 0.5f, true);
            }
            GestioSo.instance.PlaySound(aviaTristaSound, transform, 1f);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // ✅ Detener ambiente al salir
            if (desnonamentAudioSource != null)
            {
                GestioSo.instance.StopSound(desnonamentAudioSource);
                desnonamentAudioSource = null;
            }
        }
    }

    void Update(){
        if (polisDerrotats >= 4)
        {
            Destroy(cadenesBloqueig);
        }
    }
    public void RegistrarPorcDerrotat()
    {
        polisDerrotats++;

        Debug.Log("Polis derrotats: " + polisDerrotats);

        if (polisDerrotats >= 4)
        {
            Destroy(cadenesBloqueig);
        }
    }

    public void RebreCopEdificiEspecial(int propietariaArma, int punts) //colpejar mana aquesta info todo perfecto
    {
        if (polisDerrotats >= 4)
        {
            if (propietariaArma == 1)
            {
                copsJug1++;
                MostrarImatgeCop();

                Debug.Log(gameObject.name + " rebut cop de J1: " + copsJug1 + "/" + copsNecessaris);

                if ( superstarJug1 ||copsJug1 >= copsNecessaris)
                {
                    TimeManager.edificisTransformatJug1+=3;
                    ActivarTransformacio(1, 3);
                    copsJug1 = 0;
                    polisDerrotats = 0;
                    superstarJug1 = false; // Es gasta el superstar

                    GestioSo.instance.PlaySound(transformClip, transform, 1f);
                    GestioSo.instance.PlaySound(iaiaContentaSound, transform, 1f);
                }
            }

            else if (propietariaArma == 2)
            {
                copsJug2++;
                MostrarImatgeCop();

                Debug.Log(gameObject.name + " rebut cop de J2: " + copsJug2 + "/" + copsNecessaris);
                
                if (superstarJug2 || copsJug2 >= copsNecessaris)
                {
                    TimeManager.edificisTransformatJug2+=3;
                    ActivarTransformacio(2, 3);
                    copsJug2 = 0;
                    polisDerrotats = 0;
                    superstarJug2 = false; // es gasta el superstar
                    
                    GestioSo.instance.PlaySound(transformClip, transform, 1f);
                    GestioSo.instance.PlaySound(iaiaContentaSound, transform, 1f);
                }
            }
        }
    }

    void ActivarTransformacio(int propietaria, int punts)
    {
        Debug.Log("Entra en activar transformacio edifici especial");

        GestioSo.instance.PlaySound(cadenesSound, transform, 1f);
        
        // Aturar so desnonament
        if (desnonamentAudioSource != null)
        {
            GestioSo.instance.StopSound(desnonamentAudioSource);
            desnonamentAudioSource = null;
        }

        Vector3 pos = edificiCapitalistaAssociat.transform.position; //edfici pare o referenciat
        Quaternion rot = edificiCapitalistaAssociat.transform.rotation; 

        GameObject nouEdifici = Instantiate(
            edificiBoAssociat,
            pos,
            rot
        );

        nouEdifici.GetComponent<PropietariaEdifici>().SetPropietari(propietaria, punts);
        //seteja la info todo perfecto 

        Destroy(edificiCapitalistaAssociat);
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