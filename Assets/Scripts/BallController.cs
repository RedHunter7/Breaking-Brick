using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    public static int health , brick;
    GameObject health_1 , health_2, health_3;
    GameObject panelGameOver , panelSuccess , panelPause , panelFail , startText;
    GameObject pauseBtn;
    GameObject paddle;
    AudioSource audio;
    public AudioClip brickSound , metalSound , edgeSound, loseSound;
    Text healthAmount;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();   
        health = 3;
        brick = 22;
        panelGameOver = GameObject.Find("PanelGameOver");
        panelGameOver.SetActive(false);
        panelSuccess = GameObject.Find("PanelSuccess");
        panelSuccess.SetActive(false);
        paddle = GameObject.Find("paddle");
        panelPause = GameObject.Find("PauseCanvas");
        panelFail = GameObject.Find("PanelFail");
        panelFail.SetActive(false);
        audio = GetComponent<AudioSource> ();
        healthAmount = panelFail.transform.Find("HealthAmount").GetComponent<Text>();
        startText = GameObject.Find("StartText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (transform.parent == paddle.transform) {
                startText.SetActive(false);
                 transform.parent = null;
                 Vector2 arah = new Vector2(0,2).normalized;
                 rigid.AddForce(arah*force);
            }
        }   
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name == "TepiBawah") {
            audio.PlayOneShot(loseSound);
            health = health - 1;
            
            if (health == 2 || health == 1) {
                panelFail.SetActive(true);
                healthAmount.text = "Health : " + health;
                Time.timeScale = 0;
                if(health == 2) {
                    health_1 = GameObject.Find("health_1");
                    Destroy(health_1);
                }
                else if(health == 1) {
                    health_2 = GameObject.Find("health_2");
                    Destroy(health_2);
                }   
            }
            else if(health == 0) {
                Destroy(panelPause);
                panelGameOver.SetActive(true);
                health_3 = GameObject.Find("health_3");
                Destroy(health_3);
                Destroy(gameObject);
                return;
            }
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
                panelSuccess.SetActive(true);
            }
        }
    }

    public void ResetGame() {
        transform.parent = paddle.transform;
        rigid.velocity = new Vector2(0,0);
        transform.localPosition = new Vector2(0, 1);
        paddle.transform.position = new Vector2(0,-4);
        panelFail.SetActive(false);
        Time.timeScale = 1f;
    }
}
