Shader "SVGA"
{
    
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" { }
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _AtlasTex("Atlas Texture", 2D) = "white" {}

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        
        _ColorMask ("Color Mask", Float) = 15
        // instancing matrix
        _MatCol1 ("_MatCol1", VECTOR) = (1, 1, 1, 1)
        _MatCol2 ("_MatCol2", VECTOR) = (1, 1, 1, 1)
        _MatCol3 ("_MatCol3", VECTOR) = (1, 1, 1, 1)
        _MatCol4 ("_MatCol4", VECTOR) = (1, 1, 1, 1)
        // 当前纹理的坐标跟尺寸
        _TexInfo("_TexInfo", VECTOR) = (1, 1, 1, 1)
        _TexSize("_TexSize", VECTOR) = (0, 0, 0, 0)
        _InsColor("_InsColor", Color) = (1, 1, 1, 1)
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "True" }
        
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
        Blend SrcAlpha OneMinusSrcAlpha //SrcAlpha的值乘以源 Alpha 值  + OneMinusSrcAlpha的值乘以（1 - 源 Alpha）。
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
            
            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP

            #pragma multi_compile_instancing
            struct appdata_t
            {
                float4 vertex: POSITION;
                float4 color: COLOR;
                float2 texcoord: TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f
            {
                float4 vertex: SV_POSITION;
                fixed4 color: COLOR;
                float2 texcoord: TEXCOORD0;
                float4 worldPosition: TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID // use this to access instanced properties in the fragment shader.
            };
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _MatCol1)
                UNITY_DEFINE_INSTANCED_PROP(float4, _MatCol2)
                UNITY_DEFINE_INSTANCED_PROP(float4, _MatCol3)
                UNITY_DEFINE_INSTANCED_PROP(float4, _MatCol4)
                UNITY_DEFINE_INSTANCED_PROP(float4, _TexInfo)
                UNITY_DEFINE_INSTANCED_PROP(float4, _TexSize)
                UNITY_DEFINE_INSTANCED_PROP(float4, _InsColor)
            UNITY_INSTANCING_BUFFER_END(Props)
            sampler2D _MainTex;
            sampler2D _AtlasTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            
            float4x4 _Matrix32;
            
            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                
                OUT.worldPosition = v.vertex;
                #ifdef UNITY_INSTANCING_ENABLED
                    float4 texSize = UNITY_ACCESS_INSTANCED_PROP(Props, _TexSize);
                    float4 column1 = UNITY_ACCESS_INSTANCED_PROP(Props, _MatCol1);
                    float4 column2 = UNITY_ACCESS_INSTANCED_PROP(Props, _MatCol2);
                    float4 column3 = UNITY_ACCESS_INSTANCED_PROP(Props, _MatCol3);
                    float4 column4 = UNITY_ACCESS_INSTANCED_PROP(Props, _MatCol4);
                    v.vertex = mul(float4x4(column1, column2, column3, column4), mul(float4x4(texSize.x,0,0,texSize.x*0.5, 0,texSize.y,0,-texSize.y*0.5, 0,0,1,0, 0, 0,0,1), float4(v.vertex)));
                #else
                    v.vertex = mul(_Matrix32, float4(v.vertex));
                #endif
                OUT.vertex = UnityObjectToClipPos(v.vertex);
                
                //OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                //OUT.texcoord = mul(_2DMatrix, float4(OUT.texcoord, 0, 1)).xy;
                OUT.color = v.color * _Color;
                return OUT;
            }
            
            fixed4 frag(v2f IN): SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                half4 color;
                #ifdef UNITY_INSTANCING_ENABLED
                    float4 texInfo = UNITY_ACCESS_INSTANCED_PROP(Props, _TexInfo);
                    float4 inColor = UNITY_ACCESS_INSTANCED_PROP(Props, _InsColor);
                    color = (tex2D(_AtlasTex, float2(texInfo.x + IN.texcoord.x*texInfo.z, texInfo.y + IN.texcoord.y*texInfo.w)) + _TextureSampleAdd) * inColor;
                #else    
                    color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                #endif
                
                
                #ifdef UNITY_UI_CLIP_RECT
                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
                
                #ifdef UNITY_UI_ALPHACLIP
                    clip(color.a - 0.001);
                #endif
                
                return color;
            }

            ENDCG
        }
    }
}

