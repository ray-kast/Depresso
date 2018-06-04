using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), DisallowMultipleComponent]
public class FollowCam : MonoBehaviour {
  public Transform target;
  public float speed, camSize, dynamicZoom, zoomAttack, zoomDecay;

  Camera cam;
  Vector3 lastPos;

  void Awake() {
    cam = GetComponent<Camera>();
    cam.orthographicSize = camSize;

    if (target != null) {
      Vector3 pos = target.position;
      pos.z = transform.position.z;
      transform.position = pos;
      lastPos = pos;
    }
  }

  void FixedUpdate() {
    var tgtPos = target.position;

    var vel = (tgtPos - lastPos) / Time.fixedDeltaTime;

    var tgt = camSize * (1 + vel.magnitude * dynamicZoom);

    cam.orthographicSize = Mathf.Lerp(tgt, cam.orthographicSize, Mathf.Exp(-Time.fixedDeltaTime * (cam.orthographicSize < tgt ? zoomAttack : zoomDecay)));

    lastPos = tgtPos;

    var pos = Vector3.Lerp(target.position, transform.position, Mathf.Exp(-Time.deltaTime * speed));

    pos.z = transform.position.z;

    transform.position = pos;
  }
}
