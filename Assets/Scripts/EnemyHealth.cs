using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage) 
    {
        GetComponent<EnemyAI>().OnDamageTaken();
        hitPoints -= damage; 
        
        if(hitPoints <= 0)
        {
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<CapsuleCollider>());
            GetComponent<Animator>().SetBool("death", true);
        }
    }
}
