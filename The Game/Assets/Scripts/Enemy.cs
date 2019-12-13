using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int m_currentHealth;
    int m_maxHealth = 1;
    int m_damage = 1;
    int m_range = 1;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Body and health of enemy
        rb = GetComponent<Rigidbody2D>();
        m_currentHealth = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy enemy if health lower than 0
        if(m_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int currentHealth
    {
      get{return m_currentHealth;}
    }

    public int maxHealth
    {
      get{return m_maxHealth;}
    }

    public int damage
    {
      get{return m_damage;}
    }

    public int range
    {
      get{return m_range;}
    }

    void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
    }

    //Knockback after being attacked
    void Knockback(int force)
    {
        rb.AddForce(new Vector2(rb.velocity.x*-force, 0));
    }
}
