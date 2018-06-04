using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreakable : MonoBehaviour {

  public float breakTime = 1.0f;

  bool breaking = false;
  float breakTimeCount = 0;

  void Update() {
    if (breaking) {
      breakTimeCount += Time.deltaTime;

      if (breakTimeCount > breakTime) {
        breaking = false;

        Destroy(gameObject); // TODO: Spawn some kind of "broken platform"
      }
    }
  }

  void OnCollisionEnter2D(Collision2D coll) {
    if (!breaking && coll.gameObject.GetComponent<Player>() != null) breaking = true;
  }
}
