using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
  public GameObject gameManager, player;
  public new GameObject camera;

  void Awake() {
    if (GameManager.Instance == null) Instantiate(gameManager);

    var playerInst = Instantiate(player);
    var cameraInst = Instantiate(camera);
    var follow = cameraInst.GetComponent<FollowCam>();
    follow.target = playerInst.transform;
    follow.enabled = true;

    Debug.Log(GameManager.Instance.ProgressMade ? "Hey, we made progress!" : "No progress was made.");
  }
}
