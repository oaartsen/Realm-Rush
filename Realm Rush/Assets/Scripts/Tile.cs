using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    // Configuration parameters
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] Tower towerPrefab;

    // State variables
    Vector2Int coordinates = new Vector2Int();

    // Cached references
    GridManager gridManager;
    PathFinder pathFinder;


    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable) {
                gridManager.BlockNode(coordinates);
            }
        }
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start() {
        //if (gridManager != null) {
        //    coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        //
        //    if (!isPlaceable) {
        //        gridManager.BlockNode(coordinates);
        //    }
        //}
    }

    void OnMouseDown() {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates)) {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

            //gridManager.BlockNode(coordinates);
            if (isSuccessful) {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }

}
