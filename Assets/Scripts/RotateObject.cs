using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

  public float xSpeed = 5f;
  public float ySpeed = 5f;
  public float zSpeed = 5f;

  // Use this for initialization
  void Start () {
    xSpeed = Random.Range(-40f, 40f);
    ySpeed = Random.Range(-40f, 40f);
    zSpeed = Random.Range(-40f, 40f);
  }
	
	// Update is called once per frame
	void Update () {
    this.transform.Rotate(new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime);
	}
}
