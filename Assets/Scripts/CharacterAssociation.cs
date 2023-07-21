using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// This is just used to create an association between a character's button in the journal, and that character's InteractableObject info.
/// There is probably a way better to do this but I can't think of it yet. This should work in the meantime.
/// </summary>
public class CharacterAssociation : MonoBehaviour
{
    public DialogueTrigger associatedNPC;

    public InteractableObject Info { get => associatedNPC != null ? associatedNPC.objInformation : null; }
}
