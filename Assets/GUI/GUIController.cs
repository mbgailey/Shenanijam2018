using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GUIController : MonoBehaviour {

  public Text countDownText;
  public Text scoreText;
  public Text endText1;
  public Text endText2;
  public Text endText3;
  public Text endText4;
  public Text endText5;
  public CanvasGroup countdownGroup;
  public CanvasGroup scoreGroup;
  public CanvasGroup blackScreen;
  TimeManager timeManager;

  // Use this for initialization
  void Start () {
    timeManager =  FindObjectOfType<TimeManager>();
    timeManager.StopTime();
    blackScreen.alpha = 1f;
    StartCoroutine(FadeIntoGame());
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  IEnumerator FadeIntoGame()
  {
    yield return new WaitForSeconds(1f);
    blackScreen.DOFade(0f, 3f);
    yield return new WaitForSeconds(2f);
    timeManager.StartTime();
  }

  public void UpdateCountdown(float timeLeft)
  {

    countDownText.text = Mathf.Ceil(timeLeft).ToString("F0");
  }

  public void UpdateScore(int newScore)
  {
    //string postStr = " Survived";

    scoreText.text = newScore.ToString();
  }

  public IEnumerator DisplayGameOverText(int finalScore, int deathCount)
  {
    countdownGroup.DOFade(0f, 2f);
    scoreGroup.DOFade(0f, 2f);

    Color endColor = endText1.color;
    endColor.a = 1f;

    string line1 = "Earth was destroyed\n";
    string line2 = finalScore.ToString() + " ships escaped\n";
    string line3 = deathCount.ToString() + " ships perished\n";
    string line4 = "Your saved/death ratio was: " + ((float)finalScore / (float)deathCount).ToString("F2") + "\n";
    string line5 = "Hit <Enter> to Restart";

    endText1.text = line1;
    endText1.DOColor(endColor, 2f);
    yield return new WaitForSeconds(2f);

    endText2.text = line2;
    endText2.DOColor(endColor, 2f);
    yield return new WaitForSeconds(1f);

    endText3.text = line3;
    endText3.DOColor(endColor, 2f);
    yield return new WaitForSeconds(1f);

    endText4.text = line4;
    endText4.DOColor(endColor, 2f);
    yield return new WaitForSeconds(1f);

    endText5.text = line5;
    endText5.DOColor(endColor, 2f);
    yield return new WaitForSeconds(2f);

  }
}
