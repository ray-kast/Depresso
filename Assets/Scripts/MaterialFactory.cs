using System;
using System.Collections.Generic;
using UnityEngine;

using UnityObject = UnityEngine.Object;

public class MaterialFactory : IDisposable {
  Dictionary<string, Material> materials = new Dictionary<string, Material>();

  public Material Intern(string name) {
    Material ret;

    if (!materials.TryGetValue(name, out ret)) {
      var shd = Shader.Find(name);

      if (shd == null) throw new ArgumentException(string.Format("Bad shader name '{0}'.", name), "name");

      ret = new Material(shd) {
        name = name,
        hideFlags = HideFlags.DontSave,
      };

      materials.Add(name, ret);
    }

    return ret;
  }

  public void Dispose() {
    foreach (var mat in materials) {
#if UNITY_EDITOR
      if (Application.isPlaying) UnityObject.Destroy(mat.Value);
      else UnityObject.DestroyImmediate(mat.Value);
#else
      UnityObject.Destroy(mat.Value);
#endif
    }
  }
}