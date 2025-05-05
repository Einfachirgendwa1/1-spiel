using UnityEngine;

public interface IDamageable {
    public void TakeDamage(RaycastHit hit, float damage);
}