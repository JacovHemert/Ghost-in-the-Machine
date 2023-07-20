using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 desiredMove;
    private HashSet<DialogueTrigger> nearbyTriggers = new();

    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        desiredMove = context.ReadValue<Vector2>();
        Debug.Log(desiredMove);
        if (Mathf.Abs(desiredMove.x) > 0.1f && Mathf.Abs(desiredMove.y) >= 0.1f)
        {
            Debug.Log("DIAGONAL");
            //desiredMove = new Vector2(desiredMove.x * 1.7f, desiredMove.y);
            desiredMove = new Vector2(desiredMove.x* 1.4f, desiredMove.y * 0.80f);
        }
            
        desiredMove *= Time.fixedDeltaTime * speed;
        Debug.Log(desiredMove);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Don't allow the player to interact with dialogue triggers when dialogue is already playing
            if (DialogueManager.GetInstance().DialogueIsPlaying)
            {
                return;
            }

            DialogueTrigger activeTrigger = NearestTrigger();
            if (activeTrigger != null)
            {
                activeTrigger.InitiateDialogue();
            }
        }
    }

    // Add a DialogueTrigger to the list of active triggers
    public bool FocusTrigger(DialogueTrigger trigger)
    {
        return nearbyTriggers.Add(trigger);
    }

    // Remove a DialogueTrigger from the list of active triggers
    public bool UnfocusTrigger(DialogueTrigger trigger)
    {
        return nearbyTriggers.Remove(trigger);
    }



    /// <summary>
    /// Returns the nearest DialogueTrigger to the player within interactable range, or null if no triggers are within range.
    /// </summary>
    private DialogueTrigger NearestTrigger()
    {
        return nearbyTriggers.OrderBy(t => Mathf.Abs(Distance(t.gameObject))).LastOrDefault();
    }


    /// <summary>
    /// Calculates the distance from the Player to a given GameObject, based on the distance/overlap between the attached colliders.
    /// </summary>
    private float Distance(GameObject obj)
    {
        float distance = float.PositiveInfinity;
        
        if (obj.TryGetComponent<Collider2D>(out var collider))
        {
           var distanceInfo = rb.Distance(collider);

            if (distanceInfo.isValid)
                distance = distanceInfo.distance;
        }
        
        return distance;
    }


    /// <summary>
    /// Returns true if the player is in range of an NPC. Otherwise returns false.
    /// </summary>
    /// <param name="speaker"></param>
    /// <returns></returns>
    public bool SpeakerClose(out InteractableObject speaker)
    {
        bool speakerClose = false;
        speaker = null;

        if (nearbyTriggers.Count > 0)
        {
            speaker = NearestTrigger().GetComponentInParent<DialogueTrigger>().objInformation;
            if (speaker.LucidLevel > -1)
            {
                speakerClose = true;

            }
        }

        return speakerClose;
    }


    private void Update()
    {
        if (nearbyTriggers.Count > 0)
        {            
            foreach(DialogueTrigger trigger in nearbyTriggers)
            {
                trigger.visualCue.SetActive(false);
            }           

            DialogueTrigger activeTrigger = NearestTrigger();
            //Debug.Log(activeTrigger.transform.parent.name);
            activeTrigger.visualCue.SetActive(true);
        }
        

    }

    private void FixedUpdate()
    {
        // Stop the player's movement during dialogue
        if (DialogueManager.GetInstance().DialogueIsPlaying)
        {
            return;
        }

        rb.MovePosition(new Vector2(transform.position.x + desiredMove.x, transform.position.y + desiredMove.y));
    }
}
