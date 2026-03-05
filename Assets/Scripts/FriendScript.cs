using UnityEngine;
using UnityEngine.AI;

public class FriendScript : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    public float speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 5) {
            agent.speed = 0f;
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            agent.speed = speed;
        }
        
    }
}
