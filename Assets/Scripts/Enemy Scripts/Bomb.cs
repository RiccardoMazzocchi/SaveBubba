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

    Animation bombAnim;
    float timer;

    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<PlayerController>();
        isHit = false;
        StartCoroutine("ChangeZ");
        //StartCoroutine("Explosion");
        //StartCoroutine("Disappear");
        bombAnim = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        bombLifetime += Time.deltaTime;


        timer += Time.deltaTime;
        if (bombLifetime >= bombAnim.clip.length)
        {
            Destroy(gameObject);
        }

        if (bombLifetime >= bombAnim.clip.length - 0.5f)
        {
            GameObject explInst = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + -1f, transform.position.z - 9f), Quaternion.identity);
            explInst.GetComponent<SpriteRenderer>().sortingOrder = 0;

            CameraShake.Shake(0.15f, 0.3f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bombLifetime >= bombAnim.clip.length -0.5f && bombLifetime <= bombAnim.clip.length)
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

    /*IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3.5f);
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + -2f, transform.position.z - 4f), Quaternion.identity);
        CameraShake.Shake(0.2f, 0.5f);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3.75f);
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }*/
}
