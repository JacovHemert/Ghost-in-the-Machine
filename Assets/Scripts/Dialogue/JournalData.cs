using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JournalData
{
    private Dictionary<ValueTuple<string, string>, KeywordEntry> keywordMap = new();
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

    /// <summary>
    /// Returns the dialogue for a given character about a given keyword topic.
    /// </summary>
    public string AskNPCAbout(InteractableObject npc, string keyword)
    {
        string text;

        bool entryExists = keywordMap.TryGetValue((keyword, npc.ObjName), out var journalEntry);

        if (!entryExists) {
            Debug.LogWarning($"Dialogue for {npc.ObjName} with keyword {keyword} is missing.");
            text = "[MISSING]";
        }
        else if (journalEntry.RequiredLucidity <= npc.LucidLevel)
        {
            text = journalEntry.FullDialogue;
            journalEntry.Found = true;

            if (!journalEntry.Found && journalEntry.AdvanceLucidity)
            {
                npc.LucidLevel++;
            }
        }
        else //If the character can't give a coherent response yet, randomly select one of the confused responses.
        {
            if (confusedAnswers.TryGetValue(npc.ObjName, out var answers))
            {
                // Unity's random number generator is upper bound inclusive for some reason so the multiplyer has to be slightly less than an integer amount
                int randomValue = (int)(Random.value * (answers.Count - 0.001));
                text = answers[randomValue];
            }
            else
            {
                Debug.LogWarning($"Fallback dialogue for {npc.ObjName} is missing.");
                text = "[MISSING]";
            }
            journalEntry.ConfusedResponseFound = true;
        }

        return text;
    }

    /// <summary>
    /// Returns information about a previous conversation to be shown in the journal.
    /// </summary>
    public string GetDialogueForJournal(InteractableObject npc, string keyword)
    {
        if (npc == null || keyword == null)
        {
            return string.Empty;
        }

        bool entryExists = keywordMap.TryGetValue((keyword, npc.ObjName), out var journalEntry);

        if (entryExists && journalEntry.Found)
        {
            return journalEntry.FullDialogue;
        }
        else if (entryExists && journalEntry.ConfusedResponseFound)
        {
            return "I asked about this, but couldn't get a clear answer. Maybe I should try asking again later.";
        }
        else
        {
            return "";
        }
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

            // handle the fallback "confused" dialogue separately
            if (keyword.Equals(confusedAnswer))
            {
                AddConfusedAnswer(speaker, fullText);
            }
            else
            {
                int.TryParse(tokens[4], out int reqLucidity);
                bool.TryParse(tokens[5], out bool advLucidity);

                keywordMap.TryAdd(
                    (keyword, speaker),
                    new KeywordEntry
                    {
                        Speaker = speaker,
                        RequiredLucidity = reqLucidity,
                        AdvanceLucidity = advLucidity,
                        FullDialogue = fullText,
                    });
            }
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
