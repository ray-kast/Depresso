using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

  public float activeTime = 3.0f;
  public float cooldownTime = 5.0f;
  public float ReadyOpacity = 0.1f;

  enum State { Ready, Active, Cd };
  /*
    Life cycle:
    Ready
    || event: spacebar input
    Active
    || event: butterfly trigger, or
    || countdown: activeTime
    Cd
    || countdown: cooldownTime
    Ready
  */
  State state = State.Ready;
  float activeTimeCount = 0.0f;
  float cdTimeCount = 0.0f;
  float opac = 0.0f;

  void OnTriggerEnter2D(Collider2D coll) {
    var butterflyScript = coll.gameObject.GetComponent<Butterfly>();
    if (butterflyScript != null && state == State.Active) {
      butterflyScript.Die();
      state = State.Cd;
    }
  }

  void Awake() {
    state = State.Ready;
  }

  void Update() {
    switch (state) {

      case State.Active:
        opac = 1.0f;
        if (activeTimeCount > activeTime) {
          activeTimeCount = 0.0f;
          state = State.Cd;
        }
        else {
          activeTimeCount += Time.deltaTime;
        }
        break;

      case State.Cd:
        opac = 0.0f;
        if (cdTimeCount > cooldownTime) {
          cdTimeCount = 0.0f;
          state = State.Ready;
        }
        else {
          cdTimeCount += Time.deltaTime;
        }
        break;

      case State.Ready:
        opac = 0.1f;
        break;
    }

    this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, opac);
  }

  public void Activate() {
    if (state == State.Ready)
      state = State.Active;
  }
}
