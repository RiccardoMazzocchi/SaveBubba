using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbaDialogue : MonoBehaviour
{

    public string[] dialogue;
    public string npcName;

    bool bubbaDialogueOne;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bubbaDialogueOne == true)
        {
            if (DialogueSystem.Instance.dialogueEnded == true)
            {
                Debug.Log("destroy me");
                Destroy(this);
            }
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "Bubba")
        {
            if (collision.gameObject.tag == "Player")
            {
                DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
                bubbaDialogueOne = true;
            }
        }
    }
}
