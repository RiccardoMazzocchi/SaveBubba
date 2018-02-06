using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour {

    public Vector3 spawnValues;

    public GameObject bomb;

    float artilleryTimer;
    float bombTimer;
    public float artilletyStart;
    public int totalBombs;
    int currentBombs;
    public float bombDelay;

	// Use this for initialization
	void Start () {

        artilleryTimer = 0f;
        currentBombs = totalBombs;
	}
	
	// Update is called once per frame
	void Update () {
        artilleryTimer += Time.deltaTime;
        bombTimer += Time.deltaTime;
		if (artilleryTimer >= artilletyStart)
        {
            BombSpray();
            
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
                Instantiate(bomb, spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
                currentBombs--;
                bombTimer = 0f;
            }
        }
    }

}
