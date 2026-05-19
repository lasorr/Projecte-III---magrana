using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NeutralEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject edificiObjectiu;

    private float thinkTimer;
    public float thinkRate = 2f;

    private float velocitat = 3.5f; // Afegeix aquesta variable

    public PropietariaEdifici ScriptPropietaria;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Buscar per nom si no tens tags específics
        GameObject jugadora1 = GameObject.Find("Arma_1"); // O el nom exacte del GameObject
        GameObject jugadora2 = GameObject.Find("Arma_2"); // O el nom exacte del GameObject
    }

    void Update()
    {
        thinkTimer += Time.deltaTime;

        if (thinkTimer >= thinkRate)
        {
            thinkTimer = 0f;
        }

        if (edificiObjectiu != null)
        {
            // Moure's cap a l'edifici
            float step = velocitat * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, edificiObjectiu.transform.position, step);
        }
    }

    void DestruirEdificisJug()
    {
        GameObject[] edificis = GameObject.FindGameObjectsWithTag("EdificiComunista");

        if (ScriptPropietaria.Propietaria == 1)
        {
            
        }

        else if (ScriptPropietaria.Propietaria == 2)
        {
            
        }
    }
}
