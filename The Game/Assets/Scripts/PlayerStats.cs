using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text healthText;
    public Text diseaseText;

    float maxHealth = 100.0f;
    float currentHealth;
    float maxDisease = 100.0f;
    float currentDisease;

    bool onDanger;
    bool fullDisease;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentDisease = 0;
        healthText.text = "";
        diseaseText.text = "";
        StartCoroutine(StayOnHazard());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
        UpdateDiseaseText();
        CheckHealth();
        CheckDisease();
        //StartCoroutine(StayOnHazard());
        currentDisease += 0.1f*Time.deltaTime;
        Debug.Log(currentDisease);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Puddle")
        {
            //currentHealth = maxHealth;
            currentDisease = 0;
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

    void UpdateHealthText()
    {
        healthText.text = "HP: " + currentHealth.ToString();
    }

    void UpdateDiseaseText()
    {
        if(currentDisease%2 == 0)
        {
            diseaseText.text = "Disease: " + currentDisease.ToString();
        }
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
            Die();
        }
    }

    void CheckDisease()
    {
        if(currentDisease > maxDisease)
        {
            currentDisease = maxDisease;
        }
        if(currentDisease >= 90)
        {
            anim.SetBool("isCrouching", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            fullDisease = true;
        }
        if(fullDisease && currentDisease < 90)
        {
            anim.SetBool("isCrouching", false);
            fullDisease = false;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
