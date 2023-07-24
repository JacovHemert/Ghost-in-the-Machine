using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private static StoryManager instance;

    public bool ContinueStory;

    [HideInInspector] public int storyCounter = 0;    

    [Header("CharacterInfo")]
    public InteractableObject RyleInfo;
    public InteractableObject LucyInfo;

    public static StoryManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Story Manager in the scene");
        }
        instance = this;                
    }

    private void Start()
    {
        ShowStorySegment();

    }

    

    public void ShowStorySegment()
    {
        ContinueStory = false;

        if (storyCounter == 0)
        {
            MusicManager.GetInstance().PlayMusic(0);
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + storyCounter));
            ContinueStory = true;
        }
        else if (storyCounter == 1)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + storyCounter));
        }

        storyCounter++;
    }

}
