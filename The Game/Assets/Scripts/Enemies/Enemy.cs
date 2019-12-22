using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    int m_currentHealth = 0;
    int m_maxHealth = 0;
    int m_damage = 0;
    int m_range = 0;

    float m_speed = 0f;
    public float distance = 0.2f;

    public Vector2 checkOffset = new Vector2(0.1f, 0.1f);
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

        if(hitEdge)
        {
           faceRight = !faceRight;
           m_speed *= -1;
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

    public float speed
    {
      get{return m_speed;}
      set{m_speed = value;}
    }

    public bool faceRight
    {
        get
        {
            return transform.localEulerAngles.y == 180f;
        }
        set
        {
            transform.localEulerAngles = new Vector3(0f, value ? 180f : 0f);
        }
    }

    public int direction => faceRight ? 1 : -1;

    public bool hitEdge
    {
        get
        {
          var checkPosition = new Vector2(transform.position.x + checkOffset.x * direction, transform.position.y + checkOffset.y);
          var hitInfo = Physics2D.Raycast(checkPosition, Vector2.down, distance);
          return !hitInfo;
        }
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
