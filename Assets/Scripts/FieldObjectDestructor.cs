﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectDestructor : MonoBehaviour {

  public bool invincible = true;
  public ParticleSystem explodeEffect;
  private ParticleSystem.EmissionModule explodeEmitter;
  public GameObject blackHoleEffect;
  public AudioClip crashSound;
  public AudioClip explodeSound;
  public AudioClip blackHoleSound;

  public AudioSource audioSource;

	// Use this for initialization
	void Start () {
    audioSource = GetComponent<AudioSource>();
    explodeEmitter = explodeEffect.emission;

  }
	
	// Update is called once per frame
	void Update () {
		
	}

  IEnumerator Explode()
  {
    float explodeDelay = 0.2f;

    //Start break apart effect

    if (crashSound != null)
    {
      audioSource.PlayOneShot(crashSound);
    }
    yield return new WaitForSeconds(explodeDelay);

    if (explodeEffect != null)
    {
      explodeEffect.Play();
      explodeEmitter.enabled = true;
      //Instantiate(explodeEffect, transform.position, Quaternion.identity);
    }
    this.GetComponent<SpriteRenderer>().enabled = false;
    this.GetComponent<PolygonCollider2D>().enabled = false;

    if (explodeSound != null)
    {
      audioSource.PlayOneShot(explodeSound);
    }

    Destroy(this.gameObject, 2f); //Wait to destroy in case stuff is still going on
  }

  IEnumerator BlackHoleDeath()
  {
    float explodeDelay = 1f;

    if (blackHoleSound != null)
    {
      audioSource.PlayOneShot(blackHoleSound);
    }

    if (blackHoleEffect != null)
    {
      Instantiate(blackHoleEffect, transform.position, Quaternion.identity);
    }

    yield return new WaitForSeconds(explodeDelay);

    Destroy(this.gameObject);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (invincible)
      return;

    if (collision.collider.CompareTag("Planet"))
    {
      StartCoroutine(Explode());
    }

    if (collision.collider.CompareTag("Rocket"))
    {
      StartCoroutine(Explode());
    }

    if (collision.collider.CompareTag("Blackhole"))
    {
      StartCoroutine(Explode());
    }

    if (collision.collider.CompareTag("Debris"))
    {
      StartCoroutine(Explode());
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {

    if (collision.CompareTag("Blackhole"))
    {
      StartCoroutine(BlackHoleDeath());
    }
  }
}
