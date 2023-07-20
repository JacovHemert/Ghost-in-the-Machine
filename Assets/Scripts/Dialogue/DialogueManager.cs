using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject namePanel, portraitObj, objImage;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    private bool submitPressed;
    [SerializeField] private InputActionAsset playerControls;
    private InputAction submitAction;

    public bool DialogueIsPlaying { get; private set; }

    private static DialogueManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        submitAction = playerControls.FindActionMap("Player").FindAction("Advance Dialogue");
        submitAction.Disable();
    }

    private void Start()
    {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        namePanel.SetActive(false);
        portraitObj.SetActive(false);
        objImage.SetActive(false);
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!DialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        if (GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }



    public void EnterDialogueMode(GameObject NPCObj)
    {
        InteractableObject NPC = NPCObj.GetComponent<DialogueTrigger>().objInformation;

        submitAction.Enable();
        currentStory = new Story(NPC.inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        if (NPC.ObjName != "")
        {
            namePanel.GetComponentInChildren<TMP_Text>().text = NPC.ObjName;
            namePanel.SetActive(true);
        }
        if (NPC.ObjImage != null)
        {
            portraitObj.GetComponent<Image>().sprite = NPC.ObjImage;
            portraitObj.SetActive(true);
        }

        ContinueStory();
    }

    public void EnterInspectionMode(GameObject NPCObj)
    {
        InteractableObject NPC = NPCObj.GetComponent<DialogueTrigger>().objInformation;

        submitAction.Enable();
        currentStory = new Story(NPC.inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        if (NPC.ObjName != "")
        {
            namePanel.GetComponentInChildren<TMP_Text>().text = NPC.ObjName;
            namePanel.SetActive(true);
        }
        if (NPC.ObjImage != null)
        {
            objImage.GetComponent<Image>().sprite = NPC.ObjImage;
            objImage.SetActive(true);
        }

        ContinueStory();
    }


    private void ExitDialogueMode()
    {
        submitAction.Disable();
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        namePanel.SetActive(false);
        portraitObj.SetActive(false);
        objImage.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        Debug.Log("Continue story");
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public void OnSubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Submit");
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }
}
