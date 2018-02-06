using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    UIManager uiManager;
    PlayerController playerController;

    int totalMarines;

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
        playerController = FindObjectOfType<PlayerController>();
        Invoke("FindMarines", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		if (uiManager.gameTimer <= 0f)
        {
            Time.timeScale = 0f;
            Debug.Log("You lost");
        }

        if (playerController.playerHealth <= 0)
        {
            Time.timeScale = 0f;
            Debug.Log("You lost");
        }

        if (totalMarines <= 0 && uiManager.gameTimer <= 150f)
        {
            Time.timeScale = 0f;
            Debug.Log("You win");
        }
	}

    void FindMarines ()
    {
        totalMarines = playerController.marineScripts.Length;
    }
}
