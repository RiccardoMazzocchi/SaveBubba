using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public string[] dialogue;
    public string name;

    bool tutorialStarted;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (DialogueSystem.Instance.dialogueEnded == true)
        {
            Destroy(this);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!tutorialStarted)
        {
            tutorialStarted = true;
            DialogueSystem.Instance.AddNewDialogue(dialogue, name);
        }

    }
}
