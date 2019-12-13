using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    bool isFollow = false; //Checks if camera is following the player

    public bool IsFollowing() {return isFollow;}

    public void ChangeFollow()
    {
        isFollow = !isFollow;
    }
}
