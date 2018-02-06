using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public GameObject pausePanel;

    bool isPaused;

    public float gameTimer = 180f;
    public Text timerText;

	// Use this for initialization
	void Start () {
        pausePanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        gameTimer -= Time.deltaTime;

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
	}

    /// <summary> Funzione richiamabile per il tasto Resume del menu di pausa </summary>
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
