using System.Collections;
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

  public bool isBeingDestroyed = false;

  RocketController rocketController;
  CameraShake camShake;

  public GameObject flavorText;

	// Use this for initialization
	void Start () {
    audioSource = GetComponent<AudioSource>();
    explodeEmitter = explodeEffect.emission;
    rocketController = GetComponent<RocketController>();
    camShake = FindObjectOfType<CameraShake>();
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  IEnumerator Explode()
  {
    if (!isBeingDestroyed)
    {

      //Debug.Log("Explode");

      float explodeDelay = 0.2f;

      //Start break apart effect
      isBeingDestroyed = true;

      if (crashSound != null)
      {
        audioSource.PlayOneShot(crashSound);
      }
      yield return new WaitForSeconds(explodeDelay);

      StartCoroutine(camShake.Shake());
      rocketController.Destroyed();

      if (explodeEffect != null)
      {
        explodeEffect.Play();
        explodeEmitter.enabled = true;
        //Instantiate(explodeEffect, transform.position, Quaternion.identity);
      }

      GameObject flavor = Instantiate(flavorText, this.transform.position, Quaternion.identity);
      flavor.transform.position = this.transform.position;
      flavor.GetComponent<DialogueController>().InitializeText_explode();
      flavor.GetComponent<DialogueController>().SetPath(this.transform.position);

      this.GetComponent<SpriteRenderer>().enabled = false;
      this.GetComponent<PolygonCollider2D>().enabled = false;

      if (explodeSound != null)
      {
        audioSource.PlayOneShot(explodeSound);
      }

      Destroy(this.gameObject, 2f); //Wait to destroy in case stuff is still going on
    }
  }

  void BlackHoleDeath()
  {
    GameObject flavor = Instantiate(flavorText, this.transform.position, Quaternion.identity);
    flavor.transform.position = this.transform.position;
    flavor.GetComponent<DialogueController>().InitializeText_blackhole();
    flavor.GetComponent<DialogueController>().SetPath(this.transform.position);
    //Debug.Log("BH");

    isBeingDestroyed = true;


    if (blackHoleSound != null)
    {
      audioSource.PlayOneShot(blackHoleSound);
    }

    if (blackHoleEffect != null)
    {
      Instantiate(blackHoleEffect, transform.position, Quaternion.identity);
    }

    //yield return new WaitForSeconds(explodeDelay);

    //Destroy(this.gameObject);
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
      BlackHoleDeath();
      rocketController.EnteredBlackhole(collision.transform.position);
      
    }
  }
}
