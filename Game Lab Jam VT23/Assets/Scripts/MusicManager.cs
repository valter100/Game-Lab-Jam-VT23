using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        musicClip = GetComponent<AudioSource>().clip;

        GameObject musicManager = GameObject.Find("Music Manager");
        if (musicManager != gameObject)
            Destroy(musicManager);
    }

    public void GetMusic(AudioClip newMusic)
    {
        if (musicClip == newMusic)
            return;

        GetComponent<AudioSource>().clip = newMusic;
        musicClip = newMusic;
        GetComponent<AudioSource>().Play();

    }
}
