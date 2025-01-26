Shader "Custom/Bubble"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1, 1, 1, 1)
        _Iridescence ("Iridescence Strength", Range(0, 1)) = 0.5
        _Distortion ("Distortion Amount", Range(0, 1)) = 0.1
        _Transparency ("Transparency", Range(0, 1)) = 0.7
        _AlphaCutoff ("Alpha Cutoff", Range(0, 1)) = 0.1
        _PixelSize ("Pixel Size", Range(1, 64)) = 8 // Control the pixel size
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Iridescence;
            float _Distortion;
            float _Transparency;
            float _AlphaCutoff;
            float _PixelSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Pixelation logic
                float2 pixelatedUV = floor(i.uv * _PixelSize) / _PixelSize;

                // Sample the base texture using pixelated UVs
                fixed4 texColor = tex2D(_MainTex, pixelatedUV);

                // Circular mask (clipping pixels outside a circle)
                float2 center = float2(0.5, 0.5); // Circle center
                float dist = distance(i.uv, center); // Distance from center
                if (dist > 0.5) discard; // Discard pixels outside the circle

                // Alpha clipping
                if (texColor.a < _AlphaCutoff)
                    discard;

                // Iridescent effect
                float3 iridescentColor = float3(
                    sin(i.uv.x * 10.0 + _Time.y) * 0.5 + 0.5,
                    sin(i.uv.y * 10.0 + _Time.y) * 0.5 + 0.5,
                    sin((i.uv.x + i.uv.y) * 10.0 + _Time.y) * 0.5 + 0.5
                );

                // Add distortion
                float2 distortion = (i.uv - 0.5) * _Distortion;
                float2 distortedUV = pixelatedUV + distortion;
                fixed4 distortedTex = tex2D(_MainTex, distortedUV);

                // Combine texture with iridescence
                fixed4 finalColor = lerp(texColor, distortedTex, _Distortion);
                finalColor.rgb += iridescentColor * _Iridescence;

                // Apply tint and transparency
                finalColor *= _TintColor;
                finalColor.a *= _Transparency;

                return finalColor;
            }
            ENDCG
        }
    }
}
