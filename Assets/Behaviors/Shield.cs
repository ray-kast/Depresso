using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D coll) {
    var butterflyScript = coll.gameObject.GetComponent<Butterfly>();
    if (butterflyScript != null) {
      butterflyScript.Die();
    }
  }
}
