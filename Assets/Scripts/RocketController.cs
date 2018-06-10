using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketController : MonoBehaviour {

  public float rotateSpeed = 2f;
  public float thrustForce = 10f;
  public float rotationSpeedLimit = 5f;
  public float translationSpeedLimit = 10f;
  public bool controllable = false;

  private float rotateInput;
  private bool thrustInput;
  private bool onPad;
  private Rigidbody2D rb;
  private LaunchPadController myLaunchPad;
  private FieldObjectDestructor objDestructor;
  public GravityMaster[] gravityMasters;
  GameManager gameManager;

  AudioSource audioSource;
  public AudioClip blastoffSound;
  public AudioClip thrustSound;

  public ParticleSystem thrustEffect;
  ParticleSystem.EmissionModule thrustEmitter;

  private void Awake()
  {
    thrustEmitter = thrustEffect.emission;
  }

  // Use this for initialization
  void Start () {
    rb = this.GetComponent<Rigidbody2D>();
    rb.isKinematic = true;
    //rb.simulated = false;
    audioSource = GetComponent<AudioSource>();
    gravityMasters = FindObjectsOfType<GravityMaster>();
    objDestructor = GetComponent<FieldObjectDestructor>();
    gameManager = FindObjectOfType<GameManager>();

    thrustEmitter.enabled = false;
  }
	
	// Update is called once per frame
	void Update () {
    GetInputs();
    if (controllable && !objDestructor.isBeingDestroyed)
    {
      ApplyControlForces();
      ApplyExternalForces();
    }

    if (onPad)
    {
      //transform.localEulerAngles = new Vector3(0f, 0f, 90f);
      //transform.rotation = Quaternion.LookRotation(myLaunchPad.padOrientation, Vector3.forward);
      
      //transform.position = myLaunchPad.spawnPosition.position;
    }

  }
  private void FixedUpdate()
  {
    ApplySpeedLimits(); //Does this need to be done as late update? After physics calculations?
  }

  void GetInputs()
  {
    rotateInput = Input.GetAxis("Rotation");
    thrustInput = Input.GetButton("Thrust");
  }


  void ApplyControlForces()
  {
    rb.AddTorque(-rotateInput * rotateSpeed * Time.deltaTime);
    if (thrustInput)
    {
      rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
      //thrustEffect.Play();
      //Debug.Log("Thrusting");
      thrustEmitter = thrustEffect.emission;
      thrustEmitter.enabled = true;
      if (thrustSound != null)
      {
        //audioSource.Play();
      }
    }
    else
    {
      //thrustEffect.Stop();
      thrustEmitter = thrustEffect.emission;
      thrustEmitter.enabled = false;
      if (thrustSound != null)
      {
        //audioSource.Stop();
      }
    }

    if (objDestructor.isBeingDestroyed)
    {
      thrustEmitter.enabled = false;
    }
    
  }

  void ApplySpeedLimits()
  {
    Vector2 vel = rb.velocity;
    float speed = vel.magnitude;
    //Debug.Log("Speed: " + speed);
    if (speed > translationSpeedLimit)
    {
      rb.velocity = translationSpeedLimit * vel.normalized;
    }

    //float angularVel = rb.angularVelocity;
    //Debug.Log("Angular Vel: " + angularVel);
    //if (angularVel > rotationSpeedLimit)
    //{
    //  rb.angularVelocity = rotationSpeedLimit;
    //}

  }

  void ApplyExternalForces()
  {
    Vector2 forces = new Vector2();

    foreach (GravityMaster grav in gravityMasters)
    {
      forces += grav.GetGravityForce(this.transform.position);

    }
    if (rb != null)
    {
      rb.AddForce(forces * Time.deltaTime);
    }
    
    //Debug.Log("External force applied: " + forces);
  }

  public IEnumerator LaunchRoutine()
  {
    onPad = false;
    transform.SetParent(null);
    //rb.simulated = true;
    rb.isKinematic = false;
    rb.bodyType = RigidbodyType2D.Dynamic;
    rb.AddRelativeForce(Vector3.up * thrustForce/60, ForceMode2D.Impulse); //Translate vertically for X seconds

    thrustEffect.Play();
    thrustEmitter.enabled = true;

    
    if (blastoffSound != null)
    {
      audioSource.PlayOneShot(blastoffSound);
    }
    yield return new WaitForSeconds(1f);
    controllable = true;
    thrustEmitter.enabled = false;
    objDestructor.invincible = false;

    StartCoroutine(myLaunchPad.PrepareLaunchPad());
  }

  public void SetLaunchPad(LaunchPadController pad)
  {
    myLaunchPad = pad;
    onPad = true;
  }

  public void Escape()
  {
    /////Play some effect first
    objDestructor.invincible = true;
    gameManager.RocketEscaped();
    Destroy(this, 1f);
  }

  public void EnteredWormhole(Vector3 wormholePos)
  {
    /////Play some effect first
    objDestructor.invincible = true;
    gameManager.RocketEscaped();

    //Play an effect
    this.transform.DOScale(0f, 1.5f);
    this.transform.DOMove(wormholePos, 1.5f);

    Destroy(this, 1.6f);
  }

  public void EnteredBlackhole(Vector3 wormholePos)
  {
    /////Play some effect first
    objDestructor.invincible = true;
    gameManager.RocketDestroyed();

    //Play an effect
    this.transform.DOScale(0f, 1.5f);
    this.transform.DOMove(wormholePos, 1.5f);
    this.transform.DOLocalRotate(new Vector3(0f, 0f, 1080f), 1.5f, RotateMode.FastBeyond360);

    Destroy(this, 1.6f);
  }

  public void Destroyed()
  {
    gameManager.RocketDestroyed();
  }
}
