using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool hasInteracted = false;
    public bool isFocus = false;

    public float radius = 2f;

    Transform playerT;
    LayerMask trigger;

    void Start()
    {
        trigger = LayerMask.GetMask("Player");
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        playerT = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        playerT = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, trigger);

        if(player != null)
        {
            hasInteracted = true;
            OnFocused(player.transform);
        }
        else
        {
            OnDefocused();
        }

        if(isFocus)
        {
            float distance = Vector2.Distance(playerT.position, transform.position);
            if(distance <= radius)
            {

            }
        }
    }
}
