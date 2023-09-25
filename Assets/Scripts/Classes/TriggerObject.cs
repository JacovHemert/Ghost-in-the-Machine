using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriggerObject
{

    public string TriggerName;
    public bool TriggerActive;

    public TriggerObject(string triggerName, bool triggerActive)
    {
        TriggerName = triggerName;
        TriggerActive = triggerActive;
    }
}
