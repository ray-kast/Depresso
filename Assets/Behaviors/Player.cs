using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D)), DisallowMultipleComponent]
public class Player : MonoBehaviour {
  public float speed, pickup, jumpPower;
  public int airJumps;
  public bool controlsEnabled;

  BoxCollider2D box;
  bool doJump, onGround, wasOnGround;
  int jumpsLeft;
  float move;

  void Awake() {
    box = GetComponent<BoxCollider2D>();
    onGround = false;
    wasOnGround = false;
    jumpsLeft = airJumps;
    move = 0.0f;
  }

  void FixedUpdate() {
    {
      var hits = new RaycastHit2D[1];

      var size = box.size;
      size.Scale(transform.localScale);

      const float HEIGHT = 0.1f;

      // Assuming for now that box.offset == 0
      var pos = new Vector2(transform.position.x, transform.position.y - 0.5f * (size.y - HEIGHT));

      onGround = Physics2D.BoxCast(pos, new Vector2(size.x, HEIGHT), 0.0f,
        Vector2.down, new ContactFilter2D {
          layerMask = ~LayerMask.GetMask("Player"),
          useDepth = false,
          useLayerMask = true,
          useNormalAngle = false,
          useTriggers = false,
        }, hits, 0.001f) > 0;

      wasOnGround = onGround;
    }

    {
      var body = GetComponent<Rigidbody2D>();
      var pos = body.position;
      var vel = body.velocity;

      if (doJump) {
        doJump = false;
        vel.y = jumpPower;
      }

      pos.x += move * Time.deltaTime;

      body.position = pos;
      body.velocity = vel;
    }
  }

  void Update() {
    var body = GetComponent<Rigidbody2D>();

    move = Mathf.Lerp(Input.GetAxisRaw("Horizontal") * speed * (controlsEnabled ? 1.0f : 0.0f), move, Mathf.Exp(-Time.deltaTime * pickup));

    if (controlsEnabled && GameManager.Instance.GetAxisDownPos("Jump")) {
      if (onGround || jumpsLeft > 0) doJump = true;

      if (onGround) jumpsLeft = airJumps;
      else if (jumpsLeft > 0) --jumpsLeft;
    }
  }
}
