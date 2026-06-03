using UnityEngine;
using System.Collections.Generic;

public class EdificiEspecialDesnon_Tutorial : MonoBehaviour
{
    //aquest script esta al objecte especial del edifici desnonador

    private int copsJug1 = 0;
    private int copsJug2 = 0;

    private const int copsNecessaris = 3;

    public GameObject edificiCapitalistaAssociat; //aquest es la referencia del edifici on esta l'objecte per destruir
    public GameObject edificiBoAssociat; //referencia al prefab per instanciar prefab
    public GameObject cadenesBloqueig; //ref al objecte

    public GameObject imatge1CopPrefab;
    public GameObject imatge2CopPrefab;
    public GameObject imatge3CopPrefab;
    public GameObject imatgeStarCopPrefab;
    
    public GameObject brillPart;

    //public PropietariaEdifici DeQuiEsAquestEdifici; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2
    //AQUI com a cole catolic tampoc cal aquesta referncia perque ho pasa al instanciar el edifici

    public TimeManager_Tutorial TimeManager_Tutorial;

    public int polisDerrotats = 0; //CONTADOR POLIS DERROTATS

    public AudioClip transformClip;
    public AudioClip desnonamentSound;
    public AudioSource desnonamentAudioSource;
    private bool soEntradaReproduit = false;
    public AudioClip cadenesSound; //so trencar cadenes
    public AudioClip aviaTristaSound;
    public AudioClip iaiaContentaSound;
    public AudioClip porcliciaDerrotatSound;

    void Awake()
    {
        GameObject pare = transform.parent.gameObject; //pare hauria de ser el edifici capitaliste
        if (TimeManager_Tutorial == null)
            TimeManager_Tutorial = FindFirstObjectByType<TimeManager_Tutorial>();
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

        GestioSo.instance.PlaySound(porcliciaDerrotatSound, transform, 1f); // SO PORC DERROTAT

        Debug.Log("Polis derrotats: " + polisDerrotats);

        if (polisDerrotats >= 4)
        {
            GestioSo.instance.PlaySound(cadenesSound, transform, 1f);
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

                if (copsJug1 >= copsNecessaris)
                {
                    TimeManager_Tutorial.edificisTransformatJug1+=3;
                    ActivarTransformacio(1, 3);
                    copsJug1 = 0;
                    polisDerrotats = 0;

                }
            }

            else if (propietariaArma == 2)
            {
                copsJug2++;
                MostrarImatgeCop();

                Debug.Log(gameObject.name + " rebut cop de J2: " + copsJug2 + "/" + copsNecessaris);
                
                if (copsJug2 >= copsNecessaris)
                {
                    TimeManager_Tutorial.edificisTransformatJug2+=3;
                    ActivarTransformacio(2, 3);
                    copsJug2 = 0;
                    polisDerrotats = 0;
                }
            }
        }
    }

    void ActivarTransformacio(int propietaria, int punts)
    {
        Debug.Log("Entra en activar transformacio edifici especial");

        GestioSo.instance.PlaySound(iaiaContentaSound, transform, 1f);
        GestioSo.instance.PlaySound(transformClip, transform, 1f);
        
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

        GameObject brilliComu = Instantiate(brillPart, transform.position, Quaternion.identity);
        Destroy(brilliComu, 2f);

        Destroy(edificiCapitalistaAssociat);
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
        if(copsJug1 == 1 || copsJug2 == 1){
            Debug.Log("Mostrar imatge cop 1");

            // Posició una mica per sobre de l'objecte                
            Vector3 posicio = transform.position + new Vector3(0, 3f, 1f);

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