using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    public float speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);


        if (distance <= 2f)
        {
            agent.isStopped = true;
            agent.speed = 0f;
            attack();
        }
        else if (distance <= 5f)
        {
            agent.isStopped = false;
            agent.speed = speed;
            attack();
        }
        else if (distance <= 10f)
        {
            agent.isStopped = false;
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
            agent.speed = 0f;
        }

    }

    void attack()
    {

    }
}
