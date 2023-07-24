using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private static StoryManager instance;

    public bool ContinueIntroStory, ContinueBottlingStory;

    public InteractableObject ghostInfo;
    public string ghostName;

    [HideInInspector] public int introStoryCounter = 0, bottlingStoryCounter = 0;    

    [Header("CharacterInfo")]
    public InteractableObject RyleInfo;
    public InteractableObject LucyInfo, LucyBottleInfo, EmptyInfo;

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
        ShowIntroStorySegment();

    }

    

    public void ShowIntroStorySegment()
    {
        ContinueIntroStory = false;
        //START INTRO
        if (introStoryCounter == 0)
        {
            MusicManager.GetInstance().PlayMusic(0);
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 1)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 2)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 3)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));            
        }
        // END INTRO

        //First trigger
        else if (introStoryCounter == 4)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 5)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 6)
        {
            DialogueManager.GetInstance().EnterDialogueMode(EmptyInfo, JournalManager.GetInstance().GetKeywordEntry(EmptyInfo, "Story" + introStoryCounter));
        }
        //End first trigger

        //Second trigger
        else if (introStoryCounter == 7)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 8)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 9)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 10)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));
        }
        //End second trigger

        //Third trigger
        else if (introStoryCounter == 11)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 12)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 13)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
        }
        //End third trigger

        //Fourth trigger
        else if (introStoryCounter == 14)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 15)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 16)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 17)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));            
        }
        //End fourth trigger

        //Fifth trigger

        else if (introStoryCounter == 18)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 19)
        {
            DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, "Story" + introStoryCounter));
            ContinueIntroStory = true;
        }
        else if (introStoryCounter == 20)
        {
            DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, "Story" + introStoryCounter));
        }
        //End fifth trigger

        introStoryCounter++;
    }


    public void ShowBottlingStorySegment()
    {
        ContinueBottlingStory = false;

        ghostName = ghostInfo.ObjName;

        Debug.Log("COUNTER: " + bottlingStoryCounter);

        if (ghostName == "Rene")
        {
            if (bottlingStoryCounter == 0)
            {
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 1)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 2)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 3)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 4)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                
                bottlingStoryCounter = 0;
                ghostInfo.LucidLevel++;
                //ghostInfo = EmptyInfo;
            }
        }
        else if (ghostName == "Rousseau")
        {
            
        }
        else if (ghostName == "Locke")
        {
            
        }
        else if (ghostName == "Immanuel")
        {
            
        }
        else if (ghostName == "Hume")
        {
           
        }
        else if (ghostName == "Bertrand")
        {
            
        }
    }

}
