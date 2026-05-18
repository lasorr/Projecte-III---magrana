using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NeutralEnemy : MonoBehaviour
{
    private NavMeshAgent agent;

    public Colpejar ScriptCop1;
    public Colpejar ScriptCop2;

    private float thinkTimer;
    public float thinkRate = 2f;

    private Transform targetBuilding;

    public ContadorCops ScriptContadorCops;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        thinkTimer += Time.deltaTime;

        if (thinkTimer >= thinkRate)
        {
            thinkTimer = 0f;
            EscullTarget();
        }
    }
    
    void EscullTarget()
    {

    }
}
