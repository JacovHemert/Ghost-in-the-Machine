using Ink;
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
    [SerializeField] private GameObject namePanel, portraitObj, objImage, foundKeywordPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    private bool submitPressed;
    [SerializeField] private InputActionAsset playerControls;
    private InputAction submitAction;

    public bool DialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    public string keywordToAdd = "";

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

    public void EnterDialogueMode(InteractableObject NPC, KeywordEntry entry) // updated second variable to keep the more complete information
    {
        keywordToAdd = entry.AddedKeyword;
        
        submitAction.Enable();

        // Since the dialogue isn't precompiled json data, we have to compile at runtime.
        // We should maybe try to rework everything to not use the INK plugin since we aren't really using any of its features,
        // but this is okay for now.
        //TODO: Add logic to split up large text. I think we just need to insert line breaks to get the Ink compiler to split up the dialogue.
        currentStory = new Ink.Compiler(entry.FullDialogue).Compile(); 

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

    //This now looks for the Lucid0 - Lucid6 entries and displays them rather than the embedded Ink test file.
    public void EnterDialogueMode(GameObject NPCObj)
    {
        InteractableObject NPC = NPCObj.GetComponent<DialogueTrigger>().objInformation;
                
        KeywordEntry entry = JournalManager.GetInstance().journalData.AskNPCAbout(NPC, "Lucid" + NPC.LucidLevel.ToString());

        

        currentStory = new Ink.Compiler(entry.FullDialogue).Compile();

        submitAction.Enable();
        //currentStory = new Story(NPC.inkJSON.text);
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
        
        // If there is one, this displays the found keyword in a popup after the dialogue completes
        if (keywordToAdd != "")
        {
            

            JournalManager.GetInstance().AddKeyword(keywordToAdd, out bool added);
            if (added)
            {
                foundKeywordPanel.SetActive(true);
                foundKeywordPanel.GetComponentInChildren<TMP_Text>().text = keywordToAdd;
                foundKeywordPanel.GetComponent<Animator>().SetTrigger("Show");
            }
            
            keywordToAdd = "";
        }
        
        
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
