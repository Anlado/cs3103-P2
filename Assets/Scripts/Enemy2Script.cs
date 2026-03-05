using UnityEngine;
using UnityEngine.AI;

public class Enemy2Script : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;

    public GameObject[] waypoints;
    int currentWaypoint;
    public float speed;

    void Start()
    {
        currentWaypoint = 0;
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
        else if (distance <= 10)
        {
            agent.isStopped = false;
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = false;
            agent.speed = speed;
            agent.SetDestination(waypoints[currentWaypoint].transform.position);
        }
    }

    public void attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "waypoint1")
        {
            currentWaypoint = 1;
        }
        else if (collision.gameObject.tag == "waypoint2")
        {
            currentWaypoint = 2;
        }
        else if (collision.gameObject.tag == "waypoint3")
        {
            currentWaypoint = 3;
        }
        else if (collision.gameObject.tag == "waypoint4")
        {
            currentWaypoint = 4;
        }
        else if (collision.gameObject.tag == "waypoint5")
        {
            currentWaypoint = 0;
        }

    }






}
