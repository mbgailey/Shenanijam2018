using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

  DialogueManager dialogueManager;
  public Text myText;

	// Use this for initialization
	void Awake () {
    dialogueManager = FindObjectOfType<DialogueManager>();
    //myText = GetComponent<Text>();
  }

  public void InitializeText_explode()
  {
    string str = dialogueManager.GetExplodeText();
    myText.text = str;
  }

  public void InitializeText_blackhole()
  {
    string str = dialogueManager.GetBlackholeText();
    myText.text = str;
  }

  public void InitializeText_escape()
  {
    string str = dialogueManager.GetEscapeText();
    myText.text = str;
  }

  public void InitializeText_launch()
  {
    string str = dialogueManager.GetLaunchText();
    if (Random.Range(0f, 1f) > 0.25f) //Sometimes, don't show any text
    {
      str = "";
    }

    myText.text = str;
  }

  public void SetPath (Vector2 endPt) {

    if (Random.Range(0f, 1f) > 0.5f) //Sometimes, don't show any text
    {
      Destroy(this.gameObject);
    }
    else
    {
      //this.transform.DOMove(endPt, 4f).SetEase(Ease.InQuart);
      myText.DOFade(0f, 3f).SetEase(Ease.InQuart);

      Destroy(this.gameObject, 3f);
    }

	}

  
}
