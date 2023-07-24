using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public int currentTrack = -1;

    public static MusicManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Music Manager in the scene");
        }
        instance = this;
    }

    public void PlayMusic(int trackNr)
    {
        if (currentTrack != -1)
        {
            transform.GetChild(currentTrack).GetComponent<AudioSource>().Stop();
        }
        transform.GetChild(trackNr).GetComponent<AudioSource>().Play();
        currentTrack = trackNr;
    }

}
