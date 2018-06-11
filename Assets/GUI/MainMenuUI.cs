using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour {

  GameSettings gameSettings;

  public CanvasGroup controlsPanel;
  public CanvasGroup creditsPanel;
  public CanvasGroup fadePanel;
  public GameObject defaultControlsSel;
  public GameObject defaultCreditsSel;
  public CanvasGroup buttonPanel;
  bool coverPanelActive = false;

  // Use this for initialization
  void Start () {
    BackToMenu();
    Time.timeScale = 1f; //Needed in case main menu is started from the pause menu
    gameSettings = FindObjectOfType<GameSettings>();
	}

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (coverPanelActive)
      {
        BackToMenu();
      }
    }
  }

  public void StartShortGame()
  {
    gameSettings.GameDuration = 120f;
    SceneManager.LoadScene(1);
  }

  public void StartLongGame()
  {
    gameSettings.GameDuration = 300f;
    SceneManager.LoadScene(1);
  }

  public void QuitApplication()
  {
    Application.Quit();
  }

  public void ShowControlsPanel()
  {
    coverPanelActive = true;
    buttonPanel.interactable = false;
    fadePanel.alpha = 1f;
   
    controlsPanel.alpha = 1f;
    controlsPanel.interactable = true;
    controlsPanel.blocksRaycasts = true;
    EventSystem.current.SetSelectedGameObject(defaultControlsSel);
  }

  public void ShowCreditsPanel()
  {
    coverPanelActive = true;
    buttonPanel.interactable = false;
    fadePanel.alpha = 1f;

    creditsPanel.alpha = 1f;
    creditsPanel.interactable = true;
    creditsPanel.blocksRaycasts = true;
    EventSystem.current.SetSelectedGameObject(defaultCreditsSel);
  }

  public void BackToMenu()
  {
    fadePanel.alpha = 0f;

    controlsPanel.alpha = 0f;
    controlsPanel.interactable = false;
    controlsPanel.blocksRaycasts = false;

    creditsPanel.alpha = 0f;
    creditsPanel.interactable = false;
    creditsPanel.blocksRaycasts = false;
    buttonPanel.interactable = true;
    coverPanelActive = false;
  }


}
