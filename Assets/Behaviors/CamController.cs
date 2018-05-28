using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorPostFX)), DisallowMultipleComponent]
public class CamController : MonoBehaviour {
  static readonly Dictionary<GameProgress, float[]> palettes = new Dictionary<GameProgress, float[]>() {
    { GameProgress.None,     new[]{ 0.00f, 0.00f, 0.00f } },
    { GameProgress.Red,      new[]{ 1.00f, 0.00f, 0.00f } },
    { GameProgress.Green,    new[]{ 0.85f, 1.00f, 0.00f } },
    { GameProgress.Blue,     new[]{ 0.50f, 0.75f, 0.85f } },
    { GameProgress.Complete, new[]{ 1.00f, 0.85f, 0.75f } },
  };
  static readonly Dictionary<GameProgress, Color> grays = new Dictionary<GameProgress, Color>() {
    { GameProgress.None,     new Color(0.75f, 0.85f, 1.00f) },
    { GameProgress.Red,      new Color(0.75f, 0.85f, 1.00f) },
    { GameProgress.Green,    new Color(0.75f, 0.85f, 1.00f) },
    { GameProgress.Blue,     new Color(0.00f, 0.50f, 1.00f) },
    { GameProgress.Complete, new Color(1.00f, 0.95f, 0.55f) },
  };

  void Awake() {
    Debug.Log(string.Format("Loading factors for progress {0}...", GameManager.Instance.Progress));

    var factors = palettes[GameManager.Instance.Progress];

    var postFX = GetComponent<ColorPostFX>();
    postFX.fac1 = factors[0];
    postFX.fac2 = factors[1];
    postFX.fac3 = factors[2];
    postFX.gray = grays[GameManager.Instance.Progress];
  }
}
