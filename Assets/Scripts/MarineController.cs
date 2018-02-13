using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineController : MonoBehaviour {

    PlayerController playerController;
    GameManager GM;

    MarineController[] mc;
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
                gameObject.GetComponent<Animator>().SetBool("Saved", true);
                transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.parent.position, 1.2f * Time.deltaTime);

            }
            if (transform.position == gameObject.transform.parent.position)
            {
                gameObject.GetComponent<Animator>().SetBool("Saved", false);
                Destroy(this);
            }
        }
        Debug.Log(GM.totalMarines);

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
        if (this.gameObject.tag == "Bubba")
        {
            Time.timeScale = 0f;
            GameManager.instance.panel.SetActive(true);
            GameManager.instance.endText.text = "You saved Booba! Hurray!";
        }
    }


    
}
