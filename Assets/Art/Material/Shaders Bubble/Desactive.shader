Shader "Custom/Desactive"
{
 Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Desaturation ("Desaturation", Range(0, 1)) = 0.5
        _BlurAmount ("Blur Amount", Range(0, 5)) = 2.0
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Desaturation;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                float gray = dot(texColor.rgb, float3(0.3, 0.59, 0.11));
                texColor.rgb = lerp(float3(gray, gray, gray), texColor.rgb, _Desaturation);
                texColor.a *= 0.5; // Reduce alpha for a faded effect
                return texColor;
            }
            ENDCG
        }
    }
}