using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //variables for movement
    [HideInInspector]
    public float moveH, moveV;

    public float maxSpeed;
    public float minSpeed;
    [HideInInspector]
    public float currentSpeed;

    Rigidbody2D myRB;
    [HideInInspector]
    public bool diagonalMove;

    //variables for marines
    List<Transform> dropPositions = new List<Transform>();
    GameObject dropArea;
    [HideInInspector]
    public MarineController[] marineScripts;

    public GameObject marineHolder;

    public int maxHealth;
    public int playerHealth;


    public Sprite bubbaQmark, notBubba, bubbaFound;
    SpriteRenderer bubbaChildRenderer;

    Animator myAnim;
    public AnimatorOverrideController rescueAnim;
    public AnimatorOverrideController normalAnim;
    SpriteRenderer mySpriteR;

    public float maxStamina;
    [HideInInspector]
    public float currentStamina;


    GameObject tempMarine;
    // Use this for initialization
    void Start () {

        marineScripts = marineHolder.transform.GetComponentsInChildren<MarineController>();
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        mySpriteR = GetComponent<SpriteRenderer>();
        diagonalMove = false;
        dropArea = GameObject.FindGameObjectWithTag("Drop Area");

        bubbaChildRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        bubbaChildRenderer.enabled = false;

        playerHealth = maxHealth;
        currentStamina = maxStamina;

        FindDropSpots();


        if (playerHealth != 0 && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    void FindDropSpots()
    {
        foreach (Transform child in dropArea.transform)
        {
            dropPositions.Add(child);
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;

        if (Time.timeScale > 0f)
        {
            Movement();
            AnimationBools();

            marineScripts = marineHolder.transform.GetComponentsInChildren<MarineController>();
        }
        myRB.WakeUp();
        CheckIfBubba();
        
	}

    void Movement()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        myRB.velocity = new Vector2(moveH * currentSpeed, moveV * currentSpeed);
    
        if (Mathf.Abs(moveH) > 0 && Mathf.Abs(moveV) > 0)
        {
            diagonalMove = true;
            myRB.velocity = new Vector2(moveH * currentSpeed * 0.75f, moveV * currentSpeed * 0.75f);
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

    void CheckIfBubba()
        {
            if (gameObject.transform.childCount > 1)
            {
                if (gameObject.transform.GetChild(1).tag == "Bubba")
                {
                    bubbaChildRenderer.enabled = true;
                    bubbaChildRenderer.sprite = bubbaFound;
                }
                else if (gameObject.transform.GetChild(1).tag != "Bubba")
                {
                    bubbaChildRenderer.enabled = true;
                    bubbaChildRenderer.sprite = notBubba;
                }
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
                tempMarine = collision.gameObject;
                tempMarine.GetComponent<SpriteRenderer>().enabled = false;
                collision.transform.position = new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y + 0.2f, transform.position.z + 0.1f);
                myAnim.runtimeAnimatorController = rescueAnim;
                currentSpeed = minSpeed - 1.5f;
            }
        }

        if (collision.gameObject.tag == "Bubba")
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
                tempMarine = collision.gameObject;
                tempMarine.GetComponent<SpriteRenderer>().enabled = false;
                collision.transform.position = new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y + 0.2f, transform.position.z + 0.1f);
                myAnim.runtimeAnimatorController = rescueAnim;
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Drop Area")
        {
            if (transform.childCount > 1 )
            {
                foreach (Transform child in transform)
                {
                    if (child.transform.parent != transform)
                    {
                        return;
                    }
                    else if (child.transform.tag == "Marine" || child.transform.tag == "Bubba")
                    {
                        
                        tempMarine.GetComponent<SpriteRenderer>().enabled = true;
                        int p = Random.Range(0, dropPositions.Count - 1);
                        child.transform.parent = dropPositions[p].transform;
                        dropPositions.Remove(dropPositions[p]);
                        myAnim.runtimeAnimatorController = normalAnim;
                        playerHealth += 25;
                        bubbaChildRenderer.enabled = false;
                        currentSpeed = minSpeed;
                        tempMarine = null;
                        if (playerHealth > maxHealth)
                        {
                            playerHealth = maxHealth;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Marine" || collision.tag == "Bubba")
        {
            bubbaChildRenderer.enabled = true;
            bubbaChildRenderer.sprite = bubbaQmark;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Marine" || collision.tag == "Bubba")
        {
            bubbaChildRenderer.enabled = false;
        }
    }

    
}
