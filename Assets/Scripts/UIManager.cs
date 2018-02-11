using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public GameObject pausePanel;

    bool isPaused;

    public float gameTimer = 180f;
    public Text timerText;

    public Text warningText;
    float warningTimer;


    StaminaSystem staminaScript;
    HealthScript healthScript;

	// Use this for initialization
	void Start () {
        staminaScript = FindObjectOfType<StaminaSystem>();
        healthScript = FindObjectOfType<HealthScript>();

        pausePanel.SetActive(false);
        
        warningText.text = "Find your friend Bubba and the rest of the squad!";
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.timeScale == 0f)
        {
            warningText.enabled = false;
            healthScript.healthBarGO.SetActive(false);
            staminaScript.staminaBarGO.SetActive(false);
        }
        else
        {
            warningText.enabled = true;
            healthScript.healthBarGO.SetActive(true);
            staminaScript.staminaBarGO.SetActive(true);
        }

        gameTimer -= Time.deltaTime;
        warningTimer += Time.deltaTime;

        string minutes = Mathf.Floor(gameTimer / 60).ToString("00");
        string seconds = Mathf.Floor(gameTimer % 60).ToString("00");

        timerText.text = minutes + " : " + seconds;

		if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }


        if (warningTimer >= 2f)
        {
            warningText.GetComponent<Animator>().SetBool("BIG", true);
        }
	}

    /// <summary> Funzione richiamabile per il tasto Resume del menu di pausa </summary>
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
