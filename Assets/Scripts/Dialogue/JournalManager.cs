using Ink.Parsed;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private TextAsset journalDataFile;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject keywordsPage;
    [SerializeField] private GameObject namesPage;
    [SerializeField] private GameObject promptButton;
    [SerializeField] private Button keywordButton;

    private JournalData journalData;
    private SortedSet<string> foundKeywords = new();
    private static JournalManager instance;

    [SerializeField] private PlayerController playerController;

    private int selectedKeyword = 0, selectedNPC = 0;
    
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
        //TODO: this is just for testing, remove later
        AddKeyword("Hume");
        AddKeyword("Bertrand");

        SelectButtons();
        journalPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                + " with keyword \"" + keywordsPage.transform.GetChild(selectedKeyword).name.Substring(9, keywordsPage.transform.GetChild(selectedKeyword).name.Length - 9) + "\"";
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

    public void AddKeywordButton(string keyword)
    {
        var button = Instantiate(keywordButton, keywordsPage.transform);
        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        buttonText.text = keyword;
        button.name = $"Keyword: {keyword}";        
        button.onClick.AddListener(() => KeywordButtonClicked(keyword, button.transform.GetSiblingIndex()));
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


        keywordsPage.transform.GetChild(selectedKeyword).GetComponent<Image>().color = Color.cyan;
        

        namesPage.transform.GetChild(selectedNPC).GetComponent<Image>().color = Color.cyan;
        
        
    }

    public void KeywordButtonClicked(string keyword, int buttonID)
    {
        selectedKeyword = buttonID;
        SelectButtons();
        Debug.Log("keyword: " + keyword + " / " + buttonID);        
    }

    public void NameButtonClicked(int buttonID)
    {
        selectedNPC = buttonID;
        SelectButtons();
    }


}

