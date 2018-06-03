using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
  public GameObject gameManager, player;
  public new GameObject camera;

  void Awake() {
    if (GameManager.Instance == null) Instantiate(gameManager);

    var playerInst = Instantiate(player).GetComponent<Player>();
    var cameraInst = Instantiate(camera).GetComponent<FollowCam>();

    // Enable double-jump after the red level
    if (GameManager.Instance.Progress >= GameProgress.Red) playerInst.airJumps = 1;

    cameraInst.target = playerInst.transform;
    cameraInst.enabled = true;

    Debug.Log(GameManager.Instance.ProgressMade ? "Hey, we made progress!" : "No progress was made.");
  }
}
