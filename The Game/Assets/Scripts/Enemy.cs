using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int currentHealth;
    int maxHealth = 100;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Body and health of enemy
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy enemy if health lower than 0
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    //Knockback after being attacked
    void Knockback(int force)
    {
        rb.AddForce(new Vector2(rb.velocity.x*-force, 0));
    }
}
