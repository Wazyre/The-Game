using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenLevels : MonoBehaviour
{
  GameObject player;

  // Start is called before the first frame update
  void Start()
  {
      player = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update()
  {
      player.GetComponent<PlayerControlMapping>().NoInput();
  }
}
