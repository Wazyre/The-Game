using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public Scene scene;

    //Player's beginning position in new scene
    public float m_xPos;
    public float m_yPos;

    public Scene getScene() {return scene;}

    public float xPos
    {
      get{return m_xPos;}
      set{m_xPos = value;}
    }

    public float yPos
    {
      get{return m_yPos;}
      set{m_yPos = value;}
    }
}
