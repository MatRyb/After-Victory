Shader "Unlit/SlashShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SlashProgress("SlashProgress", Float) = 0.5
        _SlashDirection("SlashDirection", Float) = -1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _SlashProgress;
            float _SlashDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4x4 Bayer4 = float4x4(
                    0.0f, 8.0f, 2.0f, 10.0f,
                    12.0f, 4.0f, 14.0f, 6.0f,
                    3.0f, 11.0f, 1.0f, 9.0f,
                    15.0f, 7.0f, 13.0f, 5.0f
                ) / 16.f;


                // Time bend
                _SlashProgress = smoothstep(lerp(0.0, 1.2, sign(1.0f + _SlashDirection)), lerp(1.2, 0.0, sign(1.f + _SlashDirection)), _SlashProgress);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                float resolution = 80.0f;
                int2 pixelPos = floor(i.uv * resolution);
                float2 quantUv = pixelPos / resolution;

                float2 bigCircleScale;
                bigCircleScale.x = 1.0f;
                bigCircleScale.y = 1.0f;

                float2 cutCircleScale;
                float endWidth = 0.5f;
                float startWidth = 1.5f;

                cutCircleScale.x = lerp(startWidth, endWidth, _SlashProgress);
                cutCircleScale.y = lerp(endWidth, startWidth, _SlashProgress);

                float bigCircleDist = length(bigCircleScale * quantUv);
                float cutCircleDist = length(cutCircleScale * quantUv) * smoothstep(0.0, 0.3, _SlashProgress);

                float bigCirclePrecised = smoothstep(0.89, 0.9, bigCircleDist);
                float cutCurclePrecised = smoothstep(0.6, 0.8, cutCircleDist);

                float fadeFromRight = smoothstep(0.1, 0.3, 1.f - (1.0f * quantUv.x + _SlashProgress * 2.0f - 0.3f));
                
                float closerEdge = min(quantUv.x, quantUv.y);
                float edgeSmooth = smoothstep(0.0, 0.1, closerEdge);

                float value = edgeSmooth * cutCurclePrecised* (1.0f - bigCirclePrecised)* fadeFromRight;


                float bayer = Bayer4[pixelPos.x%4][pixelPos.y%4];

                return ceil(value - bayer);
            }
            ENDCG
        }
    }
}
