using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text healthText;
    int maxHealth = 100;
    int currentHealth;
    int maxMana = 100;
    int currentMana;
    bool onDanger;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        healthText.text = "";
        StartCoroutine(StayOnHazard());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        CheckHealth();
        StartCoroutine(StayOnHazard());

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Puddle")
        {
            currentHealth = maxHealth;
        }
        if(other.gameObject.tag == "Spikes")
        {
            currentHealth -= 10;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
      if(other.gameObject.tag == "Spikes")
      {
          onDanger = true;
      }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Spikes")
        {
            onDanger = false;
        }
    }
    IEnumerator StayOnHazard()
    {
        while (onDanger)
        {
            currentHealth -= 10;
            yield return new WaitForSeconds(1);
        }
    }

    void UpdateHealth()
    {
        healthText.text = "HP: " + currentHealth.ToString();
    }

    void CheckHealth()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
