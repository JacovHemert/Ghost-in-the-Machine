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
    public TextAsset inkJSON;

    public InteractableObject()
    {
        ObjName = null;
        JobTitle = null;
        LucidLevel = -1;
        ObjImage = null;
        inkJSON = null;
    }

    public InteractableObject(string objName, string jobTitle, int lucidLevel, Sprite objImage, TextAsset _inkJSON)
    {
        ObjName = objName;
        JobTitle = jobTitle;
        LucidLevel = lucidLevel;
        ObjImage = objImage;
        inkJSON = _inkJSON;
    }

}
