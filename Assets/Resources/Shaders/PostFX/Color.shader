Shader "Hidden/PostFX/Color" {
  Properties {
    _MainTex("Texture", 2D) = "white" {}
    _Fac1("Factor R", Float) = 0.0
    _Fac2("Factor G", Float) = 0.0
    _Fac3("Factor B", Float) = 0.0
    _Gray("Gray Color", Color) = (1.0, 1.0, 1.0, 1.0)
    _Fade("Fade Amount", Float) = 0.0
    _FadeTo("Fade Color", Color) = (0.0, 0.0, 0.0, 1.0)
  }

  CGINCLUDE

  #pragma target 3.0

  float _Fac1, _Fac2, _Fac3, _Fade;
  float4 _Gray, _FadeTo;

  ENDCG

  SubShader {
    Cull Off ZWrite Off ZTest Always

    Pass {
      CGPROGRAM

      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct appdata {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
      };

      v2f vert(appdata v) {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

      sampler2D _MainTex;

      float linearToSrgb(float x) {
        if (x <= 3.1308e-3f) return x * 12.92f;
        return 1.055f * pow(x, 1.0f / 2.4f) - 0.055f;
      }

      float srgbToLinear(float x) {
        if (x <= 4.045e-2f) return x / 12.92f;
        return pow((x + 0.055f) / 1.055f, 2.4f);
      }

      float3 srgbToLinear(float3 srgb) {
        return float3(srgbToLinear(srgb.x),
                      srgbToLinear(srgb.y),
                      srgbToLinear(srgb.z));
      }

      float3 linearToSrgb(float3 lin) {
        return float3(linearToSrgb(lin.x),
                      linearToSrgb(lin.y),
                      linearToSrgb(lin.z));
      }

      fixed4 frag(v2f i) : SV_Target {
        fixed4 col = tex2D(_MainTex, i.uv);

        float3 comps = srgbToLinear(col.rgb);

        float3 sat = float3(_Fac1 * comps.x, _Fac2 * comps.y, _Fac3 * comps.z),
               gray = float3((1.0f - _Fac1) * comps.x,
                             (1.0f - _Fac2) * comps.y,
                             (1.0f - _Fac3) * comps.z);

        // Rec.601:  (0.2989, 0.5870, 0.1140)
        // Rec.709:  (0.2126, 0.7152, 0.0722)
        // Rec.2020: (0.2627, 0.6780, 0.0593)

        float3 rgb = sat + _Gray.rgb * (0.2989 * gray.r + 0.5870 * gray.g + 0.1140 * gray.b);

        rgb = lerp(rgb, _FadeTo, _Fade);

        col.rgb = linearToSrgb(rgb);

        return col;
      }

      ENDCG
    }
  }
}