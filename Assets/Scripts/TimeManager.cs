using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{

  float slowMoTimeScale = 0.5f;

  // Use this for initialization
  void Start()
  {

  }

  public void SlowMotion(float duration)
  {
    Sequence slowSequence = DOTween.Sequence();
    slowSequence.Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, slowMoTimeScale, 0.2f).SetEase(Ease.InQuad).SetUpdate(true))
        .AppendInterval(duration)
        .Append(DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f, 0.2f).SetEase(Ease.InQuad).SetUpdate(true));

    slowSequence.Play();
  }

  public void StopTime()
  {
    Time.timeScale = 0f;
    Debug.Log("PAUSE");
  }

  public void StartTime()
  {
    Time.timeScale = 1f;
    Debug.Log("UNPAUSE");
  }
}
