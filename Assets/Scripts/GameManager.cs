using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  
  public int escapeCount = 0;
  public int launchInterval = 5;
  int rocketsDestroyed = 0;


  float launchTimer;

  GameBounds gameBounds;
  GUIController GUIController;
  LaunchPadController[] launchPads;
  PlanetController planetController;

  public AudioSource audioManagerSource;
  public AudioClip rocketEscapedClip;

  // Use this for initialization
  void Start () {
    GUIController = FindObjectOfType<GUIController>();
    launchPads = FindObjectsOfType<LaunchPadController>();
    planetController = FindObjectOfType<PlanetController>();
    
  }
	
	// Update is called once per frame
	void Update () {
    RunLaunchTimer();

  }

  void RunLaunchTimer()
  {
    launchTimer -= Time.deltaTime;
    //Debug.Log(launchTimer);
    if(launchTimer <= 0f)
    {
      LaunchAllReadyRockets();
      ResetTimer();
    }

    GUIController.UpdateCountdown(launchTimer);
  }

  public void ResetTimer()
  {
    launchTimer = launchInterval;
  }

  void LaunchAllReadyRockets()
  {
    foreach(LaunchPadController pad in launchPads)
    {
      pad.LaunchRocket();
    }
  }

  public void RocketEscaped()
  {
    audioManagerSource.PlayOneShot(rocketEscapedClip);

    escapeCount++;
    GUIController.UpdateScore(escapeCount);
    Debug.Log("Another one escaped");
  }

  public IEnumerator EndGameSequence()
  {
    planetController.DestroyPlanet();

    yield return new WaitForSeconds(1f);

    GUIController.DisplayGameOverText(escapeCount);
  }

  public void RocketDestroyed()
  {
    rocketsDestroyed++;
  }
}
