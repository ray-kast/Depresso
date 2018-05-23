using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorPostFX)), DisallowMultipleComponent]
public class CamController : MonoBehaviour {
  static readonly Dictionary<GameProgress, float[]> palettes = new Dictionary<GameProgress, float[]>() {
    { GameProgress.None, new[]{ 0.0f, 0.05f, 0.15f } },
    { GameProgress.Red, new[]{ 1.0f, 0.05f, 0.15f } },
    { GameProgress.Green, new[]{ 0.85f, 1.0f, 0.0f } },
    { GameProgress.Blue, new[]{ 0.85f, 0.85f, 1.0f } },
  };

  void Awake() {
    Debug.Log(string.Format("Loading factors for progress {0}...", GameManager.Instance.Progress));

    var factors = palettes[GameManager.Instance.Progress];

    var postFX = GetComponent<ColorPostFX>();
    postFX.fac1 = factors[0];
    postFX.fac2 = factors[1];
    postFX.fac3 = factors[2];
  }
}
