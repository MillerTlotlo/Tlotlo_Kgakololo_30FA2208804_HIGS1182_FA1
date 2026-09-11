using UnityEngine;

internal interface IDamagable
{
    void TakeDamage(int damage, Vector3 hitPosition);
}