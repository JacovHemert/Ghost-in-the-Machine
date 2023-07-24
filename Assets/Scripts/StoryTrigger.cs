using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StoryManager.GetInstance().ShowIntroStorySegment();
            this.gameObject.SetActive(false);
        }
    }

}
