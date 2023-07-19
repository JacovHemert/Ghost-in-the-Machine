using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public GameObject visualCue;
    [SerializeField] public TextAsset inkJSON;

    [SerializeField] public string associatedKeyword;

    [SerializeField] private UnityEvent interactionEvent;

    [Header("NPC/Object information")]
    public InteractableObject objInformation;


    private bool playerInRange;

    //[SerializeField] public string objName;
    //[SerializeField] public int lucidLevel;
    //[SerializeField] public Sprite image;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().DialogueIsPlaying)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    public void InitiateDialogue()
    {
        //DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        interactionEvent.Invoke();

        if (!string.IsNullOrEmpty(associatedKeyword))
        {
            JournalManager.GetInstance().AddKeyword(associatedKeyword);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            collision.gameObject.GetComponent<PlayerController>().FocusTrigger(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            collision.gameObject.GetComponent<PlayerController>().UnfocusTrigger(this);
        }
    }
}
