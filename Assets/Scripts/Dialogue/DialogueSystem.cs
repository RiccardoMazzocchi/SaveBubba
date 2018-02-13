using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public static DialogueSystem Instance { get; set; }

    [HideInInspector]
    public List<string> dialogueLines = new List<string>();
    [HideInInspector]
    public string npcName;

    public GameObject dialoguePanel;
    Button continueButton;
    Text dialogueText, nameText;
    int dialogueIndex;

    public bool dialogueEnded;

    float timer;
    // Use this for initialization
    void Awake()
    {
        continueButton = dialoguePanel.transform.Find("ContinueButton").GetComponent<Button>();
        dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();

        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialoguePanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        /*if (dialogueEnded == true)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > 0.3f)
            {
                Time.timeScale = 1;
            }
        }

        if (dialogueEnded == false)
        {
            timer = 0f;
        }*/
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.npcName = npcName;

        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueEnded = false;
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueEnded = true;
        Time.timeScale = 1f;
    }

}
