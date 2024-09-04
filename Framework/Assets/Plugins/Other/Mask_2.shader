Shader "3D-Default"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        //»Ì≤√«–XY÷·µƒ≥§∂»
        _ClipSoftX ("_ClipSoftX", Float) = 0
        _ClipSoftY ("_ClipSoftY", Float) = 0
        
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
             
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
			};
 
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
                float4 worldPosition : TEXCOORD1;
			};
 
			sampler2D _MainTex;
			float4 _MainTex_ST;
		    float _Scale;
            
			v2f vert (appdata v)
			{
				v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPosition = mul(unity_ObjectToWorld,v.vertex * _Scale);
                return o;
			}
			
            float4 _ClipRect;
            float _ClipSoftX;
            float _ClipSoftY;
            inline float SoftUnityGet2DClipping (in float2 position, in float4 clipRect)
            {
                float2 xy = (position.xy-clipRect.xy)/float2(_ClipSoftX,_ClipSoftY)*step(clipRect.xy, position.xy);
                float2 zw = (clipRect.zw-position.xy)/float2(_ClipSoftX,_ClipSoftY)*step(position.xy,clipRect.zw);
                float2 factor = clamp(0, zw, xy);
                return saturate(min(factor.x,factor.y));
            }
			
            fixed4 frag (v2f i) : SV_Target
			{
			   fixed4 color = tex2D(_MainTex, i.uv);
               //º∆À„»Ì≤√«–
               color.a *= SoftUnityGet2DClipping(i.worldPosition.xy, _ClipRect);
               return color;
                
			}
			ENDCG
		}
	}
}