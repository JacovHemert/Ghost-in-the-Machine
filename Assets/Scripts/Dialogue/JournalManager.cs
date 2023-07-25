using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private TextAsset journalDataFile; // CSV file containing the dialogue for interactions
    [SerializeField] private GameObject journalPanel;   // The UI panel for the player's journal
    [SerializeField] private GameObject namesPage;      // UI panel in the journal containing NPC name buttons
    [SerializeField] private GameObject keywordsPage;   // UI panel in the journal containing keyword buttons
    [SerializeField] private GameObject promptButton;   // Button for asking NPCs about things
    [SerializeField] private PanelIcons panelIcons;     // Panel for showing status icons for a given keyword
    [SerializeField] private TextMeshProUGUI textPanel; // UI panel in the journal for dispalying dialogue text
    [SerializeField] private Button keywordButton;      // Prefab for keyword buttons in the journal
    
    [SerializeField] private PlayerController playerController;

    public JournalData journalData; // made public so I can access this for NPC dialogue with E (in DialogueManager) for the Lucid0-6 entries
    private List<string> foundKeywords = new();
    private static JournalManager instance;


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
        //TODO: this is just for testing, remove later
        AddKeyword("Hume");
        AddKeyword("Bertrand");
        AddKeyword("Immanuel");
        AddKeyword("Rene");
        AddKeyword("Rousseau");
        AddKeyword("Locke");

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
        // Prevent opening the journal during dialogue segments
        if (!DialogueManager.GetInstance().DialogueIsPlaying)
        {
            journalPanel.SetActive(!journalPanel.activeSelf);
        }

        if (!journalPanel.activeSelf)
        {
            return;
        }

        if (FindAnyObjectByType<PlayerController>().SpeakerClose(out var speaker))
        {
            for (int i = 0; i < namesPage.transform.childCount; i++)
            {
                if (namesPage.transform.GetChild(i).GetComponent<CharacterAssociation>().associatedNPC.objInformation.ObjName == speaker.GetComponent<DialogueTrigger>().objInformation.ObjName)
                {
                    selectedNPCButton = namesPage.transform.GetChild(i).GetComponent<Button>();
                    selectedNPC = namesPage.transform.GetChild(i).GetComponent<CharacterAssociation>().associatedNPC.objInformation;
                }
            }
            
            
        }


        SelectButtons();
        //RefreshPromptButton();
        panelIcons.RefreshNPCStatusIcons(selectedKeyword);
        panelIcons.RefreshKeywordStatusIcons(selectedNPC.ObjName);

        SetJournalText();
    }

    private void RefreshPromptButton()
    {
        if (FindAnyObjectByType<PlayerController>().SpeakerClose(out var speaker) && selectedKeyword != null)
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

        // Set the sibling index of the buttton so it's in proper sorted order
        foundKeywords.Sort();
        int index = foundKeywords.IndexOf(keyword);
        button.transform.SetSiblingIndex(index);

        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = keyword;
        button.GetComponent<Image>().color = Color.green; //Highlight new keyword buttons, which will last until they are clicked
        button.name = $"Keyword: {keyword}";
        panelIcons.AddNewKeywordIconMap(keyword, button.transform.GetChild(1).GetChild(0).GetComponent<Image>());
        button.onClick.AddListener(() =>
        {
            KeywordButtonClicked(keyword, button);
            panelIcons.RefreshNPCStatusIcons(keyword);
        });
    }

    /// <summary>
    /// Set highlight colours for selected keyword/NPC buttons
    /// </summary>
    private void SelectButtons()
    {
        RefreshPromptButton();

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
        if (selectedKeywordButton != null)
            selectedKeywordButton.GetComponent<Image>().color = Color.white;

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
            string NPCName = speaker.GetComponentInParent<DialogueTrigger>().objInformation.ObjName;

            if (SafeToContinue(speaker.GetComponentInParent<DialogueTrigger>().objInformation, selectedKeyword))
            {
                KeywordEntry entry = journalData.AskNPCAbout(speaker.GetComponentInParent<DialogueTrigger>().objInformation, selectedKeyword);
                DialogueManager.GetInstance().EnterDialogueMode(speaker.GetComponentInParent<DialogueTrigger>().objInformation, entry);
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(StoryManager.GetInstance().EmptyInfo, GetKeywordEntry(StoryManager.GetInstance().EmptyInfo, "Not done"));
            }
        }
    }

    private void SetJournalText()
    {        
        textPanel.text = journalData.GetDialogueForJournal(selectedNPC, selectedKeyword);
    }

    private bool SafeToContinue(InteractableObject npc, string keyword)
    {
        bool safe = false;
        string speaker = npc.ObjName;
        if (speaker == "Rene")
        {
            if (keyword == "Mother" && npc.LucidLevel >= 5)
            {
                if (foundKeywords.Contains("Office") && foundKeywords.Contains("Computer") && foundKeywords.Contains("Hologram"))
                    safe = true;
            }
            else
                safe = true;            
        }
        else if (speaker == "Rousseau")
        {
            if (keyword == "Resignation" && npc.LucidLevel >= 5)
            {
                if (foundKeywords.Contains("GRAPS") && foundKeywords.Contains("Company") && foundKeywords.Contains("Hologram") && foundKeywords.Contains("Bathroom stall"))
                    safe = true;
            }
            else
                safe = true;
                
        }
        else if (speaker == "Locke")
        {
            if (keyword == "Update" && npc.LucidLevel >= 5)
            {
                if (foundKeywords.Contains("Hologram") && foundKeywords.Contains("Office") && foundKeywords.Contains("Bathroom stall") && foundKeywords.Contains("Pantry") && foundKeywords.Contains("Computer"))
                    safe = true;
            }
            else
                safe = true;
            
        }
        else if (speaker == "Immanuel")
        {
            if (keyword == "Birdie" && npc.LucidLevel >= 5)
            {
                if (foundKeywords.Contains("Extinction event") && foundKeywords.Contains("Company") && foundKeywords.Contains("GRAPS"))
                    safe = true;
            }
            else
                safe = true;
            
        }
        else if (speaker == "Hume")
        {
            if (keyword == "Photo" && npc.LucidLevel >= 5)
            {
                if (foundKeywords.Contains("GRAPS") && foundKeywords.Contains("Pantry") && foundKeywords.Contains("Computer") && foundKeywords.Contains("Lunch"))
                    safe = true;
            }
            else
                safe = true;
            
        }
        else if (speaker == "Bertrand")
        {
            if (keyword == "Hologram" && npc.LucidLevel >= 5)
            {
                if (foundKeywords.Contains("Pantry") && foundKeywords.Contains("Lunch"))
                    safe = true;
            }
            else
                safe = true;
            
        }


        return safe;
    }


    public KeywordEntry GetKeywordEntry(InteractableObject actor, string keyword)
    {
        return journalData.AskNPCAbout(actor, keyword);
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        AddKeyword("Resignation");
    //        AddKeyword("GRAPS");
    //        AddKeyword("Company");
    //        AddKeyword("Hologram");
    //        AddKeyword("Bathroom stall");
    //        AddKeyword("Mother");
    //        AddKeyword("Office");
    //        AddKeyword("Computer");

    //    }
    //}
}

