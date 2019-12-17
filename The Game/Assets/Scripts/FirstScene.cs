using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstScene : MonoBehaviour
{
    float fps = 3f;
    float delay = 1f;

    bool ran = false;

    Text introText;
    Fade fade;
    PlayerControlMapping control;

    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        control = player.GetComponent<PlayerControlMapping>();
        GameObject intro = GameObject.Find("WakeUp");
        introText = intro.GetComponent<Text>();
        fade = GetComponent<Fade>();
        introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 0);
    }

    void Start()
    {
        player.GetComponent<PlayerState>().Heal();
    }

    void Update()
    {
        if(!ran)
        {
            StartCoroutine(fade.FadeTextInOut(fps, fps, introText, delay));
            StartCoroutine(control.ToggleInput(delay*2));
            ran = true;
        }
    }
}
