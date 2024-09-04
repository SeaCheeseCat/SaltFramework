Shader "Custom/ColorAdjustEffect"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Brightness("Brightness", Float) = 1 //调整亮度
        _Saturation("Saturation", Float) = 1 //调整饱和度
        _Contrast("Contrast", Float) = 1 //调整对比度
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
            ZWrite On
            ZTest LEqual // 使用小于等于测试来确保像素只有在深度缓冲区中的值小于或等于像素的深度值时才被渲染
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
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
                float _Brightness;
                float _Saturation;
                float _Contrast;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                // 调整亮度
                col.rgb *= _Brightness;

                // 调整饱和度
                float gray = dot(col.rgb, fixed3(0.2125, 0.7154, 0.0721));
                col.rgb = lerp(gray, col.rgb, _Saturation);

                // 调整对比度
                col.rgb = lerp(fixed3(0.5, 0.5, 0.5), col.rgb, _Contrast);

                return col;  
            }
            ENDCG
        }
        }
}
