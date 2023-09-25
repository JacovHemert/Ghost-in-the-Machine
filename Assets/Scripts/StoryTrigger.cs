using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StoryTrigger : MonoBehaviour
{
    public string triggerName;
    public bool active = true;

    private void Awake()
    {
        triggerName = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StoryManager.GetInstance().ShowIntroStorySegment();
            active = false;
            this.gameObject.SetActive(false);
        }
    }

}
