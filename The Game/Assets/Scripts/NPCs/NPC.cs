using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public string name;
    public bool interactable; //If the npc can be interacted with
    public float interactRadius = 2f;

    Animator animator;
    GameObject player;
    GameObject interactText;
    LayerMask trigger;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle"); //Begin with Idle animation
        player = GameObject.FindGameObjectWithTag("Player");
        trigger = LayerMask.GetMask("Player");
        interactText = GameObject.Find("Interact");
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, interactRadius, trigger);

        if(player != null) //If the player is within the radius
        {
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false); //Remove camera focus from it
        }
    }
}
