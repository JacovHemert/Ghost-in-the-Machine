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

    


    private void Awake()
    {
        visualCue.SetActive(false);
    }

    private void Start()
    {
        if (objInformation.LucidLevel == -1)
            objInformation.ObjImage = transform.parent.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void InitiateDialogue()
    {        
        //stores the associated keyword in DialogueManager so it can be used by the ExitDialogueMode method after the dialogue finishes.
        if (associatedKeyword != null)
        {            
            DialogueManager.GetInstance().keywordToAdd = associatedKeyword;
        }

        //DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        interactionEvent.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().FocusTrigger(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().UnfocusTrigger(this);
            visualCue.SetActive(false);
        }
    }
}
