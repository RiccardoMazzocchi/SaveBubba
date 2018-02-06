using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    GameObject target;

    float reloadingTimer;
    float bulletTimer;
    int bullets = 20;
    public GameObject bullet;
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        reloadingTimer += Time.deltaTime;
        bulletTimer += Time.deltaTime;
        if (bullets == 0)
        {
            reloadingTimer = 0f;
            bullets = 20;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (reloadingTimer > 2f)
            {
                if (bulletTimer > 0.05f && bullets > 0)
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    bullets--;
                    bulletTimer = 0f;
                }
            }
        }
    }


}
