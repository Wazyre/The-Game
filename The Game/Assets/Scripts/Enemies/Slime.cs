using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slime : Enemy
{
    int m_maxHealth = 10;
    int m_damage = 1;
    int m_range = 1;

    float m_speed = 1f;
}
