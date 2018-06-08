using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable), typeof(AudioSource)), DisallowMultipleComponent]
public class Friend : MonoBehaviour {
  public GameProgress targetProgress;

  bool triggered = false;

  public bool WouldMakeProgress {
    get {
      if (GameManager.Instance == null) return true;
      return GameManager.Instance.Progress < targetProgress;
    }
  }

  event Action trigger;

  public event Action Trigger {
    add { trigger += value; }
    remove { trigger -= value; }
  }

  void Awake() {
    triggered = !WouldMakeProgress;
  }

  IEnumerator InteractionSequence(Player player) {
    player.controlsEnabled = false;

    yield return new WaitForSeconds(1.0f);

    GameManager.Instance.SetProgress(targetProgress);
    GetComponent<AudioSource>().Play();

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
