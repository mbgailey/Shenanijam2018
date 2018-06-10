﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMaster : MonoBehaviour {

  public float gravityMass;
  float multiplier = 2f;

	// Use this for initialization
	void Start () {
		
	}
	

	public Vector2 GetGravityForce (Vector3 pos) {
    Vector2 force = new Vector2();

    float sqDist = (this.transform.position - pos).sqrMagnitude;
    float magnitude = multiplier * gravityMass / sqDist;

    force = (this.transform.position - pos) * magnitude;

    return force;
	}
}
