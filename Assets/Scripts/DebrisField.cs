using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisField : MonoBehaviour {

  public float rotateSpeed = 4f;
  public float fieldRadiusMin = 1.9f;
  public float fieldRadiusMax = 2.1f;
  public float debrisScaleMin = 0.95f;
  public float debrisScaleMax = 1.05f;
  public float debrisLocalRotationMax = 7f;
  public int debrisCount = 20;
  public GameObject[] debrisPrefabs;

  public bool createOverTime = false;
  float clusterTiming = 10f;
  float timer = 0f;
  int clusterSize = 1;


  // Use this for initialization
  void Start () {
    if(!createOverTime)
      CreateField();

  }
	
	// Update is called once per frame
	void Update () {
    this.transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

    if (createOverTime)
    {
      if (timer >= clusterTiming)
      {
        AddNewDebrisCluster(3, 290f, 300f);
        timer = 0f;
      }

      timer += Time.deltaTime;
    }
    
  }

  void CreateField()
  {
    for (int i = 0; i < debrisCount; i++)
    {
      GameObject deb = Instantiate(debrisPrefabs[Random.Range(0, debrisPrefabs.Length)], this.transform);
      float scale = Random.Range(debrisScaleMin, debrisScaleMax);
      deb.transform.localScale = new Vector3(scale, scale, scale);
      float radius = Random.Range(fieldRadiusMin, fieldRadiusMax);
      float zAngle = Random.Range(0f, 360f);
      float x = Mathf.Cos(zAngle * Mathf.Deg2Rad) * radius;
      float y = Mathf.Sin(zAngle * Mathf.Deg2Rad) * radius;
      //Debug.Log("zAngle: " + zAngle + " x: " + x + " y: " + y);
      Vector2 pos = new Vector2(x, y);
      deb.transform.localPosition = pos;
      deb.GetComponent<DebrisController>().rotateSpeed = Random.Range(-debrisLocalRotationMax, debrisLocalRotationMax);
      deb.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
    }
  }

  void AddNewDebrisCluster(int clusterCount, float minAngle, float maxAngle)
  {
    for (int i = 0; i < clusterCount; i++)
    {
      GameObject deb = Instantiate(debrisPrefabs[Random.Range(0, debrisPrefabs.Length)], this.transform);
      float scale = Random.Range(debrisScaleMin, debrisScaleMax);
      deb.transform.localScale = new Vector3(scale, scale, scale);
      float radius = Random.Range(fieldRadiusMin, fieldRadiusMax);
      float zAngle = Random.Range(minAngle, maxAngle); //absolute angle, not local
      zAngle = zAngle - this.transform.localEulerAngles.z;
      float x = Mathf.Cos(zAngle * Mathf.Deg2Rad) * radius;
      float y = Mathf.Sin(zAngle * Mathf.Deg2Rad) * radius;
      //Debug.Log("zAngle: " + zAngle + " x: " + x + " y: " + y);
      Vector2 pos = new Vector2(x, y);
      deb.transform.localPosition = pos;
      deb.GetComponent<DebrisController>().rotateSpeed = Random.Range(-debrisLocalRotationMax, debrisLocalRotationMax);
      deb.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
    }
  }

  public void SetNewDebrisParameters(int size, float timeBetween)
  {
    clusterSize = size;
    clusterTiming = timeBetween;

  }


}
