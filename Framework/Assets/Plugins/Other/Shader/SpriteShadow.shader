Shader "Custom/SpriteShadow"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
        _Cutoff("Alpha Cutoff",Range(0,1)) = 0.5
        _MainTex("Texture", 2D) = "white" {}
        _LuminosityAmount("GrayScale Amount", Range(0.0,1.0)) = 1.0
    }

        SubShader
        {

            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha

            CGPROGRAM
            #pragma surface surf Lambert vertex:vert alphatest:_Cutoff addshadow nofog nolightmap nodynlightmap keepalpha noinstancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA

            #include "UnitySprites.cginc"

            struct Input
            {
                float2 uv_MainTex;
                fixed4 color;
            };

            void vert(inout appdata_full v, out Input o)
            {
                v.vertex = UnityFlipSprite(v.vertex, _Flip);

                #if defined(PIXELSNAP_ON)
                v.vertex = UnityPixelSnap(v.vertex);
                #endif

                UNITY_INITIALIZE_OUTPUT(Input, o);
                o.color = v.color * _Color * _RendererColor;
            }

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
                o.Albedo = c.rgb * c.a;
                o.Alpha = c.a;
            }


            ENDCG

            Pass
            {
                CGPROGRAM
                #pragma vertex vert_img
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
                sampler2D _MainTex;
                fixed _LuminosityAmount;

                fixed4 frag(v2f_img i) : SV_Target
                {
                    fixed4 renderTex = tex2D(_MainTex, i.uv);

                    float luminosity = 0.299 * renderTex.r + 0.587 * renderTex.g + 0.114 * renderTex.b;

                    fixed4 col = lerp(renderTex, luminosity,_LuminosityAmount);

                    return col;
                }
                ENDCG
            }
        }

            Fallback "Transparent/VertexLit"
}