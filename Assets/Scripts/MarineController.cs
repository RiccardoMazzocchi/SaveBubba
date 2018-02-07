using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineController : MonoBehaviour {

    PlayerController playerController;
    GameManager GM;


    // Use this for initialization
	void Start () {
        playerController = FindObjectOfType<PlayerController>();
        GM = FindObjectOfType<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.parent != null)
        {
            if (this.gameObject.transform.parent.tag == "DropSpot")
            {
                transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.parent.position, 2f * Time.deltaTime);

            }
            if (transform.position == gameObject.transform.parent.position)
            {
                Destroy(this);
            }
        }
    }

    public void MarinesToBubba()
    {
        gameObject.tag = "Bubba";
    }

    private void OnDestroy()
    {
        playerController.marineScripts = FindObjectsOfType<MarineController>();
        GM.totalMarines--;
        Debug.Log(GM.totalMarines);  
    }
}
