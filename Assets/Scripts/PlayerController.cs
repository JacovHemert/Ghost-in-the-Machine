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

    public void PrintStuff()
    {
        Debug.Log("PRASDFADS");
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

    public bool FocusTrigger(DialogueTrigger trigger)
    {
        return nearbyTriggers.Add(trigger);
    }

    public bool UnfocusTrigger(DialogueTrigger trigger)
    {
        return nearbyTriggers.Remove(trigger);
    }

    private DialogueTrigger NearestTrigger()
    {
        return nearbyTriggers.OrderBy(t => Distance(t.gameObject)).FirstOrDefault();
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


    private void Update()
    {
        if (nearbyTriggers.Count > 0)
        {
            DialogueTrigger activeTrigger = NearestTrigger();
            foreach(DialogueTrigger trigger in nearbyTriggers)
            {
                trigger.visualCue.SetActive(false);
            }            
            
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
