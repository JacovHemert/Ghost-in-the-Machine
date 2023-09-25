using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    //PLAYER DATA
    //-----------
    public Vector3 PlayerPos;

    //STORY DATA
    //----------
    public int IntroStoryCounter = 0, BottlingStoryCounter = 0;
    public bool GotPassCode, DoorsOpen;
    public List<TriggerObject> storyTriggers;
    public Dictionary<string, bool> StoryTriggers;


    //JOURNAL DATA
    //------------
    //public JournalData PlayerJournalData;
    public List<string> FoundKeyWords;

    //NPC DATA
    //--------
    public List<InteractableObject> Actors; //when loading, which lucidity level and make people disappear if lucid level is 7 (or maybe 8?)




    public GameData()
    {
        PlayerPos = new Vector3(18.96f, -1.02f);
        IntroStoryCounter = 0;
        BottlingStoryCounter = 0;
        DoorsOpen = false;
        GotPassCode = false;
        //PlayerJournalData = null;
        FoundKeyWords = new List<string>();
        Actors = new List<InteractableObject>();

        storyTriggers = new List<TriggerObject>();
        StoryTriggers = new Dictionary<string, bool>();

        //this.SpareParts = 0;
        //this.PlayerPosition = new Vector3(-6.26f, 27.27f, 0);
        //this.PlayerHP = 12;
        //this.Wrecks = new List<Wreck>();
        //this.Knowledge = new List<Knowledge>();

        //this.MapData = new List<Area>();
        //this.MinimapObjectsData = new List<MinimapObject>();

        //this.InteractableData = new List<Interactable>();

        //this.GustDefeated = false;
        //this.MinibossDefeated = false;
        //this.BossTriggerData = new List<Interactable>();

        //this.BossWrecks = new List<Wreck>();
        //this.BossWreckPositions = new List<Vector2>();
    }
}
