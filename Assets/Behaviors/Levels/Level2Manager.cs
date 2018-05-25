using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour {
  public Door exitDoor;

  void Awake() {
    Debug.Log("Hello from Level 2!");

    exitDoor.BeforeEnter += () => GameManager.Instance.SetProgress(GameProgress.Green);
  }
}
