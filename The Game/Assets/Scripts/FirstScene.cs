using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstScene : MonoBehaviour
{
    float fps = 3f;
    float delay = 5f;

    bool ran = false;

    Text introText;
    Fade fade;

    void Awake()
    {
        GameObject intro = GameObject.Find("WakeUp");
        introText = intro.GetComponent<Text>();
        fade = GetComponent<Fade>();
        introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 0);
    }

    void Update()
    {
        if(!ran)
        {
            StartCoroutine(fade.FadeInOut(fps, fps, introText, delay));
            ran = true;
        }
    }
}
