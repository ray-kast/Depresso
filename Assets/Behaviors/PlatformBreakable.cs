using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreakable : MonoBehaviour {

  public float breakTime = 1.0f;

  bool aboutToBreak = false;
  float breakTimeCount = 0;

  void Start () {
  }
  
  void Update () {

    if(aboutToBreak) {
      breakTimeCount += Time.deltaTime;
      if (breakTimeCount > breakTime) 
      {
        //Cannon ball bomb explodes
        breakSelf();
        breakTimeCount = 0f;
        aboutToBreak = false;
      }
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    Debug.Log("Platform Collision Enter");    
    var otherName = other.gameObject.name;
    if (otherName.Contains("Player")) {
      Debug.Log("aboutToBreak = true");
      aboutToBreak = true;
    }
  }

  void breakSelf() {
    Debug.Log("breakSelf()");
    Destroy(GetComponent<BoxCollider2D>());
    Destroy(gameObject,5.0f);
  } 
}
