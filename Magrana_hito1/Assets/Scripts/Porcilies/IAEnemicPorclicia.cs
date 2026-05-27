using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NeutralEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject edificiObjectiu;

    private float thinkTimer;
    public float thinkRate = 2f;

    public float velocitat = 3.5f;

    void Start()
    {
        agent.speed = velocitat;

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

            if (edificiObjectiu == null)
            {
                BuscarEdificiJug1();
            }
        }

        if (edificiObjectiu != null)
        {
            agent.SetDestination(edificiObjectiu.transform.position);
        }
    }

    void BuscarEdificiJug1()
    {
        GameObject[] edificis = GameObject.FindGameObjectsWithTag("EdificiComunista");

        Debug.Log("Edificis trobats: " + edificis.Length);

        List<GameObject> edificisJug1 = new List<GameObject>();

        foreach (GameObject edifici in edificis)
        {
            PropietariaEdifici prop = edifici.GetComponent<PropietariaEdifici>();

            if (prop != null)
            {
                Debug.Log("Propietari: " + prop.Propietaria);

                if (prop.Propietaria == 1)
                {
                    edificisJug1.Add(edifici);
                }
            }
        }

        Debug.Log("Edificis jug1: " + edificisJug1.Count);

        if (edificisJug1.Count > 0)
        {
            edificiObjectiu = edificisJug1[Random.Range(0, edificisJug1.Count)];
            Debug.Log("Nou objectiu: " + edificiObjectiu.name);
        }
    }
}