using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerState : MonoBehaviour
{
    public Text healthText;
    public Text diseaseText;

    float maxHealth = 100.0f;
    //float currentHealth;
    float maxDisease = 100.0f;
    //float currentDisease;

    bool onDanger;
    //bool fullDisease;

    Animator anim;
    Slider healthBar;
    Slider diseaseBar;

    public PlayerStatistics localPlayerData = new PlayerStatistics();

    public static PlayerState Instance;

    void Awake()
    {
        anim = GetComponent<Animator>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        diseaseBar = GameObject.FindGameObjectWithTag("DiseaseBar").GetComponent<Slider>();
        localPlayerData = GlobalControl.Instance.savedPlayerData;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "";
        diseaseText.text = "";
        //StartCoroutine(StayOnHazard());
        healthBar.value = CalculateHealthBar();
        diseaseBar.value = CalculateDiseaseBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
        UpdateDiseaseText();
        CheckHealth();
        CheckDisease();
        //StartCoroutine(StayOnHazard());
        localPlayerData.disease += 0.1f*Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Puddle")
        {
            //currentHealth = maxHealth;
            localPlayerData.disease = 0;
        }
        if(other.gameObject.tag == "Spikes")
        {
            localPlayerData.hp -= 10;
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
            localPlayerData.hp -= 10;
            yield return new WaitForSeconds(1);
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "HP: " + localPlayerData.hp.ToString();
    }

    void UpdateDiseaseText()
    {
        if(localPlayerData.disease%2 == 0)
        {
            diseaseText.text = "Disease: " + localPlayerData.disease.ToString();
        }
    }

//-----------------------------------------------------------------

    void CheckHealth()
    {
        if(localPlayerData.hp > maxHealth)
        {
            localPlayerData.hp = maxHealth;
        }
        if(localPlayerData.hp <= 0)
        {
            localPlayerData.hp = 0;
            //Die();
        }
        healthBar.value = CalculateHealthBar();
    }

    float CalculateHealthBar()
    {
        return localPlayerData.hp / maxHealth;
    }

    public void Heal()
    {
        localPlayerData.hp = maxHealth;
    }

//-------------------------------------------------------------------

    void CheckDisease()
    {
        if(localPlayerData.disease > maxDisease)
        {
            localPlayerData.disease = maxDisease;
        }
        if(localPlayerData.disease >= 90)
        {
            anim.SetBool("isCrouching", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            //fullDisease = true;
        }
        if(localPlayerData.disease < 90)
        {
            anim.SetBool("isCrouching", false);
            //fullDisease = false;
        }
        diseaseBar.value = CalculateDiseaseBar();
    }

    float CalculateDiseaseBar()
    {
        return localPlayerData.disease / maxDisease;
    }

    public void RemoveDisease()
    {
        localPlayerData.disease -= maxDisease*0.25f;
    }

//-------------------------------------------------------------------

    void Die()
    {
        Destroy(gameObject);
    }

//--------------------------------------------------------------------

    public void SavePlayer()
    {
        GlobalControl.Instance.savedPlayerData = localPlayerData;
    }
}
