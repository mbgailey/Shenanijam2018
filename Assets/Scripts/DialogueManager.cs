using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

  public string[] blackHoleOptions;
  public string[] launchOptions;
  public string[] escapeOptions;
  public string[] explodeOptions;



  // Use this for initialization
  void Start () {
		
	}
	

	public string GetExplodeText ()
  {
    string str;
    str = explodeOptions[Random.Range(0, explodeOptions.Length)];
    return str;
  }

  public string GetBlackholeText()
  {
    string str;
    str = blackHoleOptions[Random.Range(0, blackHoleOptions.Length)];
    return str;
  }

  public string GetEscapeText()
  {
    string str;
    str = escapeOptions[Random.Range(0, escapeOptions.Length)];
    return str;
  }

  public string GetLaunchText()
  {
    string str;
    str = launchOptions[Random.Range(0, launchOptions.Length)];
    return str;
  }
}
