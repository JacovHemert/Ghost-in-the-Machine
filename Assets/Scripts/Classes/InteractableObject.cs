using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InteractableObject
{
    public string ObjName;
    public int LucidLevel;
    public Sprite ObjImage;
    public TextAsset inkJSON;

    public InteractableObject()
    {
        ObjName = "";
        LucidLevel = -1;
        ObjImage = null;
        inkJSON = null;
    }

    public InteractableObject(string objName, int lucidLevel, Sprite objImage, TextAsset _inkJSON)
    {
        ObjName = objName;
        LucidLevel = lucidLevel;
        ObjImage = objImage;
        inkJSON = _inkJSON;
    }

}
