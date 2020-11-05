//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/RippleShader"
{
	Properties
	{
		_Amplitude("Amplitude of wave", Float) = 0.8
		_Speed("Speed", Float) = 0.7
		_Wavelength("Wavelength", Float) = 7
		_Color("Color", Color) = (1, 0.59, 0.2)
		_ColorMinValue("ColorMinValue", Float) = 0.5
		_ColorMaxValue("ColorMaxValue", Float) = 1.1
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
			float _ColorMinValue;
			float _ColorMaxValue;

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
				float h = sin(d / _Wavelength + _Time.y * _Speed);
				v.vertex.y += _Amplitude * h;

				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float c = lerp(_ColorMinValue, _ColorMaxValue, (h + 1) / 2);
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
