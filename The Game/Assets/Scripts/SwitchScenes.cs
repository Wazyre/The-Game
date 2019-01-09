using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    float fps = 3f;
    float delay = 4f;
    float radius = 1f;

    Fade fade;
    NewScene newScene;

    GameObject blackScreen;
    GameObject player;

    LayerMask playerLayer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        fade = blackScreen.GetComponent<Fade>();
        playerLayer = LayerMask.GetMask("Player");
    }

    /*void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player") {
             other.transform.position = new Vector3(xPosition, yPosition, 0);
             PlayerState.Instance.SavePlayer();
             SceneManager.LoadScene(sceneName);
        }
    }*/

    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if(col != null && Input.GetKeyDown("E"))
        {
            newScene = col.GetComponent<NewScene>();
            ChangeScene(sceneName.scene, newScene.xPos, newScene.yPos);
        }
    }

    public void ChangeScene(Scene scene, float xPos, float yPos)
    {
        StartCoroutine(fade.FadeInOut(fps, fps, blackScreen, delay));
        player.transform.position = new Vector3(xPos, yPos, 0);
        PlayerState.Instance.SavePlayer();
        SceneManager.LoadScene(scene);
    }
}
