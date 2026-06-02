using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class IAEnemicPorclicia : MonoBehaviour
{
    //animator.SetBool("Run", true);
    //animator.SetBool("Transformar", true);
    public Animator animator; 
    public NavMeshAgent agent;
    private GameObject edificiObjectiu;

    private float thinkTimer;
    public float thinkRate = 2f;

    [Header("Velocitats")]
    public float velocitatLenta = 2f;
    public float velocitatMitjana = 3.5f;
    public float velocitatRapida = 5f;

    private float tempsSobreEdifici = 0f;
    public float tempsNecessari = 6f;

    public int copsRebuts = 0;

    public int jugadora = 0;

    public AudioClip unTransformSound; //so destransformacions

    void Start()
    {
        agent.speed = velocitatLenta;
        transform.position += new Vector3(0, 0.5f, 0);

        if (agent.isOnNavMesh)
            Debug.Log("ESTÀ al NavMesh");
        else
            Debug.Log("NO està al NavMesh");
    }

    void Update()
    {
        thinkTimer += Time.deltaTime;

        if (thinkTimer >= thinkRate)
        {
            thinkTimer = 0f;
        }
    }

    void FixedUpdate(){
        if (jugadora == 1)
        {
            if (edificiObjectiu == null)
            {
                BuscarEdificiJug1();
            }

            else if (edificiObjectiu != null)
            {
                agent.SetDestination(edificiObjectiu.transform.position);

                float dist = Vector3.Distance(agent.transform.position, edificiObjectiu.transform.position);
                Debug.Log("Distancia a l'edifici objectiu: " + dist);

                if (dist > 7f)
                {
                    copsRebuts = 0;
                }

                else if (dist <= 7f)
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Transformar", true);

                    if (copsRebuts >= 3)
                    {
                        BuscarEdificiJug1();
                        copsRebuts = 0;
                        Debug.Log("Porcilia ha rebut 3 cops, canviant d'objectiu");
                    }

                    if (dist < 6.04f)
                    {
                        tempsSobreEdifici += Time.deltaTime;
                    }
                }

                if (tempsSobreEdifici >= tempsNecessari)
                {
                    ConvertirEdificiACapitalista();

                    tempsSobreEdifici = 0f;

                    edificiObjectiu = null;
                }

                else
                {
                    tempsSobreEdifici = 0f;
                }
            }
        }

        else if (jugadora == 2)
        {
            if (edificiObjectiu == null)
            {
                BuscarEdificiJug2();
            }

            else if (edificiObjectiu != null)
            {
                agent.SetDestination(edificiObjectiu.transform.position);

                float dist = Vector3.Distance(agent.transform.position, edificiObjectiu.transform.position);

                if (dist > 7f)
                {
                    copsRebuts = 0;
                }

                else if (dist <= 7f)
                {        
                    animator.SetBool("Run", false);
                    animator.SetBool("Transformar", true);
                    
                    if (copsRebuts >= 3)
                    {
                        BuscarEdificiJug2();
                        copsRebuts = 0;
                        Debug.Log("Porcilia ha rebut 3 cops, canviant d'objectiu");
                    }

                    if (dist < 6.04f)
                    {
                        tempsSobreEdifici += Time.deltaTime;
                    }
                }

                if (tempsSobreEdifici >= tempsNecessari)
                {
                    ConvertirEdificiACapitalista();

                    tempsSobreEdifici = 0f;

                    edificiObjectiu = null;
                }

                else
                {
                    tempsSobreEdifici = 0f;
                }
            }
        }
        
    }

    void BuscarEdificiJug1()
    {
        GameObject[] edificis = GameObject.FindGameObjectsWithTag("EdificiComunista");

        List<GameObject> edificisJug1 = new List<GameObject>();

        foreach (GameObject edifici in edificis)
        {
            PropietariaEdifici prop = edifici.GetComponent<PropietariaEdifici>();

            if (prop != null)
            {
                if (prop.Propietaria == 1)
                {
                    edificisJug1.Add(edifici);
                }
            }
        }

        int quantitatEdificis = TimeManager.Instance.edificisTransformatJug1;

        if (quantitatEdificis <= 4)
        {
            edificiObjectiu = null;

            Vector3 puntAleatori = transform.position + Random.insideUnitSphere * 30f;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(puntAleatori, out hit, 30f, NavMesh.AllAreas))
            {
                agent.speed = velocitatLenta;
                agent.SetDestination(hit.position);
            }

            return;
        }
        
        else if (quantitatEdificis >= 5 && quantitatEdificis <= 8)
        {
            agent.speed = velocitatLenta;
            tempsNecessari = 6f;
        }

        else if (quantitatEdificis >= 9 && quantitatEdificis <= 11)
        {
            agent.speed = velocitatMitjana;
            tempsNecessari = 5f;
        }

        else if (quantitatEdificis >= 12)
        {
            agent.speed = velocitatRapida;
            tempsNecessari = 4f;
        }

        // Escollir edifici random
        if (edificisJug1.Count > 0)
        {
            edificiObjectiu = edificisJug1[Random.Range(0, edificisJug1.Count)];

            Debug.Log("Nou objectiu: " + edificiObjectiu.name);
        }
    }

    void BuscarEdificiJug2()
    {
        GameObject[] edificis = GameObject.FindGameObjectsWithTag("EdificiComunista");

        List<GameObject> edificisJug2 = new List<GameObject>();

        foreach (GameObject edifici in edificis)
        {
            PropietariaEdifici prop = edifici.GetComponent<PropietariaEdifici>();

            if (prop != null)
            {
                if (prop.Propietaria == 2)
                {
                    edificisJug2.Add(edifici);
                }
            }
        }

        int quantitatEdificis = TimeManager.Instance.edificisTransformatJug2;

        if (quantitatEdificis <= 4)
        {
            edificiObjectiu = null;

            Vector3 puntAleatori = transform.position + Random.insideUnitSphere * 30f;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(puntAleatori, out hit, 30f, NavMesh.AllAreas))
            {
                agent.speed = velocitatLenta;
                agent.SetDestination(hit.position);
            }

            return;
        }
        
        else if (quantitatEdificis >= 5 && quantitatEdificis <= 8)
        {
            agent.speed = velocitatLenta;
            tempsNecessari = 6f;
        }

        else if (quantitatEdificis >= 9 && quantitatEdificis <= 11)
        {
            agent.speed = velocitatMitjana;
            tempsNecessari = 5f;
        }

        else if (quantitatEdificis >= 12)
        {
            agent.speed = velocitatRapida;
            tempsNecessari = 4f;
        }

        // Escollir edifici random
        if (edificisJug2.Count > 0)
        {
            edificiObjectiu = edificisJug2[Random.Range(0, edificisJug2.Count)];

            Debug.Log("Nou objectiu: " + edificiObjectiu.name);
        }
    }

    void ConvertirEdificiACapitalista()
    {
        PropietariaEdifici prop = edificiObjectiu.GetComponent<PropietariaEdifici>();

        prop.edificiTransformat = true;

        GestioSo.instance.PlaySound(unTransformSound, transform, 1f); // SO DESTRANSFORMACIO
        animator.SetBool("Transformar", false);
    }
}