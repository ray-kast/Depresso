using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour {
  bool triggered = false;

  void Awake() {
    triggered = false;
  }

  IEnumerator InteractionSequence(Player player) {
    player.controlsEnabled = false;

    yield return new WaitForSeconds(2.0f);

    player.controlsEnabled = true;
    triggered = true;
  }

  void OnInteract(GameObject from) {
    if (triggered) return; // Only do this once

    var player = from.GetComponent<Player>();

    if (player == null) return;

    StartCoroutine(InteractionSequence(player));
  }
}
