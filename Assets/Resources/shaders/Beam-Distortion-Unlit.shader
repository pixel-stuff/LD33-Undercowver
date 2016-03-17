Shader "Custom/Beam-Distortion-Unlit"
{
	Properties
	{
		_Refraction("Refraction", Range(0.00, 100.0)) = 1.0
		_Speed("Distort. Speed", Float) = 0.2
		_Freq("Distort. Freq", Float) = 1.0
		_Amp("Distort. Amp", Float) = 1.0
		_DistortTex("Distort (RGB)", 2D) = "white" {}
		// as color mask
		_MaskTex("Mask (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Overlay" }
		LOD 100

		GrabPass{}

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
				float4 color : COLOR;
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 color : COLOR;
				float2 uv : TEXCOORD0;
				float3 worldRefl : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
			};

			sampler2D _GrabTexture : register(s0);
			sampler2D _DistortTex : register(s2);
			sampler2D _MaskTex;
			float _Refraction;
			float _Speed;
			float _Freq;
			float _Amp;
			float4 _GrabTexture_TexelSize;

			float4 _DistortTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o = (v2f)0;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _DistortTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.color = v.color;

				half4 screenpos = ComputeGrabScreenPos(o.vertex);
				o.screenPos.xy = screenpos.xy / screenpos.w;
				half depth = length(mul(UNITY_MATRIX_MV, v.vertex));
				o.screenPos.z = depth;
				o.screenPos.w = depth;
				return o;
			}

			float hash(float2 uv) {
				return frac(sin(dot(uv, float2(100.3f, 10.73f)))*51.214255);
			}

			float noise(float2 uv) {
				return lerp(hash(uv + float2(-0.1f, 0.0f)), hash(uv + float2(0.1f, 0.0f)), hash(uv));
			}

			float3 disp(float2 uv) {
				return float3(sin(uv.y*_Freq)*_Amp, 0.0f, 0.0f);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = fixed4(0, 0, 0, 1);
				float4 distortColor = tex2D(_MaskTex, i.uv);
				float3 distort = tex2D(_DistortTex, i.uv) * float3(i.color.r,i.color.g,i.color.b);
				float2 offset = distort * _Refraction * _GrabTexture_TexelSize.xy;
				if (!(distortColor.r == 1.0f && distortColor.g == 1.0f && distortColor.b == 1.0f)) {
					i.screenPos.xy = offset * i.screenPos.z + i.screenPos.xy;
				}
				col = tex2D(_GrabTexture, i.screenPos);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}