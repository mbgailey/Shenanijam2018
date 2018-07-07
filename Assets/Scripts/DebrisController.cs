using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour {

  public float rotateSpeed = 4f;
  private Rigidbody2D rb;
  public bool activeOnScreen = false;

  // Use this for initialization
  void Start () {
    rb = this.GetComponent<Rigidbody2D>();
  }
	
	// Update is called once per frame
	void Update () {
    rb.angularVelocity = rotateSpeed;
  }

  //private void OnTriggerEnter2D(Collider2D collider)
  //{
  //  if (collider.gameObject.CompareTag("Gamebounds"))
  //  {
  //    activeOnScreen = true;
  //  }
  //}

  //private void OnTriggerExit2D(Collider2D collider)
  //{
  //  if (collider.gameObject.CompareTag("Gamebounds") && activeOnScreen)
  //  {
  //    Destroy(this);
  //  }
  //}

}
