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

  public Color gray;

  float fade;
  Color fadeTo;
  IEnumerator fadeProc;

  void OnEnable() {
    matFactory = new MaterialFactory();
  }

  void OnRenderImage(RenderTexture src, RenderTexture dst) {
    var mat = matFactory.Intern("Hidden/PostFX/Color");

    mat.SetFloat("_Fac1", fac1);
    mat.SetFloat("_Fac2", fac2);
    mat.SetFloat("_Fac3", fac3);
    mat.SetColor("_Gray", gray);
    mat.SetFloat("_Fade", fade);
    mat.SetColor("_FadeTo", fadeTo);

    Graphics.Blit(src, dst, mat);
  }

  IEnumerator FadeInProc(float len) {
    float t = 1.0f, speed = 1.0f / len;

    var w = new WaitForEndOfFrame();

    while (t >= 0.0f) {
      t -= Time.deltaTime * speed;

      fade = Mathf.Max(t, 0.0f);

      yield return w;
    }

    fadeProc = null;
  }

  IEnumerator FadeOutProc(float len) {
    float t = 0.0f, speed = 1.0f / len;

    var w = new WaitForEndOfFrame();

    while (t <= 1.0f) {
      t += Time.deltaTime * speed;

      fade = Mathf.Min(t, 1.0f);

      yield return w;
    }

    fadeProc = null;
  }

  public IEnumerator FadeIn(float len, Color color) {
    if (fadeProc != null) return null;

    fadeTo = color;

    return fadeProc = FadeInProc(len);
  }

  public IEnumerator FadeOut(float len, Color color) {
    if (fadeProc != null) return null;

    fadeTo = color;

    return fadeProc = FadeOutProc(len);
  }
}
