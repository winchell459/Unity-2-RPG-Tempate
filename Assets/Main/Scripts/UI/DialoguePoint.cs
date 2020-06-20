﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePoint : MonoBehaviour
{
    public string info;
    public TextMeshProUGUI popup;
    public DialogUIManager dialogueMenu;

    private bool inDialogueZone;
    private bool dialoguePlaying;

    //day 1 added ---------------------------------------------------------------------
    public DialogueBranch branch; //root node

    // Start is called before the first frame update
    void Start()
    {
        if (dialogueMenu == null)
        {
            dialogueMenu = GameObject.FindGameObjectWithTag("DIM").GetComponent<DialogUIManager>();
        }
        if (popup == null)
        {
            popup = GameObject.FindGameObjectWithTag("DIM").GetComponentInChildren<TextMeshProUGUI>();
            popup.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePlaying)
            {
                dialogueMenu.NextDialogue();
            }
            else if (inDialogueZone)
            {
                inDialogueZone = false;
                popup.gameObject.SetActive(false);
                dialogueMenu.ActiveDialogue();
                dialogueMenu.NextDialogue();
                dialoguePlaying = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueMenu.RecieveDialogueBranch(branch); // Add start branch here
        popup.gameObject.SetActive(true);
        popup.text = "Press [E] to " + info;
        inDialogueZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inDialogueZone = false;
        popup.gameObject.SetActive(false);
        dialoguePlaying = false;
    }
}
