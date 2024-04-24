﻿Shader "MyShaders/LiquidMaskableShader3D"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Progress ("Progress", Range(0.0, 1.0)) = 0.5
        _WaterColor ("WaterColor", Color) = (1.0, 1.0, 0.2, 1.0)
        _SecondWaterColor ("Second WaterColor", Color) = (0.2, 0.2, 0.5, 1.0)
        _WaveStrength ("WaveStrength", Float) = 2.0
        _WaveFrequency ("WaveFrequency", Float) = 180.0
        _SecondWaveStrength ("SecondWaveStrength", Float) = 1.5
        _SecondWaveFrequency ("SecondWaveFrequency", Float) = 200.0
        _WaterTransparency ("WaterTransparency", Float) = 1.0
        _WaterAngle ("WaterAngle", Float) = 4.0
        _Velocity ("Velocity", Float) = 1.0 // Nueva propiedad para la velocidad
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _StencilOp ("Stencil Operation", Float) = 0 // Add StencilOp property
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
            "Maskable" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            float _Progress;
            fixed4 _WaterColor;
            fixed4 _SecondWaterColor;
            float _WaveStrength;
            float _WaveFrequency;
            float _SecondWaveStrength;
            float _SecondWaveFrequency;
            float _WaterTransparency;
            float _WaterAngle;
            float _Velocity; // Nueva propiedad para la velocidad
            sampler2D _MaskTex;
            float _StencilOp; // Add _StencilOp variable

            fixed4 drawWater(fixed4 water_color, sampler2D color, sampler2D mask, float transparency, float height, float angle, float wave_strength, float wave_ratio, fixed2 uv);
            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 uv = i.texcoord;
                float WATER_HEIGHT = _Progress;
                float4 WATER_COLOR = _WaterColor;
                float4 SECOND_WATER_COLOR = _SecondWaterColor;
                float WAVE_STRENGTH = _WaveStrength;
                float WAVE_FREQUENCY = _WaveFrequency;
                float SECOND_WAVE_STRENGTH = _SecondWaveStrength;
                float SECOND_WAVE_FREQUENCY = _SecondWaveFrequency;
                float WATER_TRANSPARENCY = _WaterTransparency;
                float WATER_ANGLE = _WaterAngle;
                float VELOCITY = _Velocity; // Obtener la velocidad

                // Primer pase de onda
                fixed4 fragColor = drawWater(WATER_COLOR, _MainTex, _MaskTex, WATER_TRANSPARENCY, WATER_HEIGHT, WATER_ANGLE, WAVE_STRENGTH, WAVE_FREQUENCY, uv);

                // Segundo pase de onda con una progresión multiplicada por la velocidad
                float iTime2 = _Time * VELOCITY; // Multiplica el tiempo por la velocidad
                float WAVE_STRENGTH2 = SECOND_WAVE_STRENGTH * 0.8;
                float WAVE_FREQUENCY2 = SECOND_WAVE_FREQUENCY * 0.9;
                fixed4 WATER_COLOR2 = SECOND_WATER_COLOR * 0.8;
                fragColor += drawWater(WATER_COLOR2, _MainTex, _MaskTex, WATER_TRANSPARENCY, WATER_HEIGHT, WATER_ANGLE, WAVE_STRENGTH2, WAVE_FREQUENCY2, uv);

                return fragColor;
            }

            fixed4 drawWater(fixed4 water_color, sampler2D color, sampler2D mask, float transparency, float height, float angle, float wave_strength, float wave_frequency, fixed2 uv)
            {
                float iTime = _Time;
                angle *= uv.y/height+angle/1.5; // Efecto 3D
                wave_strength /= 1000.0;
                float wave = sin(10.0*uv.y+10.0*uv.x+wave_frequency*iTime)*wave_strength;
                wave += sin(20.0*-uv.y+20.0*uv.x+wave_frequency*1.0*iTime)*wave_strength*0.5;
                wave += sin(15.0*-uv.y+15.0*-uv.x+wave_frequency*0.6*iTime)*wave_strength*1.3;
                wave += sin(3.0*-uv.y+3.0*-uv.x+wave_frequency*0.3*iTime)*wave_strength*10.0;
                
                if(uv.y - wave <= height)
                {
                    // Aplicar la máscara
                    fixed4 maskColor = tex2D(_MaskTex, uv);
                    if (maskColor.a <= 0.5) // Si el valor alfa de la máscara es menor o igual a 0.5, descartar el píxel
                        discard;

                    return lerp(
                    lerp(
                        tex2D(color, fixed2(uv.x, ((1.0 + angle)*(height + wave) - angle*uv.y + wave))),
                        water_color,
                        0.6-(0.3-(0.3*uv.y/height))),
                    tex2D(color, fixed2(uv.x + wave, uv.y - wave)),
                    transparency-(transparency*uv.y/height));
                }
                else
                {
                    return fixed4(0,0,0,0);
                }
            }
        ENDCG
        }
    }
}
