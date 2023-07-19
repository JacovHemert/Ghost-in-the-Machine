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
    [SerializeField] private TextAsset journalData;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject keywordsPage;
    [SerializeField] private Button keywordButton;

    private Dictionary<string, KeywordEntry> keywordMap = new();
    private SortedSet<string> foundKeywords = new();
    private static JournalManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Journal Manager in the scene");
        }
        instance = this;

        LoadJournalData();
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
        //TODO: dynamically add buttons to the keywords panel
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

    private void LoadJournalData()
    {
        string[] lines = journalData.text.Split('\n');
        foreach (string line in lines)
        {
            string[] tokens = line.Split(',');

            string keyword = tokens[(int) Field.Trigger];
            string speaker = tokens[(int) Field.Speaker];
            string fullText = tokens[(int) Field.FullDialogue];

            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(speaker))
            {
                Debug.LogWarning("Error parsing csv data: Missing keyword or speaker.");
                continue;
            }

            if (string.IsNullOrEmpty(fullText)){
                Debug.LogWarning($"Error parsing csv data: Dialogue for {keyword}_{speaker} is missing.");
                continue;
            }

            int.TryParse(tokens[4], out int reqLucidity);
            bool.TryParse(tokens[5], out bool advLucidity);

            keywordMap.TryAdd(
                $"{keyword}_{speaker}",
                new KeywordEntry {
                    Speaker = speaker,
                    RequiredLucidity = reqLucidity,
                    AdvanceLucidity = advLucidity,
                    FullDialogue = fullText,
                });
        }
    }

    private enum Field
    {
        ID = 1,
        Speaker = 2,
        Info = 3,
        ReqLucidity = 4,
        AdvLucidity = 5,
        Trigger = 6,
        FullDialogue = 7,
    }
}

