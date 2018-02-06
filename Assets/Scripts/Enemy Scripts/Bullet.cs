using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 playerPos;
    GameObject player;
    float bulletLifetime;
    public float speed;
    PlayerController playerController;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        playerController = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        bulletLifetime += Time.deltaTime;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);

        if (bulletLifetime >= 2f)
        {
            Destroy(gameObject);
        }

        if (transform.position == playerPos)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            playerController.playerHealth -= 10;
        }
    }

}
