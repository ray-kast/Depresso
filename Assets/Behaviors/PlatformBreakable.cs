﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreakable : MonoBehaviour {

  public float breakTime = 1.0f;

  bool aboutToBreak = false;
  float breakTimeCount = 0;

  void Update() {
    if (aboutToBreak) {
      breakTimeCount += Time.deltaTime;
      if (breakTimeCount > breakTime) {
        breakSelf();
        breakTimeCount = 0f;
        aboutToBreak = false;
      }
    }
  }

  void OnCollisionEnter2D(Collision2D coll) {
    if (coll.gameObject.GetComponent<Player>() != null) aboutToBreak = true;
  }

  void breakSelf() {
    // STUB FOR ANIMATION
    // GetComponent<Animator>().SetTrigger("break");

    // DON'T KNOW WHY THIS LINE DOESN'T WORK
    // GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, -6.0f));
    Destroy(GetComponent<BoxCollider2D>());
    Destroy(gameObject, 5.0f);
  }
}
