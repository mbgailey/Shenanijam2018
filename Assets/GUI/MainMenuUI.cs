using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
