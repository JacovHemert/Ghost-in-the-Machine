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
    [SerializeField] private Button keywordButton;

    private JournalData journalData;
    private SortedSet<string> foundKeywords = new();
    private static JournalManager instance;

    private int selectedKeyword, selectedNPC;
    
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            keywordsPage.transform.GetChild(1).GetComponent<Button>().Select();
        }
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
        button.onClick.AddListener(() => KeywordButtonClicked(keyword, button.transform.GetSiblingIndex()));
    }

    private void SelectButtons()
    {
        for (int i = 0; i < keywordsPage.transform.childCount; i++)
        {
            keywordsPage.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }

        for (int i = 0; i < namesPage.transform.childCount; i++)
        {
            namesPage.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }


        //keywordsPage.transform.GetChild(selectedKeyword).GetComponent<Button>().Select();
        Debug.Log("Before: " + keywordsPage.transform.GetChild(selectedKeyword).GetComponent<Image>().color);
        keywordsPage.transform.GetChild(selectedKeyword).GetComponent<Image>().color = Color.cyan;
        Debug.Log("After: " + keywordsPage.transform.GetChild(selectedKeyword).GetComponent<Image>().color);

        //namesPage.transform.GetChild(selectedNPC).GetComponent<Button>().Select();


        namesPage.transform.GetChild(selectedNPC).GetComponent<Image>().color = Color.cyan;
        //Debug.Log(namesPage.transform.GetChild(selectedNPC).name);
        
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

