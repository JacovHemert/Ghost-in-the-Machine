using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordEntry
{
    public string ID {  get; set; }
    public string Speaker { get; set; }
    public int RequiredLucidity { get; set; }
    public bool AdvanceLucidity { get; set; }
    public string FullDialogue { get; set; }

    public string AddedKeyword { get; set; }

    /// <summary>
    /// Has this dialogue been found and entered into the journal?
    /// </summary>
    public bool Found { get; set; } = false;

    public bool ConfusedResponseFound { get; set; } = false;
}
