using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour {
  public Door exitDoor;

  void Awake() {
    exitDoor.BeforeEnter += () => GameManager.Instance.SetProgress(GameProgress.Blue);
  }
}
