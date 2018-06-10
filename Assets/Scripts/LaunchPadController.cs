using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadController : MonoBehaviour {

  public float launchPadPrepTime;
  public GameObject rocketPrefab;
  public Transform spawnPosition;
  bool launchPadReady = false;
  GameObject rocket;
  public Vector2 padOrientation = new Vector2();

  GameManager gameManager;

  // Use this for initialization
  void Start () {
    gameManager = FindObjectOfType<GameManager>();

    StartCoroutine(PrepareLaunchPad());
	}
	
	// Update is called once per frame
	void Update () {

    padOrientation = this.transform.position - this.transform.parent.transform.position;

    

    if (Input.GetButton("Launch") && launchPadReady)
    {
      LaunchRocket();
    }
	}


  public IEnumerator PrepareLaunchPad()
  {
    yield return new WaitForSeconds(launchPadPrepTime);
    rocket = null;
    rocket = Instantiate(rocketPrefab, spawnPosition.position, Quaternion.Euler(0f,0f,0f));
    rocket.transform.SetParent(this.transform.parent.transform);
    rocket.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    rocket.GetComponent<RocketController>().SetLaunchPad(this);
    launchPadReady = true;
  }

  public void LaunchRocket()
  {
    if (rocket == null)
      return;

    launchPadReady = false;
    gameManager.ResetTimer();
    StartCoroutine(rocket.GetComponent<RocketController>().LaunchRoutine());
    
  }

}
