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
  Dictionary<string, int> axisSigns = new Dictionary<string, int>(),
    prevAxisSigns = new Dictionary<string, int>();

  public GameProgress Progress { get { return progress; } }
  public bool ProgressMade {
    get {
      var ret = progressMade;
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
    var x = axisSigns[name];
    return x > 0 && x > prevAxisSigns[name];
  }

  public bool GetAxisDownNeg(string name) {
    var x = axisSigns[name];
    return x < 0 && x < prevAxisSigns[name];
  }

  public void SwitchLevels(string levelName) {
    Debug.Log(string.Format("Loading level '{0}'...", levelName));

    var op = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

    if (op == null) Debug.LogError("Failed to load scene.");
    else op.completed += o => Debug.Log("Scene loaded.");
  }

  public void ResetLevel() { SwitchLevels(SceneManager.GetActiveScene().path); }

  public void SetProgress(GameProgress value) {
    if ((int)value > (int)progress) {
      Debug.Log(string.Format("Set progress to {0}", value));
      progress = value;
      progressMade = true;
    }
  }
}
