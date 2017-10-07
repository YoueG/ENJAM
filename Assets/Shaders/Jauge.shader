﻿Shader "Custom/Jauge"
{
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Ratio ("Ratio", Range(0,1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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

			float _Ratio;

			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				if(i.uv.y > _Ratio)
				{
					discard;
				}

				fixed4 col = _Color;
				UNITY_APPLY_FOG(i.fogCoord, col);

				return _Color;		
			}
			ENDCG
		}
	}
}
