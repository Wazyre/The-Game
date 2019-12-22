using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public string name;
    public bool interactable; //If the npc can be interacted with
    public float interactRadius = 2f;

    Animator animator;
    GameObject player;
    LayerMask trigger;
    Text interactText;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle"); //Begin with Idle animation
        player = GameObject.FindGameObjectWithTag("Player");
        trigger = LayerMask.GetMask("Player");
        interactText = GameObject.Find("Interact").GetComponent<Text>();
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, trigger);

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
