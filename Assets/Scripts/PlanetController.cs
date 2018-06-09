using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

  public float rotateSpeed = 4f;
  private Rigidbody2D rb;

  

  // Use this for initialization
  void Start ()
  {
    rb = this.GetComponent<Rigidbody2D>();
  }
	
	// Update is called once per frame
	void Update () {
    rb.angularVelocity = rotateSpeed;
  }

  
}
