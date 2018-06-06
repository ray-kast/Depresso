using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour {

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

  // Update is called once per frame
  void Update() {
    var body = GetComponent<Rigidbody2D>();

    if (changeDirectionLock > 0) changeDirectionLock--;
    else if (Mathf.Abs(transform.position.x - origin.x - offset) > range) {
      directionRight = !directionRight;
      changeDirectionLock = 3;
    }

    body.velocity = new Vector3(directionRight ? xVelocity : -xVelocity, 0.0f, 0.0f);

  }

  public void Die() {
    // EXPECTING ANIMATION HERE
    // animator.SetTrigger("Die");
    Destroy(gameObject);
  }

  void OnCollisionEnter2D(Collision2D coll) {
    if (coll.gameObject.GetComponent<Player>() != null) {
      Destroy(gameObject);
    }
  }
}
