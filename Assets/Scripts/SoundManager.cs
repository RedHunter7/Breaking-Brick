using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource BgSound;

    void Awake() 
    {
        if (instance == null) {
             instance = this;
             DontDestroyOnLoad(instance);
             return;
        } else {
             Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip clip)
    {
        BgSound.clip = clip;
        BgSound.Play ();
    }

    public void StopMusic(AudioClip clip)
    {
        BgSound.clip = clip;
        BgSound.Stop ();
    }
}
