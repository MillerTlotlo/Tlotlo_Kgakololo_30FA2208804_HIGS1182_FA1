
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField]
    [Range(5000f, 25000f)]
    private float _launchForce = 10000f;

    [SerializeField]
    [Range(10f, 1000f)]
    private int _damage = 100;

    [SerializeField]
    [Range(2f, 10f)]
    private float _range = 5f;

    private Rigidbody _rigidbody;
    private float _duration;
    private bool _hasHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _duration = _range;
        _hasHit = false;

        if (_rigidbody != null)
        {
            _rigidbody.useGravity = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.AddForce(
                transform.forward * _launchForce,
                ForceMode.Force
            );
        }
        else
        {
            Debug.LogError(
                "Projectile needs a Rigidbody component!",
                this
            );
        }
    }

    private void Update()
    {
        _duration -= Time.deltaTime;

        if (_duration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit)
            return;

        _hasHit = true;

        // Find the enemy, including on a parent object.
        SpaceEnemy enemy =
            collision.collider.GetComponentInParent<SpaceEnemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage);

            Debug.Log(
                "Projectile hit SpaceEnemy! Damage: " + _damage
            );
        }
        else
        {
            // Also support objects using your existing IDamagable interface.
            MonoBehaviour[] components =
                collision.collider.GetComponentsInParent<MonoBehaviour>();

            foreach (MonoBehaviour component in components)
            {
                if (component is IDamagable damageable)
                {
                    Vector3 hitPosition = collision.contactCount > 0
                        ? collision.GetContact(0).point
                        : collision.transform.position;

                    damageable.TakeDamage(_damage, hitPosition);
                    break;
                }
            }
        }

        // Destroy the projectile after a collision.
        Destroy(gameObject);
    }
}
