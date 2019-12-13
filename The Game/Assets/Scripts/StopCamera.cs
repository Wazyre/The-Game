using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    public bool stopFollow = false;

    public void StopFollow()
    {
        stopFollow = !stopFollow;
    }
}
