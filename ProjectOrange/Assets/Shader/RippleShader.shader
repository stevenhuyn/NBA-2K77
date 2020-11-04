//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/RippleShader"
{
	Properties
	{
		_Amplitude("Amplitude of wave", Float) = 0.8
		_Speed("Speed", Float) = 0.7
		_Wavelength("Wavelength", Float) = 7
		_Color("Color", Color) = (1, 0.59, 0.2)
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

			float _Amplitude;
			float _Wavelength;
			float _Speed;
			float3 _Color;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				float d = length(float2(v.vertex.x, v.vertex.z));
				float h = sin(d / _Wavelength * _Time.x * _Speed);
				v.vertex.y += _Amplitude * h;


				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float min_c = 0.5, max_c = 1.1;
				float c = (h + 1 + min_c) / 2 * max_c;
				o.color.rgb = c * _Color;
				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				return v.color;
			}
			ENDCG
		}
	}
}
