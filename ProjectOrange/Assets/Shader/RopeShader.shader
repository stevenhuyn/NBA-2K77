//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/RopeShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Up ("Up Vector", Vector) = (0,0,0,0)
		_GunLocation ("Gun Location", Vector) = (0,0,0,0)
		_HookLocation ("Hook Location", Vector) = (0,0,0,0)
		_Amplitude ("Amplitude", float) = 0.5
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
			float4 _HookLocation;
			float _Amplitude;

			float normaliseAmplitude(float d)
			{
				return (-0.3f/(d + 1.0f)) + 0.3f;
			}

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

				float lengthA  = abs(distance(v.vertex, _GunLocation));
				float hookDistance = abs(distance(v.vertex, _HookLocation));
				float gunNorm = normaliseAmplitude(lengthA - 0.98f);
				float hookNorm = normaliseAmplitude(hookDistance - 0.98f);
				float4 displacement = min(gunNorm, hookNorm) * sin(lengthA - 0.98f + _Time[3]*10) * _Up * _Amplitude;
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
