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
        AddedKeyword = 9 //added this for extra column that holds added keywords linked to specific dialogue
    }

    public JournalData(TextAsset csvData)
    {
        LoadJournalData(csvData);
    }

    /// <summary>
    /// Returns the dialogue for a given character about a given keyword topic.
    /// </summary>
    public KeywordEntry AskNPCAbout(InteractableObject npc, string keyword) // changed this to KeywordEntry because we need some of the information later on the line
    {
        KeywordEntry entry = new KeywordEntry();
        
        bool entryExists = keywordMap.TryGetValue((keyword, npc.ObjName), out var journalEntry);

        if (!entryExists) {
            Debug.LogWarning($"Dialogue for {npc.ObjName} with keyword {keyword} is missing.");
            entry.FullDialogue = "[MISSING]";
        }
        else if (journalEntry.RequiredLucidity <= npc.LucidLevel)
        {
            entry = journalEntry;

            // We need to check that the journal entry hasn't been found before so the same keyword can't raise lucidity multiple times
            // but yes, make sure to do it *before* flagging Found as true :P
            if (!journalEntry.Found && journalEntry.AdvanceLucidity) 
            {
                npc.LucidLevel++;
            }

            journalEntry.Found = true;
        }
        else //If the character can't give a coherent response yet, randomly select one of the confused responses.
        {
            if (confusedAnswers.TryGetValue(npc.ObjName, out var answers))
            {
                // Unity's random number generator is upper bound inclusive for some reason so the multiplier has to be slightly less than an integer amount
                int randomValue = (int)(Random.value * (answers.Count - 0.001));
                entry.FullDialogue = answers[randomValue]; 
            }
            else
            {
                Debug.LogWarning($"Fallback dialogue for {npc.ObjName} is missing.");
                entry.FullDialogue = "[MISSING]";
            }
            journalEntry.ConfusedResponseFound = true;
        }

        return entry;
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
            string[] tokens = SplitCSVLine(line);

            string keyword = tokens[(int)Field.Trigger];
            string speaker = tokens[(int)Field.Speaker];
            string fullText = tokens[(int)Field.FullDialogue];
            string addedKeyword = tokens[(int)Field.AddedKeyword];

            string advanceLucidityStr = tokens[(int)Field.AdvLucidity];

            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(speaker))
            {
                Debug.LogWarning("Error parsing csv data: Missing keyword or speaker.");
                continue;
            }

            if (string.IsNullOrEmpty(fullText))
            {
                Debug.LogWarning($"Error parsing csv data: Dialogue for {speaker}:{keyword} is missing.");
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

                if (advanceLucidityStr == "Y")
                    advLucidity = true;
                else
                    advLucidity = false;



                keywordMap.TryAdd(
                    (keyword, speaker),
                    new KeywordEntry
                    {
                        Speaker = speaker,
                        RequiredLucidity = reqLucidity,
                        AdvanceLucidity = advLucidity,
                        FullDialogue = fullText,
                        AddedKeyword = addedKeyword
                    });
            }
        }
    }

    // Splits a line of a CSV file into separate fields
    // The method here is a bit naive but it should be fine for our purposes
    private string[] SplitCSVLine(string line)
    {
        List<string> output = new();
        // Adding a comma to the end of the line simplifies the logic a bit
        string remainder = line + ',';

        while (remainder.Length > 0)
        {
            if (remainder[0] == '"') 
            {
                var tokens = remainder.Split("\",", 2);

                // remove leading double-quote
                output.Add(tokens[0].Substring(1));

                remainder = tokens[1];
            }
            else
            {
                var tokens = remainder.Split(",", 2);

                output.Add(tokens[0]);

                remainder = tokens[1];
            }
        }

        return output.ToArray();
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
