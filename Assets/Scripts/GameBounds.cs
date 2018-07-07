using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour {

  //BoxCollider2D gameBounds;
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

  private void OnTriggerExit2D(Collider2D collider)
  {
    //Debug.Log("OnTriggerEnter " + collision.name.ToString());
    if (collider.CompareTag("Rocket"))
    {
      if (!collider.GetComponent<FieldObjectDestructor>().isBeingDestroyed)
      {
        //collision.GetComponent<RocketController>().Escape();
        collider.GetComponent<RocketController>().Destroyed();
      }


      //gameManager.RocketEscaped();
    }

    else if (collider.CompareTag("Debris"))
    {
      if(collider.GetComponent<DebrisController>().activeOnScreen == true)
      {
        GameObject.Destroy(collider.gameObject, 1f);
      }
    }
  }
  

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Debris"))
    {
      collider.GetComponent<DebrisController>().activeOnScreen = true;


    }
  }

}
