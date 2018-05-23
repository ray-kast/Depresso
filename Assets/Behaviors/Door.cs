using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
  public string levelName;

  event Action beforeEnter;

  public event Action BeforeEnter {
    add { beforeEnter += value; }
    remove { beforeEnter -= value; }
  }

  void OnTriggerEnter2D(Collider2D other) {
    var player = other.GetComponent<Player>();

    if (beforeEnter != null) beforeEnter();

    if (player != null) {
      GameManager.Instance.SwitchLevels(levelName);
    }
  }
}
