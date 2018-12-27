using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float minX = -.4f;
    float maxX = 50.3f;
    float minY;
    float maxY;

    GameObject player;

    Vector3 offset;
    Vector3 newPos;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update () {
        newPos = player.transform.position + offset;

        if(maxX >= newPos.x && newPos.x >= minX)
        {
            transform.position = newPos;
        }
    }
}
