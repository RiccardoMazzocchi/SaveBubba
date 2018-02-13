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

    Animator myAnim;
    float timer;
    bool exploded;
    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<PlayerController>();
        isHit = false;
        StartCoroutine("ChangeZ");
        //StartCoroutine("Explosion");
        //StartCoroutine("Disappear");
        myAnim = GetComponent<Animator>();
        


    }
	
	// Update is called once per frame
	void Update () {
        bombLifetime += Time.deltaTime;


        timer += Time.deltaTime;
        if (timer > 3.8f)
        {
            Destroy(gameObject);
        }

        if (timer > 3.05f && !exploded)
        {
            GameObject explInst = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 9f), Quaternion.identity);
            explInst.GetComponent<SpriteRenderer>().sortingOrder = 0;
            CameraShake.Shake(0.2f, 0.4f);
            exploded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timer > 3.05f && timer < 3.5f)
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
        if (collision.tag == "Drop Area" || collision.tag == "Sniper" || collision.tag == "Infantry" || collision.tag == "NoArtillery")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeZ()
    {
        yield return new WaitForSeconds(0.25f);
        zPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.position = zPosition;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -999;
    }
}
