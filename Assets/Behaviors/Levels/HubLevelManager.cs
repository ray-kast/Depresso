using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubLevelManager : MonoBehaviour {
  public Door redDoor, greenDoor, blueDoor, completeDoor;

  void Awake() {
    switch (GameManager.Instance.Progress) {
      case GameProgress.Blue: break;
      case GameProgress.Green: // TODO: Do more to disable the last level
        completeDoor.enabled = false;
        break;
      case GameProgress.Red:
        blueDoor.enabled = false;
        goto case GameProgress.Green;
      case GameProgress.None:
        greenDoor.enabled = false;
        goto case GameProgress.Red;
    }

    completeDoor.BeforeEnter += () => GameManager.Instance.SetProgress(GameProgress.Complete);
  }
}
