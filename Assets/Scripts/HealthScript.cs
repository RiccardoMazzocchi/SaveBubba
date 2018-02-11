using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public Slider healthBar;
    public GameObject healthBarGO;
    PlayerController playerController;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Slider>();
        healthBarGO = this.gameObject;
        playerController = FindObjectOfType<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
        healthBar.value = playerController.playerHealth;
    }
}
