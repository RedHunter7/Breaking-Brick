using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public static bool GameIsPaused = false;
    AudioSource BgSound , audio;
    GameObject panelPause;
    GameObject pauseBtn, resumeBtn;
    public AudioClip openSound , closeSound;
    // Start is called before the first frame update
    void Start()
    {
        panelPause = GameObject.Find("PanelPause");
        panelPause.SetActive(false);

        pauseBtn = GameObject.Find("pause");
        pauseBtn.SetActive(true);

        resumeBtn = GameObject.Find("resume");
        resumeBtn.SetActive(false);

        BgSound = GameObject.Find("Background Sound").GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        } 
    }

    public void Resume() {
        BgSound.UnPause();
        audio.PlayOneShot(closeSound);
        GameIsPaused = false;
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        pauseBtn.SetActive(true);
        resumeBtn.SetActive(false);
    }

    public void Pause() {
        BgSound.Pause();
        audio.PlayOneShot(openSound);
        GameIsPaused = true;
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        pauseBtn.SetActive(false);
        resumeBtn.SetActive(true);
    }

    public void DestroyPausePanel() {
        panelPause.SetActive(true);
        Destroy(panelPause);
    }

    public void RestartGame() {
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
