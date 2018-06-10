using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("Debris"))
    {
      collision.GetComponent<Rigidbody2D>().isKinematic = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Debris"))
    {
      collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("Debris"))
    {
      collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
  }

}
