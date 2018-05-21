using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class Player : MonoBehaviour {
	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update () {
		var body = GetComponent<Rigidbody2D>();

		var vel = body.velocity;

		vel.x = Input.GetAxis("Horizontal") * 5.0f;

		if (Input.GetAxis("Jump")) vel.y = 1.0f;

		body.velocity = vel;
	}
}
