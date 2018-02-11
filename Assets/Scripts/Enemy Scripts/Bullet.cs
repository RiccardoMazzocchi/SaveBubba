using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 playerPos;
    GameObject player;
    float bulletLifetime;
    public float speed;
    PlayerController playerController;

    Vector3 normalizedDirection;
    public int damage;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
        playerController = player.GetComponent<PlayerController>();
        normalizedDirection = (playerPos - transform.position).normalized;

    }
	
	// Update is called once per frame
	void Update () {
        bulletLifetime += Time.deltaTime;

        //loat step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        transform.position += normalizedDirection * speed * Time.deltaTime;

        if (bulletLifetime >= 2f)
        {
            Destroy(gameObject);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            playerController.playerHealth -= damage;
        }
    }

}
