﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  
  public int escapeCount = 0;
  public int rocketsDestroyed = 0;
  public int launchInterval = 5;
  public float gameDurationSec = 120f;
  public DebrisField mainDebrisField;

  public int gamePhase = 0;
  float timeToNextPhase = 20f;
  float gameTimer = 0f;

  float launchTimer;
  public bool gameOver = false;
  bool readyToRestartLevel = false;

  GameBounds gameBounds;
  GUIController GUIController;
  LaunchPadController[] launchPads;
  PlanetController planetController;
  BlackholeController[] blackHoles;
  GameSettings gameSettings;

  int activePad = 1;

  public AudioSource audioManagerSource;
  public AudioClip rocketEscapedClip;

  // Use this for initialization
  void Start () {
    GameSettings gameSettings = FindObjectOfType<GameSettings>();
    GUIController = FindObjectOfType<GUIController>();
    launchPads = FindObjectsOfType<LaunchPadController>();
    planetController = FindObjectOfType<PlanetController>();
    blackHoles = FindObjectsOfType<BlackholeController>();
    gameDurationSec = GameSettings.Instance.GameDuration;
    Debug.Log("Game duration set to " + gameDurationSec);
    SetBlackHoleGrowSpeed();
    timeToNextPhase = gameDurationSec / 15f; //Phases will be 20s for long (300s) game. 8s for short (120s) game;
    UpdateGameDifficulty();
  }
	
	// Update is called once per frame
	void Update () {

    if (!gameOver)
    {
      RunLaunchTimer();
      RunGameTimer();
    }
    

    if(readyToRestartLevel && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
    {
      RestartLevel();
    }

  }

  void RunLaunchTimer()
  {
    launchTimer -= Time.deltaTime;
    //Debug.Log(launchTimer);
    if(launchTimer <= 0f)
    {
      LaunchNextReadyRocket();
      ResetTimer();
    }

    GUIController.UpdateCountdown(launchTimer);
  }

  void RunGameTimer()
  {
    gameTimer += Time.deltaTime;
    if(gameTimer >= timeToNextPhase)
    {
      gamePhase++;
      UpdateGameDifficulty();
      gameTimer = 0f;
    }

  }

  void UpdateGameDifficulty()
  {
    switch (gamePhase)
    {
      case 0:
        mainDebrisField.SetNewDebrisParameters(0, 20f);
        break;
      case 1:
        mainDebrisField.SetNewDebrisParameters(1, 15f);
        break;
      case 2:
        mainDebrisField.SetNewDebrisParameters(2, 15f);
        break;
      case 3:
        mainDebrisField.SetNewDebrisParameters(3, 10f);
        break;
      case 4:
        mainDebrisField.SetNewDebrisParameters(4, 10f);
        break;
      case 5:
        mainDebrisField.SetNewDebrisParameters(3, 5f);
        break;
      case 6:
        mainDebrisField.SetNewDebrisParameters(4, 5f);
        break;
      default:
        mainDebrisField.SetNewDebrisParameters(4, 3f);
        break;


    }
  }

  public void ResetTimer()
  {
    launchTimer = launchInterval;
  }

  void LaunchNextReadyRocket()
  {
    activePad++;
    if (activePad>= launchPads.Length)
    {
      activePad = 0;
    }

    launchPads[activePad].LaunchRocket();
  }

  void LaunchAllReadyRockets()
  {
    foreach(LaunchPadController pad in launchPads)
    {
      pad.LaunchRocket();
    }
  }

  void SetBlackHoleGrowSpeed()
  {
    foreach (BlackholeController bh in blackHoles)
    {
      bh.growSpeed = 5 / gameDurationSec;
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
    gameOver = true;

    StartCoroutine(planetController.DestroyPlanet());

    yield return new WaitForSeconds(5f);

    StartCoroutine(GUIController.DisplayGameOverText(escapeCount, rocketsDestroyed));

    yield return new WaitForSeconds(7.5f);

    readyToRestartLevel = true;
  }

  public void RocketDestroyed()
  {
    rocketsDestroyed++;
  }

  public void RestartLevel()
  {
    SceneManager.LoadScene(1);
  }
}
