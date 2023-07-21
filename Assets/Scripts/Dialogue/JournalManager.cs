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

    private JournalData journalData;
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
        RefreshPromptButton();
    }

    private void RefreshPromptButton()
    {
        if (FindAnyObjectByType<PlayerController>().SpeakerClose(out var speaker))
        {
            promptButton.GetComponentInChildren<TMP_Text>().text = 
                $"Prompt {speaker.ObjName} with keyword \"{selectedKeyword}\"";

            promptButton.SetActive(true);
        }
        else
        {
            promptButton.SetActive(false);
        }
    }

    public void AddKeyword(string keyword)
    {        
        foundKeywords.Add(keyword);
        AddKeywordButton(keyword);
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
            string text = journalData.AskNPCAbout(speaker, selectedKeyword);
            DialogueManager.GetInstance().EnterDialogueMode(speaker, text);
        }
    }

    private void SetJournalText()
    {
        textPanel.text = journalData.GetDialogueForJournal(selectedNPC, selectedKeyword);
    }

}

