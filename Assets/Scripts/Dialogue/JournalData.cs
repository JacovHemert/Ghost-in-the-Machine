using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalData
{
    private Dictionary<string, KeywordEntry> keywordMap = new();
    private Dictionary<string, List<string>> confusedAnswers = new();

    private string confusedAnswer = "When given a keyword they can't yet give an answer to";
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

    public JournalData(TextAsset csvData)
    {
        LoadJournalData(csvData);
    }

    public Dictionary<string, KeywordEntry> KeywordMap()
    {
        return keywordMap;
    }

    public Dictionary<string, List<string>> ConfusedAnswers()
    {
        return confusedAnswers;
    }

    private void LoadJournalData(TextAsset data)
    {
        string[] lines = data.text.Split('\n');
        foreach (string line in lines)
        {
            string[] tokens = line.Split(',');

            string keyword = tokens[(int)Field.Trigger];
            string speaker = tokens[(int)Field.Speaker];
            string fullText = tokens[(int)Field.FullDialogue];

            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(speaker))
            {
                Debug.LogWarning("Error parsing csv data: Missing keyword or speaker.");
                continue;
            }

            if (string.IsNullOrEmpty(fullText))
            {
                Debug.LogWarning($"Error parsing csv data: Dialogue for {keyword}_{speaker} is missing.");
                continue;
            }

            if (keyword.Equals(confusedAnswer))
            {
                AddConfusedAnswer(speaker, fullText);
            }

            int.TryParse(tokens[4], out int reqLucidity);
            bool.TryParse(tokens[5], out bool advLucidity);

            keywordMap.TryAdd(
                $"{keyword}_{speaker}",
                new KeywordEntry
                {
                    Speaker = speaker,
                    RequiredLucidity = reqLucidity,
                    AdvanceLucidity = advLucidity,
                    FullDialogue = fullText,
                });
        }
    }

    private void AddConfusedAnswer(string speaker, string fullText)
    {
        if (!confusedAnswers.ContainsKey(speaker))
        {
            confusedAnswers[speaker] = new List<string>();
        }

        confusedAnswers[speaker].Add(fullText);
    }
}
