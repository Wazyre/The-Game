﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool hasInteracted = false; //Has been interacted with
    public bool isFocus = false; //Is the current focus for the camera

    public float radius = 2f; //Radius for interactability

    //Transform playerT;
    LayerMask trigger;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trigger = LayerMask.GetMask("Player");
    }

    /*public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        //playerT = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        //playerT = null;
    }*/

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, trigger);

        if(player != null) //If the player is within the radius
        {
            isFocus = true; //Turn camera focus on it
        }
        else
        {
            isFocus = false; //Remove camera focus from it
        }

        if(isFocus)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if(distance <= radius) //THIS WILL ALWAYS BE TRUE>>>WHAT?
            {

            }
        }
    }
}
