using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem lazors;
    [SerializeField] float range = 50f;

    Transform target;

    void Update()
    {

        PickTarget();
        AimWeapon();
        
    }

    void PickTarget()
    {
        Enemy[] enemiesOnBoard = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        if (enemiesOnBoard.Length > 0)
        {

            foreach (Enemy e in enemiesOnBoard)
            {
                float distance = Vector3.Distance(transform.position, e.transform.position);

                if (distance < maxDistance)
                {
                    closestTarget = e.transform;
                    maxDistance = distance;
                }
            }

            target = closestTarget;

        }
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if(targetDistance < range)
        {
            Attack(true);
        }

        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emission = lazors.emission;
        emission.enabled = isActive;
    }
}
