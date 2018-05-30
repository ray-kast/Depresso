using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D other) {
    GameManager.Instance.ResetLevel();
  }

  void OnCollisionEnter2D(Collision2D coll) {
    GameManager.Instance.ResetLevel();
  }
}
