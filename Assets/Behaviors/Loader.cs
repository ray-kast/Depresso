using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
  public GameObject gameManager, player;
  public new GameObject camera;

  Action updateHandler;

  void UpdatePlayer(Player player) {
    Debug.Log(player);
    // Enable double-jump after the red level
    if (GameManager.Instance.Progress >= GameProgress.Red) player.airJumps = 1;
    if (GameManager.Instance.Progress >= GameProgress.Green) player.ShieldEnabled = true;
  }

  void Awake() {
    if (GameManager.Instance == null) Instantiate(gameManager);

    var playerInst = Instantiate(player).GetComponent<Player>();
    var cameraInst = Instantiate(camera).GetComponent<FollowCam>();

    UpdatePlayer(playerInst);

    cameraInst.target = playerInst.transform;
    cameraInst.enabled = true;

    // Because it's a weak event, we have to hold on to a reference.
    GameManager.Instance.ProgressChanged += updateHandler = () => UpdatePlayer(playerInst);

    Debug.Log(GameManager.Instance.ProgressMade ? "Hey, we made progress!" : "No progress was made.");
  }
}
