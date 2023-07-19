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
    [SerializeField] private Button keywordButton;

    private JournalData journalData;
    private SortedSet<string> foundKeywords = new();
    private static JournalManager instance; 
    
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
        journalPanel.SetActive(false);

        //TODO: this is just for testing, remove later
        AddKeyword("Hume");
        AddKeyword("Bertrand");
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
        button.onClick.AddListener(() => ButtonClicked(keyword));
    }

    public void ButtonClicked(string keyword)
    {
        Debug.Log(keyword);
    }
}

