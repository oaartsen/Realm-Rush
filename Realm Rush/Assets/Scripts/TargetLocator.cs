using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour {

    // Configuration parameters
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float towerRange = 15f;

    // Cached references
    Transform target;

    // Update is called once per frame
    void Update() {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget() {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance) {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon() {
        if (target == null) { return; }

        float targetDistance = Vector3.Distance(transform.position, target.transform.position);

        if (targetDistance < towerRange) {
            Attack(true);
        }
        else {
            Attack(false);
        }

        weapon.LookAt(target);
    }

    void Attack(bool isActive) {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

}
