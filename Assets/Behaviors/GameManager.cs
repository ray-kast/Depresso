using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameProgress : int {
  None = 0,
  Red,
  Green,
  Blue,
}

public class GameManager : MonoBehaviour {
  public static GameManager Instance = null;

  GameProgress progress = GameProgress.None;

  public GameProgress Progress { get { return progress; } }

  void Awake() {
    if (Instance == null) Instance = this;
    else if (Instance != this) Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
  }

  public void SwitchLevels(string levelName) {
    Debug.Log(string.Format("Loading level '{0}'...", levelName));

    var op = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

    if (op == null) Debug.LogError("Failed to load scene.");
    else op.completed += o => Debug.Log("Scene loaded.");
  }

  public void SetProgress(GameProgress value) {
    progress = (GameProgress)Mathf.Max((int)progress, (int)value);
  }
}
