using Ink.Parsed;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private TextAsset journalDataFile; // CSV file containing the dialogue for interactions
    [SerializeField] private GameObject journalPanel;   // The UI panel for the player's journal
    [SerializeField] private GameObject namesPage;      // UI panel in the journal containing NPC name buttons
    [SerializeField] private GameObject keywordsPage;   // UI panel in the journal containing keyword buttons
    [SerializeField] private GameObject promptButton;   // Button for asking NPCs about things
    [SerializeField] private Button keywordButton;      // Prefab for keyword buttons in the journal
    private TextMeshProUGUI textPanel;                  // UI panel in the journal for dispalying dialogue text

    public JournalData journalData; // made public so I can access this for NPC dialogue with E (in DialogueManager) for the Lucid0-6 entries
    private SortedSet<string> foundKeywords = new();
    private static JournalManager instance;

    [SerializeField] private PlayerController playerController;

    private Button selectedKeywordButton;
    private Button selectedNPCButton;

    private string selectedKeyword;
    private InteractableObject selectedNPC;
    
    public static JournalManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Journal Manager in the scene");
        }
        instance = this;

        journalData = new JournalData(journalDataFile);
    }


    // Start is called before the first frame update
    void Start()
    {
        textPanel = journalPanel.transform.Find("Info Panel/Dialogue Text").GetComponent<TextMeshProUGUI>();

        //TODO: this is just for testing, remove later
        AddKeyword("Hume");
        AddKeyword("Bertrand");
        AddKeyword("Immanuel");
        AddKeyword("Rene");

        selectedNPCButton = namesPage.transform.GetChild(0).GetComponent<Button>();
        if (selectedNPCButton.TryGetComponent<CharacterAssociation>(out var npc))
        {
            selectedNPC = npc.Info;
        }

        if (foundKeywords.Count > 0)
        {            
            //selectedKeywordButton = keywordsPage.transform.GetChild(0).GetComponent<Button>();
        }

        
        SelectButtons();
        journalPanel.SetActive(false);
    }

    public void OnActivateJournal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleJournal();
        }
    }

    private void ToggleJournal()
    {
        journalPanel.SetActive(!journalPanel.activeSelf);
        SelectButtons();
        //RefreshPromptButton();
    }

    private void RefreshPromptButton()
    {
        if (FindAnyObjectByType<PlayerController>().SpeakerClose(out var speaker) && selectedKeyword != "")
        {
            promptButton.GetComponentInChildren<TMP_Text>().text = 
                $"Prompt {speaker.GetComponentInParent<DialogueTrigger>().objInformation.ObjName} with keyword \"{selectedKeyword}\"";

            promptButton.SetActive(true);
        }
        else
        {
            promptButton.SetActive(false);
        }
    }

    /// <summary>
    /// Adds a keyword to the notebook/journal. 
    /// </summary>
    /// <returns>True if the keyword is new, false if it is already known.</returns>
    public bool AddKeyword(string keyword) 
    {
        if (!string.IsNullOrEmpty(keyword) && !foundKeywords.Contains(keyword))
        {
            foundKeywords.Add(keyword);
            AddKeywordButton(keyword);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AddKeywordButton(string keyword)
    {
        var button = Instantiate(keywordButton, keywordsPage.transform);
        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        buttonText.text = keyword;
        button.name = $"Keyword: {keyword}";        
        button.onClick.AddListener(() => KeywordButtonClicked(keyword, button));
    }

    /// <summary>
    /// Set highlight colours for selected keyword/NPC buttons
    /// </summary>
    private void SelectButtons()
    {
        RefreshPromptButton();

        foreach (Transform buttonTransform in keywordsPage.transform)
        {
            buttonTransform.GetComponent<Image>().color = Color.white;
        }

        foreach (Transform buttonTransform in namesPage.transform)
        {
            buttonTransform.GetComponent<Image>().color = Color.white;
        }

        if (selectedKeywordButton != null)
        {
            selectedKeywordButton.GetComponent<Image>().color = Color.cyan;
        }

        if (selectedNPCButton != null)
        {
            selectedNPCButton.GetComponent<Image>().color = Color.cyan;
        }
    }

    public void KeywordButtonClicked(string keyword, Button button)
    {
        selectedKeywordButton = button;
        selectedKeyword = keyword;

        SelectButtons();
        SetJournalText();
    }

    public void NameButtonClicked(Button button)
    {
        selectedNPCButton = button;
        if (button.TryGetComponent<CharacterAssociation>(out var npc)) { 
            selectedNPC = npc.Info; 
        }

        SelectButtons();
        SetJournalText();
    }

    public void PromptButtonClicked()
    {
        journalPanel.SetActive(false);


        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().SpeakerClose(out var speaker))
        {
            KeywordEntry entry = journalData.AskNPCAbout(speaker.GetComponentInParent<DialogueTrigger>().objInformation, selectedKeyword);
            DialogueManager.GetInstance().EnterDialogueMode(speaker.GetComponentInParent<DialogueTrigger>().objInformation, entry);
        }
    }

    private void SetJournalText()
    {
        textPanel.text = journalData.GetDialogueForJournal(selectedNPC, selectedKeyword);
    }

}

