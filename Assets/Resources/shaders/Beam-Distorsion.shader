Shader "Custom/Distort" {
	Properties{
		_Refraction("Refraction", Range(0.00, 100.0)) = 1.0
		_Speed("Distort. Speed", Float) = 0.2
		_Freq("Distort. Freq", Float) = 1.0
		_Amp("Distort. Amp", Float) = 1.0
		_DistortTex("Base (RGB)", 2D) = "white" {}
	// as color mask
		_BeamTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader{

		Tags{ "RenderType" = "Transparent" "Queue" = "Overlay" }
		LOD 100

		GrabPass
		{

		}

		CGPROGRAM
		#pragma surface surf NoLighting
		#pragma vertex vert

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		sampler2D _GrabTexture : register(s0);
		sampler2D _DistortTex : register(s2);
		sampler2D _BeamTex;
		float _Refraction;
		float _Speed;
		float _Freq;
		float _Amp;

		float4 _GrabTexture_TexelSize;

		struct Input {
			float2 uv_DistortTex;
			float2 uv_BeamTex;
			float3 color;
			float3 worldRefl;
			float4 screenPos;
			INTERNAL_DATA
		};

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.color = v.color;
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

		// 0.97 // 0.97 // 0.88
		float isNoiseColor(float3 rgb, float error) {
			return rgb.r == 0.97f && rgb.g == 0.97f && rgb.b == 0.88f;
			return abs(rgb.r- 0.97f)<error && abs(rgb.g-0.97f)<error && abs(rgb.b-0.88f)<error;
		}

		float isInMask(float3 rgb, float3 m, float error) {
			return abs(rgb.r-m.r)<error && abs(rgb.g - m.g)<error &&abs(rgb.b - m.b)<error;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			IN.screenPos.y = 1.0f - IN.screenPos.y;
			float3 behindColor = tex2D(_GrabTexture, IN.screenPos.xy);
			float4 distortColor = tex2D(_DistortTex, IN.uv_DistortTex);
			//float4 beamColor = tex2D(_BeamTex, IN.uv_BeamTex)*distortColor.a;
			float3 distortion = float3(0.0f, 0.0f, 0.0f);
			if (!isNoiseColor(behindColor, 0.001f)) {
				o.Albedo = behindColor;// float3(1.0f, 0.0f, 0.0f);
				//float2 hashUV = IN.uv_DistortTex + float2(0.0f, _Time.y*_Speed);
				//distortion = disp(hashUV)*distortColor.r*distortColor.a;// float3(h, h, h)*distortColor.r*distortColor.a;
			}
			/*float3 distort = distortion * float3(IN.color.r, IN.color.g, IN.color.b);
			float2 offset = distort;

			IN.screenPos.xy = offset * IN.screenPos.z + IN.screenPos.xy;

			float4 refrColor = tex2Dproj(_GrabTexture, IN.screenPos);
			o.Alpha = refrColor.a;
			o.Emission = refrColor.rgb;*/
		}
		ENDCG
	}
}