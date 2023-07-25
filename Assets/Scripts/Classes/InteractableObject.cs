using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InteractableObject
{
    public string ObjName;
    public string JobTitle;
    public int LucidLevel;
    public Sprite ObjImage;
    public List<AudioClip> VoiceNormal = new List<AudioClip>();
    public TextAsset inkJSON;

    public InteractableObject()
    {
        ObjName = null;
        JobTitle = null;
        LucidLevel = -1;
        ObjImage = null;
        VoiceNormal = new List<AudioClip>();
        inkJSON = null;
    }

    public InteractableObject(string objName, string jobTitle, int lucidLevel, Sprite objImage, List<AudioClip> voiceNormal, TextAsset _inkJSON)
    {
        ObjName = objName;
        JobTitle = jobTitle;
        LucidLevel = lucidLevel;
        ObjImage = objImage;
        VoiceNormal = voiceNormal;
        inkJSON = _inkJSON;
    }

}
