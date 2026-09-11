using UnityEngine;

public class Projectile : MonoBehaviour
{
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

    private bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _duration = _range;

        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.AddForce(
                transform.forward * _launchForce,
                ForceMode.Force
            );
        }
    }

    private void Update()
    {
        if (OutOfFuel)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damageable =
            collision.collider.GetComponent<IDamagable>();

        if (damageable != null)
        {
            Vector3 hitPosition = collision.GetContact(0).point;

            damageable.TakeDamage(_damage, hitPosition);
        }

        // Destroy the projectile when it hits something
        Destroy(gameObject);
    }
}