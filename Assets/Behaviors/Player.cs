using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class Player : MonoBehaviour {
	void Update () {
		var body = GetComponent<Rigidbody2D>();

		var vel = body.velocity;

		vel.x = Input.GetAxisRaw("Horizontal") * 5.0f;

    if (Input.GetButtonDown("Jump")) vel.y = 5.0f;

		body.velocity = vel;
	}
}
