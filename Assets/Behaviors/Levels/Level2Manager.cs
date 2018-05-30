using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour {
  public Door exitDoor;

  void Awake() {
    exitDoor.BeforeEnter += () => GameManager.Instance.SetProgress(GameProgress.Green);
  }
}
