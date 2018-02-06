using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public AudioClip audioClip;

    float bombLifetime;
    public LayerMask playerLayer;
    PlayerController playerController;
    bool isHit;

    Vector3 zPosition;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, audioClip.length - 5.5f);
        playerController = FindObjectOfType<PlayerController>();
        isHit = false;
        StartCoroutine("ChangeZ");
	}
	
	// Update is called once per frame
	void Update () {
        bombLifetime += Time.deltaTime;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bombLifetime >= audioClip.length - 7f)
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
        Debug.Log(collision.tag);
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

}
