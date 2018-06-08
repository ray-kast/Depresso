using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForFriend : MonoBehaviour {
  public Friend friend;

  void Awake() {
    gameObject.SetActive(!friend.WouldMakeProgress);

    friend.Trigger += () => gameObject.SetActive(true);
  }
}
