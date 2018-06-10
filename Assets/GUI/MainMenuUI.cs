using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Time.timeScale = 1f; //Needed in case main menu is started from the pause menu
	}
	
	public void StartGame()
  {
    SceneManager.LoadScene(1);
  }

  public void QuitApplication()
  {
    Application.Quit();
  }
}
