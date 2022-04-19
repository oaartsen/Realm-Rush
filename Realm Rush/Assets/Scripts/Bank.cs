using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour {

    // Configuration parameters
    [SerializeField] int startingBalance = 150;
    [SerializeField] TextMeshProUGUI displayBalance;

    // State variables
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    // Cached Ref

    void Awake() {
        currentBalance = startingBalance;
        UpdateDislay();
    }

    public void Deposit(int amount) {
        currentBalance += Mathf.Abs(amount);
        UpdateDislay();
    }

    public void Withdraw(int amount) {
        currentBalance -= Mathf.Abs(amount);
        UpdateDislay();

        if (currentBalance < 0) {
            // lose the game
            ReloadScene();
        }
    }

    void UpdateDislay() {
        displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }


}
