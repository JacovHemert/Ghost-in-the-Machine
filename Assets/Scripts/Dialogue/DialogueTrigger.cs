using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    //[Header("Visual Cue")]
    [SerializeField] public GameObject visualCue;

    //[Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;

    [SerializeField] private UnityEvent interactionEvent;

    private bool playerInRange;

    [Header("NPC/Object information")]
    public InteractableObject objInformation;
    
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
