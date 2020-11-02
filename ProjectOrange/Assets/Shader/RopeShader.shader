//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/RopeShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Up ("Up Vector", Vector) = (0,0,0,0)
		_GunLocation ("Gun Location", Vector) = (0,0,0,0)
	}
	SubShader
	{
		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			float4 _Up;
			float4 _GunLocation;

			struct vertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				//float4 displacement = float4(0.0f, sin(v.vertex.x * 10.0f), 0.0f, 0.0f); // Q4
				v.vertex = mul(UNITY_MATRIX_M, v.vertex);

				float lengthA  = distance(v.vertex, _GunLocation);
				float4 displacement = sin(lengthA - 0.98f) * _Up;
				v.vertex += displacement;

				vertOut o;
				o.vertex = mul(UNITY_MATRIX_VP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, v.uv);
				return col;
			}
			ENDCG
		}
	}
}
