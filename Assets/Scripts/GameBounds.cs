using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour {

  BoxCollider2D gameBounds;
  GameManager gameManager;

	// Use this for initialization
	void Start () {
    gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnCollisionExit2D(Collision2D collision)
  {
    


  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("Rocket"))
    {
      gameManager.RocketEscaped();
    }
  }

}
