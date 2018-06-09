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
    Debug.Log("OnTriggerEnter " + collision.name.ToString());
    if (collision.CompareTag("Rocket"))
    {
      gameManager.RocketEscaped();
    }

    else if (collision.CompareTag("Debris"))
    {
      GameObject.Destroy(collision.gameObject, 1f);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("OnTriggerEnter " + collision.name.ToString());
  }

}
