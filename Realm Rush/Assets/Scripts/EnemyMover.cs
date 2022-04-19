using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour {

    // Configuration parameters 
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    // Cached references
    Enemy enemy;
    List<Node> path = new List<Node>();
    GridManager gridManager;
    PathFinder pathFinder;


    void OnEnable() {
        ReturnToStart();
        RecalculatePath(true);

    }

    void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start() {
        
    }

    void RecalculatePath(bool resetPath) {
        Vector2Int coordinates = new Vector2Int();
        
        if (resetPath) {
            coordinates = pathFinder.StartCoordinates;
        }
        else {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();

        path.Clear();

        path = pathFinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());

    }

    void ReturnToStart() {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    IEnumerator FollowPath() {
        for (int i = 1; i < path.Count; i++) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercentage = 0;

            transform.LookAt(endPosition);

            while (travelPercentage < 1f) {
                travelPercentage += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }


        FinishPath();
    }

    void FinishPath() {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

}
