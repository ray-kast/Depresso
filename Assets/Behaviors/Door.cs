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

  void Activate() {
    if (beforeEnter != null) beforeEnter();

    GameManager.Instance.SwitchLevels(levelName);
  }

  void OnInteract(GameObject from) {
    if (!enabled) return;

    Activate();
  }
}
