Shader "Unlit/MaskDo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _TransparentAmount("Transparent Amount",Float)=1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent"}
        
        Pass{
            ZWrite On
            ColorMask 0
        }
 
        Pass
        {
            Tags{"LightMode"="ForwardBase"}
 
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
 
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
            float4 _MainTex_ST;
            sampler2D  _MaskTex;
            float4 _MaskTex_ST; 
            Float _TransparentAmount;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 mask = tex2D(_MaskTex, i.uv);
                col.a =  col.a * mask.a * _TransparentAmount;
                return col;
            }
            ENDCG
        }
    }
}