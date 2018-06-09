using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  public GravityMaster[] gravityMasters;

  AudioSource audioSource;
  public AudioClip blastoffSound;
  public AudioClip thrustSound;

  public ParticleSystem thrustEffect;
  ParticleSystem.EmissionModule thrustEmitter;

  // Use this for initialization
  void Start () {
    rb = this.GetComponent<Rigidbody2D>();
    rb.isKinematic = true;
    //rb.simulated = false;
    audioSource = GetComponent<AudioSource>();
    gravityMasters = FindObjectsOfType<GravityMaster>();
    thrustEmitter = thrustEffect.emission;
    thrustEmitter.enabled = false;
  }
	
	// Update is called once per frame
	void Update () {
    GetInputs();
    if (controllable)
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
      rb.AddRelativeForce(Vector3.right * thrustForce * Time.deltaTime);
      //thrustEffect.Play();
      Debug.Log("Thrusting");
      thrustEmitter.enabled = true;
      if (thrustSound != null)
      {
        //audioSource.Play();
      }
    }
    else
    {
      //thrustEffect.Stop();

      thrustEmitter.enabled = false;
      if (thrustSound != null)
      {
        //audioSource.Stop();
      }
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

    rb.AddForce(forces * Time.deltaTime);
    //Debug.Log("External force applied: " + forces);
  }

  public IEnumerator LaunchRoutine()
  {
    onPad = false;
    transform.SetParent(null);
    //rb.simulated = true;
    rb.isKinematic = false;
    rb.bodyType = RigidbodyType2D.Dynamic;
    rb.AddRelativeForce(Vector3.right * thrustForce/15, ForceMode2D.Impulse); //Translate vertically for X seconds

    thrustEmitter.enabled = true;
    if (blastoffSound != null)
    {
      audioSource.PlayOneShot(blastoffSound);
    }
    yield return new WaitForSeconds(1f);
    controllable = true;
    thrustEmitter.enabled = false;
    StartCoroutine(myLaunchPad.PrepareLaunchPad());
  }

  public void SetLaunchPad(LaunchPadController pad)
  {
    myLaunchPad = pad;
    onPad = true;
  }
}
