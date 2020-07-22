using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float damage = 40;

    void Start()
    {
        
    }

    public void AttackHitEvent()
    {
        if (!target) return;
        var playerHealth = target.GetComponent<PlayerHealth>();

        if (!playerHealth) return;
        playerHealth.PlayerTakeDamage(damage);
    }
}
