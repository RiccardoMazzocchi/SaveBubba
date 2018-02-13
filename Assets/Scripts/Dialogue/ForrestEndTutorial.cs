using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForrestEndTutorial : MonoBehaviour {

    public string[] dialogue;
    public string npcName;

    bool tutorialStarted;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

        if (this.gameObject.GetComponent<Tutorial>() == null)
        {
            if (!tutorialStarted)
            {
                tutorialStarted = true;
                DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
            }
        }


        if (DialogueSystem.Instance.dialogueEnded == true)
        {
            Destroy(this);
        }
    }
}
