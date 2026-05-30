using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class IAEnemicPorcliciaJug2 : MonoBehaviour
{
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

    

    void ConvertirEdificiACapitalista()
    {
        PropietariaEdifici prop = edificiObjectiu.GetComponent<PropietariaEdifici>();

        prop.edificiTransformat = true;
    }
}