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

    MarineController[] mc;

	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
        playerController = FindObjectOfType<PlayerController>();

        FindMarines();
        panel.SetActive(false);

        mc = FindObjectsOfType<MarineController>();
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

        if (totalMarines <= 7)
        {
            mc = FindObjectsOfType<MarineController>();
            for (int i = 0; i < totalMarines; i++)
            {
                mc[i].MarinesToBubba();
            }
        }

        if (GameObject.FindGameObjectWithTag("Bubba").GetComponent<MarineController>() == null)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            endText.text = "You saved Bubba! Hurray!";
        }
	}

    void FindMarines ()
    {
        totalMarines = playerController.marineScripts.Length;
    }


}
