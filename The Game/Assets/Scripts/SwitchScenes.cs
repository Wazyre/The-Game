using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public string sceneName;
    public float xPosition;
    public float yPosition;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player") {
             other.transform.position = new Vector3(xPosition, yPosition, 0);
             PlayerState.Instance.SavePlayer();
             SceneManager.LoadScene(sceneName);
        }
    }
}
