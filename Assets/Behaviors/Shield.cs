using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	void Awake() {
		// gameObject.SetActive(false);
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		var butterflyScript = coll.gameObject.GetComponent<Butterfly>();
    if (butterflyScript != null) {		
			butterflyScript.Die();
		}
  }
}
