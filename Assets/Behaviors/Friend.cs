using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour {
  void OnInteract(GameObject from) {
    var player = from.GetComponent<Player>();

    if (player == null) return;

    player.controlsEnabled = false;
  }
}
