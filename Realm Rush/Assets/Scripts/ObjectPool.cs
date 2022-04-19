using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    // Configuration parameters
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    // State variables
    GameObject[] pool;

    void Awake() {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool() {
        pool = new GameObject[poolSize];

        for (int counter = 0; counter < pool.Length; counter++) {
            pool[counter] = Instantiate(enemyPrefab, transform);
            pool[counter].SetActive(false);
        }
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableObjectInPool() {
        for (int i = 0; i < pool.Length; i++) {
            if (!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return;
            }
        }
    }


}
