using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    public float speed;
    public float shootInterval = 1f;
    public float projectileSpeed = 0;

    Coroutine shootingRoutine;

    public float maxHealth = 100;
    public float currentHealth;

    StatsManager statsmanager;

    void Start()
    {
        statsmanager = player.GetComponent<StatsManager>();
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.linearDamping = 0;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, direction.magnitude))
        {
            float distance = hit.distance;

            if (distance <= 2f)
            {
                agent.isStopped = true;
                if (shootingRoutine == null)
                {
                    shootingRoutine = StartCoroutine(ShootRoutine());
                }
            }
            else if (distance <= 5f)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                if (shootingRoutine == null)
                {
                    shootingRoutine = StartCoroutine(ShootRoutine());
                }
            }
            else if (distance <= 10f)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                if (shootingRoutine != null)
                {
                    StopCoroutine(shootingRoutine);
                    shootingRoutine = null;
                }
            }
            else
            {
                agent.isStopped = true;

                if (shootingRoutine != null)
                {
                    StopCoroutine(shootingRoutine);
                    shootingRoutine = null;
                }
            }
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator ShootRoutine()
    {
        while (true)
        {
            attack();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void attack()
    {
        GameObject projectilePrefab = Resources.Load<GameObject>("EnemyShot");
        GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody rb = instantiatedProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * projectileSpeed;
        }

        Destroy(instantiatedProjectile, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SmallShot")
        {
            currentHealth -= 10;
            statsmanager.changeTimesHit(1);
        }
        else if(other.gameObject.tag == "MedShot")
        {
            currentHealth -= 20;
            statsmanager.changeTimesHit(1);
        }
        else if(other.gameObject.tag == "LargShot")
        {
            currentHealth -= 30;
            statsmanager.changeTimesHit(1);
        }
        else if (other.gameObject.tag == "FriendShot")
        {
            currentHealth -= 10;
        }
        else if (other.gameObject.tag == "ShotgunPellet")
        {
            currentHealth -= 5;
            statsmanager.changeTimesHit(1);
        }

    }


}
