using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    private bool CanTalk;
    PlayerController player;
    DialogueManager DM;
    public TextAsset inkJSON;
    Animator animator;
    public GameObject exclamationmark;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        DM = GameObject.Find("UI").GetComponent<DialogueManager>();
        animator = GetComponent<Animator>();
        exclamationmark.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CanTalk = true;
            exclamationmark.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CanTalk = false;
            exclamationmark.SetActive(false);
            DM.ExitDialogueMode();
        }

    }

    void Update()
    {
        if(CanTalk && player.Interacted && !DM.dialoguePlaying)
        {
            DM.EnterDialogueMode(inkJSON);
            animator.SetBool("Speak", true);
            player.Interacted = false;
        }

        else if(player.Interacted && CanTalk && DM.dialoguePlaying)
        {
            DM.ContinueStory();
            player.Interacted = false;
        }
    }
}
