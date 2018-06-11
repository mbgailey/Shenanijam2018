using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour {

  public static GameSettings Instance;

  private static bool created = false;

  public float GameDuration = 120f;

  void Awake()
  {
    if (Instance == null)
    {
      DontDestroyOnLoad(gameObject);
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }
  }
}
