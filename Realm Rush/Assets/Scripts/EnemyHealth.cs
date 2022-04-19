using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour {

    // Configuration parameters
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Adds amount to maxHitPoints when enemy dies")] [SerializeField] int difficultyRamp = 1;

    // State variables
    int currentHitPoints = 0;

    // Cached references
    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable() {
        currentHitPoints = maxHitPoints;
    }

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    private void ProcessHit() {
        currentHitPoints--;

        if (currentHitPoints <= 0) {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }

}
