
using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform _muzzle;
    [SerializeField][Range(0f, 5f)] private float _coolDownTime = 0.25f;

    private float _coolDown;

    private bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

    private void Update()
    {
        // Press and hold SPACE to shoot
        if (Keyboard.current != null &&
            CanFire &&
            Keyboard.current.spaceKey.isPressed)
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        _coolDown = _coolDownTime;

        Instantiate(
            projectilePrefab,
            _muzzle.position,
            _muzzle.rotation
        );
    }
}

