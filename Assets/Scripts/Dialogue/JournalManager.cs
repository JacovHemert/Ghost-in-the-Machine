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
    [SerializeField] private TextAsset journalDataFile;
    [SerializeField] private GameObject journalPanel;
    private TextMeshProUGUI textPanel;
    [SerializeField] private GameObject keywordsPage;
    [SerializeField] private GameObject namesPage;
    [SerializeField] private GameObject promptButton;
    [SerializeField] private Button keywordButton;

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
            promptButton.GetComponentInChildren<TMP_Text>().text = "Prompt " + speaker.ObjName
                + " with keyword \"" + selectedKeyword + "\"";
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

    private void SelectButtons()
    {
        RefreshPromptButton();

        for (int i = 0; i < keywordsPage.transform.childCount; i++)
        {
            keywordsPage.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        for (int i = 0; i < namesPage.transform.childCount; i++)
        {
            namesPage.transform.GetChild(i).GetComponent<Image>().color = Color.white;
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
        SetJournalText(selectedKeyword, selectedNPC);
    }

    public void NameButtonClicked(Button button)
    {
        selectedNPCButton = button;
        if (button.TryGetComponent<CharacterAssociation>(out var npc)) { 
            selectedNPC = npc.Info; 
        }

        SelectButtons();
    }

    private void SetJournalText(string keyword, InteractableObject npc)
    {
        
        textPanel.text = journalData.GetDialogueForJournal(npc, keyword);
    }


}

