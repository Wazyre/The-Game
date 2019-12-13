using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float minX = -.4f;
    float maxX = 50.3f;
    float minY;
    float maxY;
    float smoothTime = 0.4f;
    float newPosX;
    float newPosY;
    float camVelocity = 0.0f;

    bool notFollowing;

    GameObject player;

    Vector3 offset;
    Vector3 newPos;

    Interactable focus;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        focus = player.GetComponent<PlayerMechanics>().focus;
        offset = transform.position - player.transform.position;
        notFollowing = GetComponent<StopCamera>().stopFollow;
    }

    // Update is called once per frame
    void Update() {

        notFollowing = GetComponent<StopCamera>().stopFollow;
        if(!notFollowing)
        {
            newPos = player.transform.position + offset;

            if(maxX >= newPos.x && newPos.x >= minX)
            {
                transform.position = newPos;
            }
        }
        else
        {
            focus = player.GetComponent<PlayerMechanics>().focus;
            Vector2 pos = focus.gameObject.transform.position;

            /*if(transform.position.x < pos.x)
            {
                for(float i = transform.position.x; i < pos.x; i++)
                {
                    transform.position = new Vector3(i, transform.position.y, transform.position.z);
                }
            }
            else
            {
                for(float i = transform.position.x; i > pos.x; i--)
                {
                    transform.position = new Vector3(i, transform.position.y, transform.position.z);
                }
            }*/
            newPosX = Mathf.SmoothDamp(transform.position.x, pos.x, ref camVelocity, smoothTime);
            transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
        }
    }
}
