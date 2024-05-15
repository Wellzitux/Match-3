Shader "Welliton/Unlit/Shader_UI_Foliage_VertexDisplacement"
{
    Properties
    {
        _WindVelocity ("Amplitude", Float) = 0
        _NoiseScale ("Noise Scale", float) = 1
        _SmoothStepMin ("Smooth Step Min", Range(0, 10)) = 0.3
        _SmoothStepMax ("Smooth Step Max", Range(0, 10)) = 0.7
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
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
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _WindVelocity;
            float _NoiseScale;
            float _SmoothStepMin;
            float _SmoothStepMax;
            float _HeightMin;
            float _HeightMax;

            float2 unity_gradientNoise_dir(float2 p)
            {
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }

            float unity_gradientNoise(float2 p)
            {
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(unity_gradientNoise_dir(ip), fp);
                float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
            }

            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            {
                Out = unity_gradientNoise(UV * Scale) + 0.5;
            }



v2f vert(appdata_t v)
{
    v2f OUT;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

    float noise;
    OUT.color = v.color * _Color;   
    OUT.vertex = UnityObjectToClipPos(v.vertex);

    // Defina a altura mínima e máxima onde o efeito será aplicado
    float minHeight = _HeightMin;
    float maxHeight = _HeightMax;

    // Calcule a altura normalizada do vértice (entre 0 e 1)
    float normalizedHeight = saturate((OUT.texcoord.y - minHeight) / (maxHeight - minHeight));   

    // Aplique uma função de interpolação (smoothstep, lerp, etc.) para determinar a intensidade do efeito com base na altura
    float smoothValue = smoothstep(_SmoothStepMin, _SmoothStepMax, OUT.texcoord.y);

    float time = _Time.y; // Use _Time.y para obter o tempo em segundos
    // Calcule o ruído de gradiente
    Unity_GradientNoise_float(v.vertex.xy + time, _NoiseScale, noise);

    // Calcule o deslocamento do vértice na direção Y e aplique a intensidade do efeito
    float displacementY = noise * _WindVelocity * smoothValue;
    OUT.vertex.y += displacementY;

    // Calcule o deslocamento do vértice na direção X e aplique a intensidade do efeito
    float displacementX = noise * _WindVelocity * smoothValue;
    OUT.vertex.x += displacementX;

    return OUT;
}


            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
        ENDCG
        }
    }
}