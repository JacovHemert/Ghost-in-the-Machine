using Ink.Parsed;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private TextAsset journalData;

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
        foundKeywords.Add("Hume");
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

