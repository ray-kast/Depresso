using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class Player : MonoBehaviour {
  public float speed, pickup, jumpPower;

  void Update() {
    var body = GetComponent<Rigidbody2D>();

    var vel = body.velocity;

    vel.x = Mathf.Lerp(Input.GetAxisRaw("Horizontal") * speed, vel.x, Mathf.Exp(-Time.deltaTime * pickup));

    if (GameManager.Instance.GetAxisDownPos("Jump")) vel.y = jumpPower;

    body.velocity = vel;
  }
}
