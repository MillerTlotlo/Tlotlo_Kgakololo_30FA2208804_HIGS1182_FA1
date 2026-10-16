
using UnityEngine;

public class SpaceEnemy : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float stoppingDistance = 15f;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float projectileSpeed = 30f;
    [SerializeField] private float shootingRange = 100f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private float nextFireTime;

    private void Start()
    {
        currentHealth = maxHealth;

        // Find the player automatically.
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning(
                    "SpaceEnemy: No object tagged Player was found."
                );
            }
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Rotate toward the player.
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction.normalized);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Chase the player until stopping distance is reached.
        if (distance > stoppingDistance)
        {
            transform.position += transform.forward
                * moveSpeed * Time.deltaTime;
        }

        // Shoot when the player is within range.
        if (distance <= shootingRange &&
            Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning(
                "SpaceEnemy: Assign Projectile Prefab and Fire Point.",
                this
            );
            return;
        }

        // Create a visible projectile at the firing point.
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(
                (player.position - firePoint.position).normalized
            )
        );

        // Make the projectile travel toward the player.
        EnemyProjectile bullet =
            projectile.GetComponent<EnemyProjectile>();

        if (bullet != null)
        {
            bullet.Launch(
                (player.position - firePoint.position).normalized,
                projectileSpeed
            );
        }
        else
        {
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity =
                    (player.position - firePoint.position).normalized
                    * projectileSpeed;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
