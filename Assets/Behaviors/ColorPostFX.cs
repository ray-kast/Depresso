using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera)), DisallowMultipleComponent, ExecuteInEditMode]
[AddComponentMenu("Color PostFX", -1)]
public class ColorPostFX : MonoBehaviour {
  MaterialFactory matFactory;

  [Range(0.0f, 1.0f)]
  public float fac1, fac2, fac3;

  void OnEnable() {
    matFactory = new MaterialFactory();
  }

  void OnRenderImage(RenderTexture src, RenderTexture dst) {
    var mat = matFactory.Intern("Hidden/PostFX/Color");

    mat.SetFloat("_Fac1", fac1);
    mat.SetFloat("_Fac2", fac2);
    mat.SetFloat("_Fac3", fac3);

    Graphics.Blit(src, dst, mat);
  }
}
