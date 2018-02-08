using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour {

    public Slider staminaBar;
    PlayerController playerController;

    public float reduceStamina;
    public float recoverStamina;

    float staminaTimer;
    bool depletedStamina;
	// Use this for initialization
	void Start () {
        playerController = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        staminaBar.value = playerController.currentStamina;

        if (Input.GetKey(KeyCode.LeftShift) && playerController.currentStamina >= 0f && depletedStamina == false)
        {
            playerController.currentStamina -= reduceStamina * Time.deltaTime;
            playerController.currentSpeed = playerController.maxSpeed;
            if (playerController.gameObject.transform.childCount > 1)
            {
                playerController.currentSpeed = playerController.maxSpeed * 0.75f;
            }
            if (playerController.currentStamina <= 1f)
            {
                depletedStamina = true;
            }
        }
        else
        {
            playerController.currentStamina += recoverStamina * Time.deltaTime;
            playerController.currentSpeed = playerController.minSpeed;
            if (playerController.gameObject.transform.childCount > 1)
            {
                playerController.currentSpeed = playerController.minSpeed * 0.75f;
            }
        }

        if (depletedStamina == true)
        {
            staminaTimer += Time.deltaTime;

            if (staminaTimer >= 2f)
            {
                depletedStamina = false;
                staminaTimer = 0f;
            }
        }

        if (playerController.currentStamina > playerController.maxStamina)
        {
            playerController.currentStamina = playerController.maxStamina;
        }
	}
}
