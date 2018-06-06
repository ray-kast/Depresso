using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Interactable : MonoBehaviour {
  public void Interact(GameObject from) {
    foreach (var comp in GetComponents<Component>()) {
      var type = comp.GetType();

      var method = type.GetMethod("OnInteract", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

      if (method != null) method.Invoke(comp, new[] { from });
    }
  }
}
