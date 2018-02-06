﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //variables for movement
    public float moveH, moveV;
    public float speed;
    Rigidbody2D myRB;
    public bool diagonalMove;

    //variables for marines
    List<Transform> dropPositions = new List<Transform>();
    GameObject dropArea;
    public MarineController[] marineScripts;

    public int playerHealth;


    public Sprite bubbaQmark, notBubba, bubbaFound;
    SpriteRenderer bubbaChildRenderer;

    Animator myAnim;
    SpriteRenderer mySpriteR;

    // Use this for initialization
    void Start () {
        marineScripts = FindObjectsOfType<MarineController>();
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        mySpriteR = GetComponent<SpriteRenderer>();
        diagonalMove = false;
        dropArea = GameObject.Find("Drop Area(Clone)");
        bubbaChildRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();


        bubbaChildRenderer.enabled = false;
        Invoke("FindMarines", 1f);
        
    }


    void FindMarines()
    {
        foreach (Transform child in dropArea.transform)
        {
            dropPositions.Add(child);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        myRB.WakeUp();
        CheckIfBubba();
        AnimationBools();
	}

    void Movement()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");


        myRB.velocity = new Vector2(moveH * speed, moveV * speed);



            
        if (Mathf.Abs(moveH) > 0 && Mathf.Abs(moveV) > 0)
        {
            diagonalMove = true;
            myRB.velocity = new Vector2(moveH * speed * 0.75f, moveV * speed * 0.75f);
        }
        else
        {
            diagonalMove = false;
        }

    }
    
    void AnimationBools()
    {
        if (moveH > 0)
        {
            mySpriteR.flipX = true;
            myAnim.SetBool("Side", true);
            myAnim.SetBool("Top", false);
            myAnim.SetBool("Down", false);
        }
        else if (moveH < 0)
        {
            mySpriteR.flipX = false;
            myAnim.SetBool("Side", true);
            myAnim.SetBool("Top", false);
            myAnim.SetBool("Down", false);
        }
        if (moveV > 0)
        {
            myAnim.SetBool("Side", false);
            myAnim.SetBool("Top", true);
            myAnim.SetBool("Down", false);
        }
        else if (moveV < 0)
        {
            myAnim.SetBool("Side", false);
            myAnim.SetBool("Top", false);
            myAnim.SetBool("Down", true);
        }
        if (moveH > 0 && moveV > 0 || moveH > 0 && moveV < 0 || moveH < 0 && moveV > 0 || moveH < 0 && moveV < 0)
        {
            myAnim.SetBool("Side", true);
            myAnim.SetBool("Top", false);
            myAnim.SetBool("Down", false);
        }
        if (moveH == 0 && moveV == 0)
        {
            myAnim.SetBool("Side", false);
            myAnim.SetBool("Top", false);
            myAnim.SetBool("Down", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Marine")
        {

            if (!collision.transform.IsChildOf(transform))
            {
                if (gameObject.transform.childCount > 1)
                {
                    return;
                }
                collision.transform.parent = gameObject.transform;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                collision.transform.position = new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y + 0.2f, transform.position.z + 0.1f);
                myAnim.SetBool("Rescue", true);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Drop Area")
        {
            if (transform.childCount > 0 )
            {
                foreach (Transform child in transform)
                {
                    if (child.transform.parent != transform)
                    {
                        return;
                    }
                    else if (child.transform.name == "Marine(Clone)")
                    {
                        int p = Random.Range(0, dropPositions.Count - 1);
                        child.transform.parent = dropPositions[p].transform;
                        
                        dropPositions.Remove(dropPositions[p]);
                        myAnim.SetBool("Rescue", false);
                        playerHealth += 25;
                        bubbaChildRenderer.enabled = false;
                        if (playerHealth > 100)
                        {
                            playerHealth = 100;
                        }
                    }
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Marine")
        {
            bubbaChildRenderer.enabled = true;
            bubbaChildRenderer.sprite = bubbaQmark;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Marine")
        {
            bubbaChildRenderer.enabled = false;
        }
    }

    void CheckIfBubba()
    {
        if (gameObject.transform.childCount > 1)
        {
            if (gameObject.transform.GetChild(1).name == "Bubba")
            {
                bubbaChildRenderer.enabled = true;
                bubbaChildRenderer.sprite = bubbaFound;
            }
            else if (gameObject.transform.GetChild(1).name != "Bubba")
            {
                bubbaChildRenderer.enabled = true;
                bubbaChildRenderer.sprite = notBubba;
            }
        }
         
    }
}
