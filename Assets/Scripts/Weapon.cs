using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [Range(1, 100)] [SerializeField] float damage = 25;
    [SerializeField] float range = 100;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFX;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRayCast();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (!target) return;

            target.TakeDamage(damage);
        }
        else return;
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(impact, 0.1f);
    }
}
