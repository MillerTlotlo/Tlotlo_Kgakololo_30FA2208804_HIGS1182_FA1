
using System;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamagable
{
    [SerializeField] private FracturedAsteroid fracturedAsteroidPrefab;
    [SerializeField] private Detonator _explosionPrefab;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAsteroid(hitPosition);
    }

    private void FractureAsteroid(Vector3 hitPosition)
    {
        if (fracturedAsteroidPrefab != null)
        {
            Instantiate(
                fracturedAsteroidPrefab,
                _transform.position,
                _transform.rotation
            );

            if (_explosionPrefab != null)
            {
                Instantiate(
                    _explosionPrefab,
                    hitPosition,
                    Quaternion.identity
                );
            }

            Destroy(gameObject);
        }
    }
}

