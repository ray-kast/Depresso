using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
  public Friend friend;
  public Door exit;

  void Awake() {
    exit.enabled = false;

    friend.Trigger += () => exit.enabled = true;
  }
}
