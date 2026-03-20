/*using UnityEngine;
using UnityEngine.AI;

public class FriendScript : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    public float speed;

    public GameObject EnemyOne;
    public GameObject EnemyTwo;

    public float attackCooldown = 1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FollowPlayer();
        CheckAndAttackEnemies();
    }

    void FollowPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= 5f)
        {
            agent.isStopped = true;
            agent.speed = 0f;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            agent.speed = speed;
        }
    }

    void CheckAndAttackEnemies()
    {
        GameObject[] enemies = { EnemyOne, EnemyTwo };

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= attackRange && canAttack)
            {
                StartCoroutine(Attack(enemy));
            }
        }
    }

    IEnumerator Attack(GameObject enemy)
    {
        canAttack = false;

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }


        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
}
*/

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FriendScript : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    public float speed;

    StatsManager statsManager;

    public GameObject EnemyOne;
    public GameObject EnemyTwo;

    public float attackRange = 10f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private bool canAttack = true;

    void Start()
    {
        statsManager = player.GetComponent<StatsManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FollowPlayer();
        CheckAndAttackEnemies();
    }

    void FollowPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= 5f)
        {
            agent.isStopped = true;
            agent.speed = 0f;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            agent.speed = speed;
        }
    }

    void CheckAndAttackEnemies()
    {
        GameObject[] enemies = { EnemyOne, EnemyTwo };

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= attackRange && canAttack)
            {
                StartCoroutine(Attack(enemy));
            }
        }
    }

    IEnumerator Attack(GameObject enemy)
    {
        canAttack = false;

        ShootAtEnemy(enemy);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void ShootAtEnemy(GameObject enemy)
    {
        if (enemy == null) return;

        GameObject projectilePrefab = Resources.Load<GameObject>("FriendlyShot");
        if (projectilePrefab == null) return;

        Vector3 direction = (enemy.transform.position - transform.position).normalized;

        GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position + direction, Quaternion.LookRotation(direction));

        Rigidbody rb = instantiatedProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; 
            rb.linearVelocity = direction * 10f; 
        }

        Destroy(instantiatedProjectile, 5f);
    }
}