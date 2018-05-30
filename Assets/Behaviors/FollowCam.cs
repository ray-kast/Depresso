using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), DisallowMultipleComponent]
public class FollowCam : MonoBehaviour {
  public Transform target;
  public float speed;

  void Awake() {
    if (target != null) {
      Vector3 pos = target.position;
      pos.z = transform.position.z;
      transform.position = pos;
    }
  }

  void Update() {
    var pos = Vector3.Lerp(target.position, transform.position, Mathf.Exp(-Time.deltaTime * speed));

    pos.z = transform.position.z;

    transform.position = pos;
  }
}
