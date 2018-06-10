using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GUIController : MonoBehaviour {

  public Text countDownText;
  public Text scoreText;
  public Text endText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void UpdateCountdown(float timeLeft)
  {

    countDownText.text = Mathf.Ceil(timeLeft).ToString("F0");
  }

  public void UpdateScore(int newScore)
  {
    string postStr = " Survived";

    scoreText.text = newScore.ToString() + postStr;
  }

  public void DisplayGameOverText(int finalScore)
  {
    endText.text = "Earth was destroyed\n" + finalScore.ToString() + " ships escaped";
    Color endColor = endText.color;
    endColor.a = 1f;

    endText.DOColor(endColor, 2f);
  }
}
