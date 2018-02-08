using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public AudioClip audioClip;

    float bombLifetime;
    PlayerController playerController;
    bool isHit;

    Vector3 zPosition;

    public GameObject explosion;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 6f);
        playerController = FindObjectOfType<PlayerController>();
        isHit = false;
        StartCoroutine("ChangeZ");
        StartCoroutine("Explosion");
        StartCoroutine("Disappear");
	}
	
	// Update is called once per frame
	void Update () {
        bombLifetime += Time.deltaTime;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bombLifetime >= 3.5f)
        {
            if (collision.tag == "Player" && isHit == false)
            {
                Debug.Log("Player hit by bomb");
                playerController.playerHealth -= 25;
                isHit = true;  
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Drop Area")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeZ()
    {
        yield return new WaitForSeconds(0.25f);
        zPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.position = zPosition;
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3.3f);
        Instantiate(explosion, new Vector3 (transform.position.x, transform.position.y + 0.75f, transform.position.z), Quaternion.identity);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3.75f);
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}
