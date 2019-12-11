﻿using System.Collections;
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
        //Applies a collider to detect player
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        //This is for doors, entryways...etc. NOT EDGE OF SCREEN TRANSITION
        if(col != null && Input.GetKeyDown("E")) //If player is there and pressing E
        {
            newScene = col.GetComponent<NewScene>();
            ChangeScene(sceneName.scene, newScene.xPos, newScene.yPos);
        }
    }

    public void ChangeScene(Scene scene, float xPos, float yPos)
    {
        StartCoroutine(fade.FadeInOut(fps, fps, blackScreen, delay)); //Begins fade to black
        player.transform.position = new Vector3(xPos, yPos, 0); //Changes player's position in new scene
        PlayerState.Instance.SavePlayer(); //Save data
        SceneManager.LoadScene(scene); //Load new scene
    }
}
