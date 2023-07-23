using Ink.Runtime;
using System;
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
    [Space(10)]
    [SerializeField] private int textboxCharacterLimit = 360;
    [SerializeField] private InputActionAsset playerControls;

    private InputAction submitAction;
    private Story currentStory;
    private bool submitPressed;

    [NonSerialized] public string keywordToAdd = "";

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

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Starts dialogue when prompting and NPC with a keyword from the journal.
    /// </summary>
    public void EnterDialogueMode(InteractableObject NPC, KeywordEntry entry) // updated second variable to keep the more complete information
    {
        keywordToAdd = entry.AddedKeyword;

        currentStory = CompileDialogue(entry.FullDialogue);

        StartDialogueMode(NPC);
    }

    /// <summary>
    /// Starts dialogue when interacting directly with NPCs. Uses the NPC's lucidity level to display the appropriate dialogue.
    /// </summary>
    public void EnterDialogueMode(GameObject NPCObj)
    {
        InteractableObject NPC = NPCObj.GetComponent<DialogueTrigger>().objInformation;
                
        KeywordEntry entry = JournalManager.GetInstance().journalData.AskNPCAbout(NPC, "Lucid" + NPC.LucidLevel);
        currentStory = CompileDialogue(entry.FullDialogue);

        StartDialogueMode(NPC);
    }

    /// <summary>
    /// Starts dialogue when interacting with items/points of interest in the level
    /// </summary>
    public void EnterInspectionMode(GameObject NPCObj)
    {
        InteractableObject itemActor = NPCObj.GetComponent<DialogueTrigger>().objInformation;
        currentStory = new Story(itemActor.inkJSON.text);

        StartDialogueMode(itemActor);
    }

    private void StartDialogueMode(InteractableObject actor)
    {
        submitAction.Enable();
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        if (actor.ObjName != "")
        {
            namePanel.GetComponentInChildren<TMP_Text>().text = actor.ObjName;
            namePanel.SetActive(true);
        }
        if (actor.ObjImage != null)
        {
            objImage.GetComponent<Image>().sprite = actor.ObjImage;
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
        if (string.IsNullOrEmpty(keywordToAdd))
        {
            bool added = JournalManager.GetInstance().AddKeyword(keywordToAdd);
            if (added)
            {
                foundKeywordPanel.SetActive(true);
                foundKeywordPanel.GetComponentInChildren<TMP_Text>().text = keywordToAdd;
                foundKeywordPanel.GetComponent<Animator>().SetTrigger("Show");
            }
            
            keywordToAdd = string.Empty;
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

    // Converts the string representation of a dialogue into an Ink Story object.
    // Text that is too long to fit into the text box will be split into segments that can be displayed in sequence.
    private Story CompileDialogue(string text)
    {
        int index = textboxCharacterLimit;

        while (text.Length > index)
        {
            int splitPosition = text.LastIndexOf(". ", index, textboxCharacterLimit);

            // I we don't find a period to split on, set the splitPosition to the current index
            // so it cuts the text off at the limit but doesn't overrun the textbox.
            if (splitPosition < 0)
            {
                splitPosition = index;
            }

            // Replace the space after the period with a line break. 
            text = text.Remove(splitPosition + 1, 1);
            text = text.Insert(splitPosition + 1, "\n");

            index = textboxCharacterLimit + splitPosition + 2; // +2 for the period and newline characters
        }

        return new Ink.Compiler(text).Compile();
    }
}
