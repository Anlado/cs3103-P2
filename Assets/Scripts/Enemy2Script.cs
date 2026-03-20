using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy2Script : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;

    public GameObject[] waypoints;
    int currentWaypoint;
    public float speed;

    bool canAttack;
    public float attackCooldown = 2;

    StatsManager statsManager;

    public float maxHealth = 100;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        statsManager = player.GetComponent<StatsManager>();
        canAttack = true;
        currentWaypoint = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        Vector3 direction = transform.position - player.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, direction.normalized, out hit, direction.magnitude))
        {
            float distance = hit.distance;
            Debug.Log(distance);

            if (distance <= 2f)
            {
                agent.isStopped = true;
                if (canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
            else if (distance <= 10)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(waypoints[currentWaypoint].transform.position);
            }
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SmallShot")
        {
            currentHealth -= 10;
            statsManager.changeTimesHit(1);
        }
        else if (other.gameObject.tag == "MedShot")
        {
            currentHealth -= 20;
            statsManager.changeTimesHit(1);
        }
        else if (other.gameObject.tag == "LargShot")
        {
            currentHealth -= 30;
            statsManager.changeTimesHit(1);
        }
        else if (other.gameObject.tag == "FriendShot")
        {
            currentHealth -= 10;
        }
        else if (other.gameObject.tag == "ShotgunPellet")
        {
            currentHealth -= 5;
            statsManager.changeTimesHit(1);
        }

    }

    IEnumerator Attack()
    {
        canAttack = false;

        statsManager.changeHealth(-10);
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
