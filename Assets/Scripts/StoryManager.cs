using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private static StoryManager instance;

    public bool ContinueIntroStory, ContinueBottlingStory;

    public InteractableObject ghostInfo;
    public string ghostName;

    public GameObject creditsObj;

    [HideInInspector] public int introStoryCounter = 0, bottlingStoryCounter = 0;    

    [Header("CharacterInfo")]
    public InteractableObject RyleInfo;
    public InteractableObject LucyInfo, LucyBottleInfo, EmptyInfo;
    public InteractableObject BertrandInfo, HumeInfo, ImmanuelInfo, LockeInfo, ReneInfo;

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

        //Debug.Log("COUNTER: " + bottlingStoryCounter + " (HC)Rousseau vs (GN)" + ghostName + "? " );
        //Debug.Log(ghostName == "Rousseau");
        //Debug.Log(ghostName.Equals("Rousseau"));

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

        else if (ghostName == "Locke")
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
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
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
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 5)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 6)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));

                bottlingStoryCounter = 0;
                ghostInfo.LucidLevel++;
                //ghostInfo = EmptyInfo;
            }
        }
        else if (ghostName == "Immanuel")
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

                bottlingStoryCounter = 0;
                ghostInfo.LucidLevel++;
                //ghostInfo = EmptyInfo;
            }
        }
        else if (ghostName == "Hume")
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
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
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
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 5)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 6)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 7)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 8)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 9)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));

                bottlingStoryCounter = 0;
                ghostInfo.LucidLevel++;
                //ghostInfo = EmptyInfo;
            }
        }
        else if (ghostName == "Bertrand")
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
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
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
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 5)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 6)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));

                bottlingStoryCounter = 0;
                ghostInfo.LucidLevel++;
                //ghostInfo = EmptyInfo;
            }
        }
        else if (ghostName == "Rousseau")
        {
            Debug.Log("gets to ros");
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
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 3)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
                MusicManager.GetInstance().PlayMusic(1);
            }
            else if (bottlingStoryCounter == 4)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyBottleInfo, JournalManager.GetInstance().GetKeywordEntry(LucyBottleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 5)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 6)
            {
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 7)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }


            //Releasing of other ghosts
            else if (bottlingStoryCounter == 8)
            {
                DialogueManager.GetInstance().EnterDialogueMode(BertrandInfo, JournalManager.GetInstance().GetKeywordEntry(BertrandInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 9)
            {
                DialogueManager.GetInstance().EnterDialogueMode(HumeInfo, JournalManager.GetInstance().GetKeywordEntry(HumeInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 10)
            {
                DialogueManager.GetInstance().EnterDialogueMode(ImmanuelInfo, JournalManager.GetInstance().GetKeywordEntry(ImmanuelInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 11)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LockeInfo, JournalManager.GetInstance().GetKeywordEntry(LockeInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 12)
            {
                DialogueManager.GetInstance().EnterDialogueMode(ReneInfo, JournalManager.GetInstance().GetKeywordEntry(ReneInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }


            // Conversation with Lucy

            else if (bottlingStoryCounter == 13)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 14)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 15)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 16)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 17)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 18)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 19)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 20)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 21)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 22)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 23)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 24)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 25)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 26)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 27)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 28)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 29)
            {
                DialogueManager.GetInstance().EnterDialogueMode(LucyInfo, JournalManager.GetInstance().GetKeywordEntry(LucyInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }

            //END Lucy convo

            else if (bottlingStoryCounter == 30)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 31)
            {
                DialogueManager.GetInstance().EnterDialogueMode(ghostInfo, JournalManager.GetInstance().GetKeywordEntry(ghostInfo, ghostName + "Bottle" + bottlingStoryCounter));
                ContinueBottlingStory = true;
                bottlingStoryCounter++;
            }
            else if (bottlingStoryCounter == 32)
            {
                DialogueManager.GetInstance().EnterDialogueMode(RyleInfo, JournalManager.GetInstance().GetKeywordEntry(RyleInfo, ghostName + "Bottle" + bottlingStoryCounter));

                bottlingStoryCounter = 0;
                //ghostInfo.LucidLevel++;
                //ghostInfo = EmptyInfo;
                creditsObj.SetActive(true);
                creditsObj.GetComponent<Animator>().SetTrigger("Play");
            }

        }
        else
        {
            Debug.Log("Name doesn't match any of the options.");
        }
        
    }

}
