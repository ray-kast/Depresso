using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
  public string levelName;
  public bool autoActivate;
  bool colliding = false;

  event Action beforeEnter;

  public event Action BeforeEnter {
    add { beforeEnter += value; }
    remove { beforeEnter -= value; }
  }

  void Activate() {
    if (beforeEnter != null) beforeEnter();

    GameManager.Instance.SwitchLevels(levelName);
  }

  void Update() {
    if (!colliding || autoActivate) return;

    if (GameManager.Instance.GetAxisDownPos("Interact")) Activate();
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (!enabled || other.GetComponent<Player>() == null) return;

    if (autoActivate) Activate();
    else {
      colliding = true;
      return;
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (!enabled || other.GetComponent<Player>() == null) return;

    if (!autoActivate) colliding = false;
  }
}
