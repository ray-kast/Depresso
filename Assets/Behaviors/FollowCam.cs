using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), DisallowMultipleComponent]
public class FollowCam : MonoBehaviour {
	public Transform target;
	public float speed;
	Camera cam;

	void Awake() {
		cam = GetComponent<Camera>();
	}

	void Update() {
		var pos = Vector3.Lerp(target.position, cam.transform.position, Mathf.Exp(-Time.deltaTime * speed));

		pos.z = cam.transform.position.z;

		cam.transform.position = pos;
	}
}
