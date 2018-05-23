using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubLevelManager : MonoBehaviour {
  public Door redDoor, greenDoor, blueDoor;

  void Awake() {
    Debug.Log("Hello from the hub!");

    switch (GameManager.Instance.Progress) {
      case GameProgress.None: break;
      case GameProgress.Red:
        redDoor.gameObject.SetActive(false);
        break;
      case GameProgress.Green:
        greenDoor.gameObject.SetActive(false);
        goto case GameProgress.Red;
      case GameProgress.Blue:
        blueDoor.gameObject.SetActive(false);
        goto case GameProgress.Green;
    }
  }
}
