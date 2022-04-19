using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    // Configuration parameters
    [SerializeField] int cost = 75;
    [SerializeField] [Range(0f, 2f)] float sequentialBuildTime = 1f;

    void Start() {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position) {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) { 
            return false;
        }

        if (bank.CurrentBalance >= cost) {
            bank.Withdraw(cost);
            Instantiate(tower.gameObject, position, Quaternion.identity);
            return true;
        }

        return false;
    }

    IEnumerator Build() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);

            foreach (Transform grandchild in child) {
                child.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(sequentialBuildTime);

            foreach (Transform grandchild in child) {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

}
