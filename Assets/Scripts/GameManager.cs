using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  GameBounds gameBounds;
  public int escapeCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



  public void RocketEscaped()
  {
    escapeCount++;

    Debug.Log("Another one escaped");
  }
}
