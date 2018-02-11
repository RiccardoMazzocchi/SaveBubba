using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    float reloadingTimer;
    float bulletTimer;
    int bullets;
    public GameObject bullet;
    AudioSource audioSource;
    SpriteRenderer mySR;

    GameObject player;

    public Sprite spriteTop, spriteTopRight, spriteTopLeft, spriteLeft, spriteRight, spriteBot, spriteBotLeft, spriteBotRight;

    // Use this for initialization
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        reloadingTimer = 3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        reloadingTimer += Time.deltaTime;
        bulletTimer += Time.deltaTime;
        if (bullets == 0)
        {
            reloadingTimer = 0f;
            bullets = 25;
        }

        LookRotation();
    }


    void LookRotation()
    {
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 22.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 0f 
            || gameObject.transform.GetChild(0).transform.localEulerAngles.z < 360f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 337.5f)
        {
            mySR.sprite = spriteTop;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 67.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 22.5f)
        {
            mySR.sprite = spriteTopLeft;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 112.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 67.5f)
        {
            mySR.sprite = spriteLeft;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 157.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 112.5f)
        {
            mySR.sprite = spriteBotLeft;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 202.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 157.5f)
        {
            mySR.sprite = spriteBot;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 247.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 202.5f)
        {
            mySR.sprite = spriteBotRight;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 292.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 247.5f)
        {
            mySR.sprite = spriteRight;
        }
        if (gameObject.transform.GetChild(0).transform.localEulerAngles.z < 337.5f && gameObject.transform.GetChild(0).transform.localEulerAngles.z > 292.5f)
        {
            mySR.sprite = spriteTopRight;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (this.gameObject.tag == "Infantry")
            {
                if (reloadingTimer > 3f)
                {
                    if (bulletTimer > 0.05f && bullets > 0)
                    {
                        Instantiate(bullet, transform.position, Quaternion.identity);
                        audioSource.PlayOneShot(audioSource.clip);
                        bullets--;
                        bulletTimer = 0f;
                    }
                }
            }
            else if (this.gameObject.tag == "Sniper")
            {
                if (reloadingTimer > 3f)
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    reloadingTimer = 0f;
                    audioSource.Play();
                }
            }
        }
    }
}
