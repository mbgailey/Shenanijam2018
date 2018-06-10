using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WormholeController : MonoBehaviour {

  public float rotateSpeed = 4f;
  public Transform starburst1;
  public Transform starburst2;
  private float currentScale;

  float starburst1Speed = 1f;
  float starburst2Speed = 1.5f;

  private Rigidbody2D rb;
  GameManager gameManager;

  // Use this for initialization
  void Start()
  {
    rb = this.GetComponent<Rigidbody2D>();
    gameManager = FindObjectOfType<GameManager>();

  }

  // Update is called once per frame
  void Update()
  {
    starburst1.Rotate(0f, 0f, starburst1Speed * Time.deltaTime);
    starburst2.Rotate(0f, 0f, starburst2Speed * Time.deltaTime);
    rb.angularVelocity = rotateSpeed;

  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Rocket"))
    {
      Debug.Log("Rocket entered wormhole");
      collision.GetComponent<RocketController>().EnteredWormhole();
    }
  }

}
