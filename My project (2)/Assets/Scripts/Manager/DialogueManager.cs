using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    PlayerInput input;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    public bool dialoguePlaying {get; private set; }
    private static DialogueManager instance;

    PlayerController Pc;
    Animator animator1;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There are many DialogueManager");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
        input = GameObject.Find("Player").GetComponent<PlayerInput>();

        try 
        {
            animator1 = GameObject.Find("NPC1").GetComponent<Animator>();
        }
        catch(Exception e)
        {
            return;
        }
    }

    private void Update()
    {
        if(!dialoguePlaying)
        {
            return;
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);
        

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public void ExitDialogueMode()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        animator1.SetBool("Speak", false);
        dialogueText.text = "";
        
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
