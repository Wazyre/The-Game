using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public static bool claw;
    public static bool gun;
    public static bool sword;
    public static bool spear;
    public static bool shotgun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Claw")
        {
            claw = true;
        }
        else if(other.gameObject.tag == "Gun")
        {
            gun = true;
        }
        else if(other.gameObject.tag == "Sword")
        {
            sword = true;
        }
        else if(other.gameObject.tag == "Spear")
        {
            spear = true;
        }
        else if(other.gameObject.tag == "Shotgun")
        {
            shotgun = true;
        }
    }
}
