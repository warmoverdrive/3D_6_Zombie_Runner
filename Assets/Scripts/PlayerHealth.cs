using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100;

    public void PlayerTakeDamage(float damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
