using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
  public CanvasGroup pauseMenuGroup;
  public CanvasGroup pausePanel;
  public CanvasGroup controlsPanel;
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
    pausePanel.alpha = 1f;
    pausePanel.interactable = true;
    pausePanel.blocksRaycasts = true;
    if (timeManager != null)
    {
      timeManager.StopTime();
    }
    EventSystem.current.SetSelectedGameObject(defaultSelection);
  }

  public void HidePauseMenu()
  {
    pauseMenuActive = false;
    pausePanel.alpha = 0f;
    pausePanel.interactable = false;
    pausePanel.blocksRaycasts = false;
    if (timeManager != null)
    {
      timeManager.StartTime();
    }
  }

  public void ShowControlsPanel()
  {
    pausePanel.alpha = 0f;
    pausePanel.interactable = false;
    pausePanel.blocksRaycasts = false;

    controlsPanel.alpha = 1f;
    controlsPanel.interactable = true;
    controlsPanel.blocksRaycasts = true;

  }

  public void HideControlsPanel()
  {
    pausePanel.alpha = 1f;
    pausePanel.interactable = true;
    pausePanel.blocksRaycasts = true;

    controlsPanel.alpha = 0f;
    controlsPanel.interactable = false;
    controlsPanel.blocksRaycasts = false;

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
