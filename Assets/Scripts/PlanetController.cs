using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlanetController : MonoBehaviour {

  public float rotateSpeed = 4f;
  private Rigidbody2D rb;

  public SpriteRenderer main;
  public SpriteRenderer explode0;

  public GameObject[] pieces;
  public GameObject[] launchpads;
  public SpriteRenderer atmosphere;

  // Use this for initialization
  void Start ()
  {
    rb = this.GetComponent<Rigidbody2D>();
  }
	
	// Update is called once per frame
	void Update () {
    rb.angularVelocity = rotateSpeed;
  }

  public IEnumerator DestroyPlanet()
  {
    //Startshaking planet immediately
    this.transform.DOShakePosition(3f, 0.05f, 20);

    yield return new WaitForSeconds(1f);

    explode0.enabled = true;
    main.DOFade(0f, 2f);
    yield return new WaitForSeconds(3f); //Now showing cracked earth

    explode0.enabled = false;  //Hide cracked earth;
    foreach(GameObject go in pieces) //Show all pieces
    {
      go.GetComponent<SpriteRenderer>().enabled = true;

    }
    atmosphere.DOFade(0f, 1f);
    foreach (GameObject go in pieces)
    {
      go.transform.DOShakePosition(1f, 0.1f);
    }
    foreach (GameObject go in launchpads) //Scale down to nothing
    {
      go.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
    }
    yield return new WaitForSeconds(1f); //Ready to send away

    foreach (GameObject go in pieces) //Scale down to nothing
    {
      go.transform.DOScale(0f, 3f);
    }
  }

  
}
