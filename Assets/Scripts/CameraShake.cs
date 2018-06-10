using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

  float startingShakeDistance;
  float decreasePercentage;
  float shakeSpeed;
  int numberOfShakes;
  float maxShakeAngle;
  Camera mainCam;
  Vector3 originalPosition;

  // Use this for initialization
  void Start()
  {
    startingShakeDistance = 0.5f;
    decreasePercentage = 0.70f;
    shakeSpeed = 50f;
    numberOfShakes = 8;
    maxShakeAngle = 30.0f;
    mainCam = Camera.main;
    originalPosition = mainCam.transform.position;
  }

  void Update()
  {
    //bool space = Input.GetKeyDown(KeyCode.Space);

    //if (space)
    //{
    //  StartCoroutine(Shake());
    //}
  }

  public IEnumerator Shake()
  {

    float shakeDist = mainCam.orthographicSize / 40f * startingShakeDistance;   //Scale shake distance by camera zoom because small shakes don't look like much when zoomed out
    float hitTime = Time.time;
    //originalPosition = mainCam.transform.position;
    int shake = numberOfShakes;
    float shakeAngle = Random.Range(-maxShakeAngle, maxShakeAngle);
    Vector2 shakeDistance = new Vector2(shakeDist * Mathf.Sin(shakeAngle), shakeDist * Mathf.Cos(shakeAngle));

    while (shake > 0)
    {
      float timer = (Time.time - hitTime) * shakeSpeed;
      Vector3 temp = new Vector3(originalPosition.x + Mathf.Sin(timer) * shakeDistance.x, originalPosition.y + Mathf.Sin(timer) * shakeDistance.y, originalPosition.z);
      this.transform.position = temp;

      if (timer > Mathf.PI * 2f)
      {
        hitTime = Time.time;
        shakeDistance *= decreasePercentage;
        shake--;
      }
      yield return null;

    }
    this.transform.position = originalPosition;
  }
}
