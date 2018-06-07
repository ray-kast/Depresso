using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable)), DisallowMultipleComponent]
public class Friend : MonoBehaviour {
  public GameProgress targetProgress;

  bool triggered = false;

  event Action trigger;

  public event Action Trigger {
    add { trigger += value; }
    remove { trigger -= value; }
  }

  void Awake() {
    triggered = false;
  }

  IEnumerator InteractionSequence(Player player) {
    player.controlsEnabled = false;

    yield return new WaitForSeconds(1.0f);

    GameManager.Instance.SetProgress(targetProgress);

    yield return new WaitForSeconds(1.0f);

    player.controlsEnabled = true;
    triggered = true;

    if (trigger != null) trigger();
  }

  void OnInteract(GameObject from) {
    if (triggered) return; // Only do this once

    var player = from.GetComponent<Player>();

    if (player == null) return;

    StartCoroutine(InteractionSequence(player));
  }
}
