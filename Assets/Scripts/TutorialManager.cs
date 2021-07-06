using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    public int brick , tutorIndex;
    GameObject panelSuccess , panelPause;
    GameObject[] panelTutorial = new GameObject[4];
    GameObject pauseBtn;
    GameObject paddle;
    GameObject bricksGroup;
    AudioSource audio;
    public AudioClip brickSound , metalSound , edgeSound, loseSound;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();   
        panelSuccess = GameObject.Find("PanelSuccess");
        panelSuccess.SetActive(false);
        paddle = GameObject.Find("paddle");
        panelPause = GameObject.Find("PauseCanvas");
        audio = GetComponent<AudioSource> ();
        bricksGroup = GameObject.Find("Bricks Group Object");
        bricksGroup.SetActive(false);
        tutorIndex = 0;
        brick = 4;

        for (int i = 0; i < 4; i++)
        {
            string x = "PanelTutorial" + (i+1);
            panelTutorial[i] = GameObject.Find(x);
            panelTutorial[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (transform.parent == paddle.transform) {
                 transform.parent = null;
                 Vector2 arah = new Vector2(0,2).normalized;
                 rigid.AddForce(arah*force);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if(i == tutorIndex) {
                panelTutorial[i].SetActive(true);
            } else {
                panelTutorial[i].SetActive(false);
            }
        }

        if(tutorIndex == 0) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ) {
                tutorIndex++;
            }
        } else if (tutorIndex == 1) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                tutorIndex++;
            }
        } else if (tutorIndex == 2) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                tutorIndex++;
            }
        } else if (tutorIndex == 3) {
            bricksGroup.SetActive(true);
        }  
    }

        private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name == "TepiBawah") {
            audio.PlayOneShot(loseSound);
            ResetGame();
            }
            
        if (coll.gameObject.name == "paddle") {
            if (transform.parent != paddle.transform) {
                audio.PlayOneShot(edgeSound);
            }
            
            float sudut = (transform.position.x - coll.transform.position.x)*5f;
            Vector2 arah = new Vector2(sudut, rigid.velocity.y).normalized;
            rigid.velocity = new Vector2(0,0);
            rigid.AddForce(arah * force * 1.6f);
        }
        if (coll.gameObject.tag == "Metal") {
            audio.PlayOneShot(metalSound);
        }
        if (coll.gameObject.tag == "Edge") {
            audio.PlayOneShot(edgeSound);
        }
        if (coll.gameObject.tag == "Brick") {
            audio.PlayOneShot (brickSound);
            brick = brick - 1;
            Destroy(coll.gameObject);
            if (brick == 0) {
                Destroy(panelPause);
                Destroy(gameObject);
                panelTutorial[3].SetActive(false);
                panelSuccess.SetActive(true);
            }
        }
    }

    public void ResetGame() {
        transform.position = new Vector2(0,-3.5f);
        rigid.velocity = new Vector2(0,0);
        paddle.transform.position = new Vector2(0,-4);
        transform.parent = paddle.transform;
        Time.timeScale = 1f;
    }
}
