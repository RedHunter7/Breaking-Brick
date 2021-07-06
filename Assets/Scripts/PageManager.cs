using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public Animator crossfade, circle;
    public AudioClip audio;
    // Start is called before the first frame update
    void start()
    {

    }

    void awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        StartCoroutine(CircleTransition(2));
        SoundManager.instance.StopMusic(audio);
    }

    public void LoadPrevScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GotoHome() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Homepage");
        SoundManager.instance.PlayMusic(audio);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void RetryGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene() {
        StartCoroutine(CircleTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void SelectLevel() {
        StartCoroutine(CrossfadeTransition(1));
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
        SoundManager.instance.StopMusic(audio);
    }

    IEnumerator CrossfadeTransition(int levelIndex) {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator CircleTransition(int levelIndex) {
        crossfade.SetTrigger("start1");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
