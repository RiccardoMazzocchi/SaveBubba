using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public static GameManager instance;

    UIManager uiManager;
    PlayerController playerController;

    public int totalMarines;

    public Text endText;
    public GameObject panel;

    MarineController[] mc;

    public bool marinesConverted;
	// Use this for initialization
	void Start () {

        marinesConverted = false;

        if (instance == null)
        {
            instance = this;
        }

        uiManager = FindObjectOfType<UIManager>();
        playerController = FindObjectOfType<PlayerController>();

        panel.SetActive(false);

        mc = FindObjectsOfType<MarineController>();

        Invoke("FindMarines", 1f);
    }
	
	// Update is called once per frame
	void Update () {
		if (uiManager.gameTimer <= 0f)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            endText.text = "You couldn't find Booba before the airstrike :(";
        }

        if (playerController.playerHealth <= 0)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
            endText.text = "The charlies bit you!";
        }

        if (totalMarines <= 6)
        {
            mc = FindObjectsOfType<MarineController>();
            for (int i = 0; i < totalMarines; i++)
            {
                mc[i].MarinesToBubba();
            }
        }
    }

    void FindMarines ()
    {
        totalMarines = playerController.marineScripts.Length;
    }


}
