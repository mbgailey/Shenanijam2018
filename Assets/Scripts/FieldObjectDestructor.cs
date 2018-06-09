using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectDestructor : MonoBehaviour {

  public GameObject explodeEffect;
  public GameObject blackHoleEffect;
  public AudioClip crashSound;
  public AudioClip explodeSound;
  public AudioClip blackHoleSound;

  public AudioSource audioSource;

	// Use this for initialization
	void Start () {
    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  IEnumerator Explode()
  {
    float explodeDelay = 0.2f;

    if (crashSound != null)
    {
      audioSource.PlayOneShot(crashSound);
    }
    yield return new WaitForSeconds(explodeDelay);
    if (explodeEffect != null)
    {
      Instantiate(explodeEffect, transform.position, Quaternion.identity);
    }
    if (explodeSound != null)
    {
      audioSource.PlayOneShot(explodeSound);
    }

    Destroy(this.gameObject);
  }

  IEnumerator BlackHoleDeath()
  {
    float explodeDelay = 1f;

    if (blackHoleSound != null)
    {
      audioSource.PlayOneShot(crashSound);
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
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {

    if (collision.CompareTag("Blackhole"))
    {
      StartCoroutine(BlackHoleDeath());
    }
  }
}
