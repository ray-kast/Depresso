using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class Platform : MonoBehaviour {

  public float range = 3.0f;
  public float xVelocity = 2.0f;
  public float offset = 0.0f;

  Vector3 origin = new Vector3(100.0f, 0.0f, 0.0f);

  bool directionRight = true;
  int changeDirectionLock = 0;

  void Start() {
    origin = transform.position;

    var body = GetComponent<Rigidbody2D>();
    body.velocity = new Vector3(xVelocity, 0.0f, 0.0f);
  }

  void Update() {
    var body = GetComponent<Rigidbody2D>();

    if (changeDirectionLock > 0) changeDirectionLock--;
    else if (Mathf.Abs(transform.position.x - origin.x - offset) > range) {
      directionRight = !directionRight;
      changeDirectionLock = 3;
    }

    body.velocity = new Vector3(directionRight ? xVelocity : -xVelocity, 0.0f, 0.0f);
  }
}