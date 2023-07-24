using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPromptPanel, journalPanel;

    private Rigidbody2D rb;
    private Vector2 desiredMove;
    private HashSet<DialogueTrigger> nearbyTriggers = new();

    [SerializeField] private float speed;
    [SerializeField] private List<AudioClip> footsteps = new List<AudioClip>();
    private bool footstepOne = false;
    private float footstepCounter = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        desiredMove = context.ReadValue<Vector2>();
        //Debug.Log(desiredMove);
        if (Mathf.Abs(desiredMove.x) > 0.1f && Mathf.Abs(desiredMove.y) >= 0.1f)
        {
            //Debug.Log("DIAGONAL");
            //desiredMove = new Vector2(desiredMove.x * 1.7f, desiredMove.y);
            desiredMove = new Vector2(desiredMove.x* 1.4f, desiredMove.y * 0.80f);
        }
            
        desiredMove *= Time.fixedDeltaTime * speed;
        //Debug.Log(desiredMove);

        UpdateMovementSprites(desiredMove);
    }

    private void UpdateMovementSprites(Vector2 desiredMove)
    {
        
        Animator anim = GetComponent<Animator>();

        if (Mathf.Approximately(desiredMove.x, 0f))
            anim.SetBool("XZero", true);
        else
            anim.SetBool("XZero", false);

        if (Mathf.Approximately(desiredMove.y, 0f))
            anim.SetBool("YZero", true);
        else
            anim.SetBool("YZero", false);

        anim.SetFloat("X", desiredMove.x);
        anim.SetFloat("Y", desiredMove.y);

        footstepCounter += Time.deltaTime;

        if (footstepCounter >= 0.5f)
        {
            if (footstepOne)
            {
                GetComponent<AudioSource>().clip = footsteps[0];
            }
            else
            {
                GetComponent<AudioSource>().clip = footsteps[1];
            }
            GetComponent<AudioSource>().Play();

            footstepOne = !footstepOne;
            footstepCounter = 0;
        }

        if (Mathf.Approximately(desiredMove.x, 0f) && Mathf.Approximately(desiredMove.y, 0f))
        {
            footstepCounter = 0;
        }
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Don't allow the player to interact with dialogue triggers when dialogue is already playing or when the journal is open
            if (DialogueManager.GetInstance().DialogueIsPlaying || journalPanel.activeSelf)
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
    public bool SpeakerClose(out GameObject speaker) //Changed this to a GameObject out; originally to do something else, but now just in case we need more info than just the ObjInfo.
    {
        bool speakerClose = false;
        speaker = null;

        if (nearbyTriggers.Count > 0)
        {
            speaker = NearestTrigger().gameObject;
            if (speaker.GetComponentInParent<DialogueTrigger>().objInformation.LucidLevel > -1)
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

            if (!DialogueManager.GetInstance().DialogueIsPlaying && !journalPanel.activeSelf)
                ShowButtonPrompt(activeTrigger.objInformation.LucidLevel);
            else HideButtonPrompt();
        }
        else
        {
            HideButtonPrompt();
        }

        UpdateMovementSprites(desiredMove);

    }

    private void ShowButtonPrompt(int lucidLevel)
    {
        HideButtonPrompt();
        buttonPromptPanel.SetActive(true);
        int childNum = 0;
        if (lucidLevel > -1)
            childNum = 1;
        buttonPromptPanel.transform.GetChild(childNum).gameObject.SetActive(true);
    }

    private void HideButtonPrompt()
    {
        buttonPromptPanel.SetActive(false);
        buttonPromptPanel.transform.GetChild(0).gameObject.SetActive(false);
        buttonPromptPanel.transform.GetChild(1).gameObject.SetActive(false);
    }


    private void FixedUpdate()
    {
        // Stop the player's movement during dialogue and while the journal is open
        if (DialogueManager.GetInstance().DialogueIsPlaying || journalPanel.activeSelf)
        {
            return;
        }

        rb.MovePosition(new Vector2(transform.position.x + desiredMove.x, transform.position.y + desiredMove.y));
    }



    public void MatchSpriteMask()
    {
        GetComponentInChildren<SpriteMask>().sprite = GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
