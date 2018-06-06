using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
  public CanvasGroup pauseMenuGroup;
  public bool showOnStart = false;
  public GameObject defaultSelection;
  bool pauseMenuActive = false;
  TimeManager timeManager;

  private void Start()
  {
    timeManager = FindObjectOfType<TimeManager>();
    if (showOnStart)
    {
      ShowPauseMenu();
    }
    else
    {
      HidePauseMenu();
    }
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      TogglePauseMenu();
    }
  }

  public void TogglePauseMenu()
  {
    if (pauseMenuActive)
    {
      HidePauseMenu();
    }
    else
    {
      ShowPauseMenu();
    }
  }

  public void ShowPauseMenu()
  {
    pauseMenuActive = true;
    pauseMenuGroup.alpha = 1f;
    if(timeManager != null)
    {
      timeManager.StopTime();
    }
    EventSystem.current.SetSelectedGameObject(defaultSelection);
  }

  public void HidePauseMenu()
  {
    pauseMenuActive = false;
    pauseMenuGroup.alpha = 0f;
    if (timeManager != null)
    {
      timeManager.StartTime();
    }
  }


  public void GoToMainMenu()
  {
    SceneManager.LoadScene(0);
  }

  public void QuitApplication()
  {
    Application.Quit();
  }
}
