using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    UIManager uiManager;
    PlayerController playerController;

    public int totalMarines;

    public Text endText;
    public GameObject panel;

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
        playerController = FindObjectOfType<PlayerController>();
        Invoke("FindMarines", 2f);
        panel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (uiManager.gameTimer <= 0f)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            endText.text = "You couldn't find Bubba before the airstrike :(";
        }

        if (playerController.playerHealth <= 0)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            endText.text = "The charlies bit you!";
        }

        if (totalMarines <= 0 && uiManager.gameTimer <= 150f)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            endText.text = "You saved everyone!";
        }
	}

    void FindMarines ()
    {
        totalMarines = playerController.marineScripts.Length;
    }
}
