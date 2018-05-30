using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameProgress : int {
  None = 0,
  Red,
  Green,
  Blue,
  Complete,
}

public class GameManager : MonoBehaviour {
  public static GameManager Instance = null;

  GameProgress progress = GameProgress.None;
  bool progressMade = false;
  readonly Dictionary<string, int> axisSigns = new Dictionary<string, int>();
  readonly Dictionary<string, int> prevAxisSigns = new Dictionary<string, int>();
  IEnumerator switchLevelProc;

  public GameProgress Progress { get { return progress; } }
  public bool ProgressMade {
    get {
      bool ret = progressMade;
      progressMade = false;
      return ret;
    }
  }

  int GetAxisSign(float axis) {
    if (Mathf.Abs(axis) < 1e-5) return 0;
    return (int)Mathf.Sign(axis);
  }

  void Awake() {
    if (Instance == null) Instance = this;
    else if (Instance != this) Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
    axisSigns["Jump"] = GetAxisSign(Input.GetAxisRaw("Jump"));
    axisSigns["Interact"] = GetAxisSign(Input.GetAxisRaw("Interact"));
  }

  void UpdateAxes() {
    // TODO: Hard-coding this is stupid
    prevAxisSigns["Jump"]     = axisSigns["Jump"];
    prevAxisSigns["Interact"] = axisSigns["Interact"];

    axisSigns["Jump"] = GetAxisSign(Input.GetAxisRaw("Jump"));
    axisSigns["Interact"] = GetAxisSign(Input.GetAxisRaw("Interact"));
  }

  void Update() {
    UpdateAxes();
  }

  public bool GetAxisDownPos(string name) {
    int x = axisSigns[name];
    return x > 0 && x > prevAxisSigns[name];
  }

  public bool GetAxisDownNeg(string name) {
    int x = axisSigns[name];
    return x < 0 && x < prevAxisSigns[name];
  }

  IEnumerator SwitchLevelProc(string levelName) {
    IEnumerator e = CamController.MainCamera.GetComponent<ColorPostFX>().FadeOut(0.7f, Color.black);

    while (e.MoveNext()) yield return e.Current;

    AsyncOperation op = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

    bool newCam = false;
    Action a = () => newCam = true;

    if (op == null) Debug.LogError("Failed to load scene.");

    CamController.OnCamSwitch += a;

    while (!newCam) yield return null; // Spin-waiting is dumb but I have no other options

    CamController.OnCamSwitch -= a;

    e = CamController.MainCamera.GetComponent<ColorPostFX>().FadeIn(0.3f, Color.black);

    while (e.MoveNext()) yield return e.Current;

    switchLevelProc = null;
  }

  public bool SwitchLevels(string levelName) {
    if (switchLevelProc != null) {
      Debug.LogWarning("Denying level switch for '" + levelName + "'");
      return false;
    }

    StartCoroutine(switchLevelProc = SwitchLevelProc(levelName));

    return true;
  }

  public void ResetLevel() { SwitchLevels(SceneManager.GetActiveScene().path); }

  public void SetProgress(GameProgress value) {
    if ((int)value > (int)progress) {
      progress = value;
      progressMade = true;
    }
  }
}
