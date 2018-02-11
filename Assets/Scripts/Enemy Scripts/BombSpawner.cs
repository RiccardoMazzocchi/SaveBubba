using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour {

    public Vector3 spawnValues;

    public GameObject bomb;

    float artilleryTimer;
    float bombTimer;
    public float artilleryStart;
    public int totalBombs;
    int currentBombs;
    public float bombDelay;
    public GameObject artilleryText;

	// Use this for initialization
	void Start () {
        artilleryText.SetActive(false);
        artilleryTimer = 0f;
        currentBombs = totalBombs;
	}
	
	// Update is called once per frame
	void Update () {

        artilleryTimer += Time.deltaTime;
        bombTimer += Time.deltaTime;

        if (artilleryTimer >= artilleryStart - 5f)
        {
            artilleryText.SetActive(true);
        }
		if (artilleryTimer >= artilleryStart)
        {
            BombSpray();
            artilleryText.SetActive(false);
        }
        if (currentBombs <= 0)
        {
            artilleryTimer = 0f;
            currentBombs = totalBombs; 
        }
        
	}


    void BombSpray()
    {
        if (currentBombs > 0)
        {
            if (bombTimer >= Random.Range(bombDelay, bombDelay + 0.15f))
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 10f);
                GameObject instantiation = Instantiate(bomb, spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
                instantiation.GetComponent<SpriteRenderer>().sortingOrder = -1001;
                currentBombs--;
                bombTimer = 0f;
            }
        }
    }

}
