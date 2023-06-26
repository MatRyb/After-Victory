Shader "Unlit/QuadShader"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _AddColor("Add Color", Color) = (1, 1, 1, 1)
        _PulseScale("Pulse Scale/Selected", Float) = 1.0

        _CooldownProgress("Cooldown Progress", Float) = 0.3
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        //Blend One One

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
            float4 _AddColor;
            float _PulseScale;
            float _CooldownProgress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = _AddColor;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float edgeDist = i.uv.x +  i.uv.y;
                float radialDist = length(i.uv);
                float reverseRadialDist = 1.f - radialDist;
                
                float edgeDrawDist = 0.01f;
                float sideEdges = smoothstep(edgeDrawDist, 0.0f, min(i.uv.x, i.uv.y));

                float visibleRadiusStart = 0.7f;
                float visibleRadiusEnd = 0.9f;


                float radialVisibility = smoothstep(visibleRadiusEnd, visibleRadiusStart, radialDist);
                
                float radialIndicatorStart = 0.0f;
                float indicatorSpeed = 50.f;
                float radialIndicatorEnd = 0.5f + 0.5f * (1.f + sin(_Time* indicatorSpeed))/2.f;

                float radialIndicatorAlpha = 0.1f + (1.f + sin( _Time * indicatorSpeed ))/2.f;
                float radialIndicator = smoothstep(radialIndicatorEnd, radialIndicatorStart, radialDist) * radialIndicatorAlpha;

                float waveScale = 1.f;
                float waveFade = 0.1f;
                float waveDensity = 5.f;
                float waveDepth = 0.9999f;
                float waveSpeed = 50;
                float wave = ((1.f + sin( reverseRadialDist * waveDensity + _Time * waveSpeed)) / 2.f - waveDepth) / (1.f - waveDepth);

                float distanceFade = clamp(0, 1, reverseRadialDist);
                
                float pulseEdgeFade = smoothstep(0.02, 0.1, min(i.uv.x, i.uv.y));
                float pulse = clamp(wave * wave * wave * clamp(reverseRadialDist, 0, 1), 0, 1) * pulseEdgeFade;
                float sides = clamp(sideEdges , 0, 1) * radialVisibility;
                float indicator = radialIndicator;

                float activeAmbient = distanceFade * 0.1f;

                float pulseAlpha = 0.4f;
                float sidesAlpha = 0.2f;
                float indicatorAlpha = .2f;
                
                float PI = 3.141592;

                //_CooldownProgress = 0.2f + frac(_Time.x);
                float2 pointOnCooldownCircle;
                pointOnCooldownCircle.x = sin(_CooldownProgress * PI / 2.f);
                pointOnCooldownCircle.y = cos(_CooldownProgress * PI / 2.f);
                float2 radRelativePos = i.uv - pointOnCooldownCircle;

                float2 pointOnCooldownCircleDerivative;
                pointOnCooldownCircleDerivative.x = cos(_CooldownProgress * PI / 2.f);
                pointOnCooldownCircleDerivative.y = -sin(_CooldownProgress * PI / 2.f);

                float cooldownProgressCut = 1.f - smoothstep(-0.005, 0.005, dot(pointOnCooldownCircleDerivative, radRelativePos));
                float cooldownCircleRadius = 0.3f;
                float cooldownCircleMask = smoothstep(-0.005, 0.005, cooldownCircleRadius - length(i.uv)) ;

                float postCooldownCircleFade = smoothstep(0.0, 0.01, 1.f - _CooldownProgress);

                float postCooldownBooster = clamp(smoothstep(0.02, 0.0, (1.f - _CooldownProgress)) * clamp(1.5f - _CooldownProgress, 0, 1), 0, 1);
                float boosterMultiplier = 20.f *postCooldownBooster;
                //// pulseAlpha = max(pulseAlpha, boosterMultiplier);
                //sidesAlpha = max(sidesAlpha, boosterMultiplier);
                //indicatorAlpha = max(indicatorAlpha, boosterMultiplier);
                _PulseScale = max(_PulseScale, boosterMultiplier);

                float normalValue = _PulseScale * pulseAlpha * pulse + clamp(sidesAlpha + _PulseScale * (1.f - sidesAlpha), 0, 1) * sides + _PulseScale * indicatorAlpha * radialIndicator + _PulseScale * activeAmbient;
                //value *= cooldownProgressCut;
                float cooldownValue = cooldownProgressCut * cooldownCircleMask;
                
                float value = lerp(cooldownValue, normalValue, 1.f - postCooldownCircleFade);
                col.rgb += postCooldownBooster / 1.5f;
                col.a = value;
                return col;
            }
            ENDCG
        }
    }
}
