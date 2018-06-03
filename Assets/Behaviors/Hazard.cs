using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D other) {
    var player = other.GetComponent<Player>();

    if (player != null) {
      GameManager.Instance.ResetLevel();
      player.controlsEnabled = false;
    }
  }

  void OnCollisionEnter2D(Collision2D coll) {
    var player = coll.gameObject.GetComponent<Player>();

    if (player != null) {
      GameManager.Instance.ResetLevel();
      player.controlsEnabled = false;
    }
  }
}
