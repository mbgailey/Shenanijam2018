using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackholeController : MonoBehaviour {

  public float rotateSpeed = 4f;
  public Transform starburst1;
  public Transform starburst2;
  public Transform mainScaleTransform;
  private float currentScale;

  float starburst1Speed = 1f;
  float starburst2Speed = 1.5f;

  private Rigidbody2D rb;

  private GravityMaster gravityMaster;
  float baseMass;
  float pulseSize;
  float pulsePeriod;
  //float growSpeed = 0.03f; //Appx. 1 min game
  public float growSpeed = 0.001f;
  public bool doPulse = true;
  GameManager gameManager;

  // Use this for initialization
  void Start()
  {
    rb = this.GetComponent<Rigidbody2D>();
    gravityMaster = this.GetComponent<GravityMaster>();
    gameManager = FindObjectOfType<GameManager>();
    if (gravityMaster != null)
    {
      baseMass = gravityMaster.gravityMass;
    }
    currentScale = mainScaleTransform.localScale.x;
    if (doPulse)
    {
      StartPulse();
    }
    
  }

  // Update is called once per frame
  void Update()
  {
    starburst1.Rotate(0f, 0f, starburst1Speed * Time.deltaTime);
    starburst2.Rotate(0f, 0f, starburst2Speed * Time.deltaTime);
    rb.angularVelocity = rotateSpeed;
    if(gravityMaster != null)
    {
      gravityMaster.gravityMass = transform.localScale.x * baseMass;
    }
    
    RunGrow();
  }

  void RunGrow()
  {
    currentScale += growSpeed * Time.deltaTime;
    mainScaleTransform.localScale = new Vector2(currentScale, currentScale);
  }

  public void StartPulse()
  {
    pulseSize = Random.Range(1.1f, 2f);
    pulsePeriod = Random.Range(8f, 20f);
    


    transform.DOScale(pulseSize, pulsePeriod).SetLoops(-1, LoopType.Yoyo);
    
  }
  public void EndPulse()
  {
    DOTween.Kill(transform);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Planet"))
    {
      Debug.Log("Blackhole collided with planet");
      if (!gameManager.gameOver)
      {
        StartCoroutine(gameManager.EndGameSequence());
      }
      
    }
  }

}
