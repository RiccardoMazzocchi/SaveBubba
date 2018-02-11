using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLayer : MonoBehaviour {

    int randFlip;

	// Use this for initialization
	void Start () {
        randFlip = Random.Range(0, 2);

        if (randFlip == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) *-1;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f );
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 4f);
        }
    }*/
}
