/**
 * Invisibility/Obfuscation effect for sprites
 * @author Pierre-Marie Plans
 * @date 07/04/2016
 **/

Shader "Custom/Obfuscate"
{
	Properties
	{
		_Refraction("Refraction", Range(0.00, 100.0)) = 1.0
		_Speed("Distort. Speed", Float) = 0.2
		_Freq("Distort. Freq", Float) = 1.0
		_Amp("Distort. Amp", Float) = 1.0
		_DistortTex("Distort (RGB)", 2D) = "white" {}
		// as color mask
		_SpriteTex("Mask (RGB)", 2D) = "white" {}
		_StartAnim("Start Anim Time", Float) = 0.0
		_FadeInKeepOut("Type of shading", Float) = 1.0
		_UseInternalTime("Use Internal Time ?", Float) = 0.0
		[HideInInspector]_ScriptTime("Script Time", Float) = 0.0
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
			sampler2D _SpriteTex;
			float _Refraction;
			float _Speed;
			float _Freq;
			float _Amp;
			float4 _GrabTexture_TexelSize;
			float _StartAnim;
			float _FadeInKeepOut;
			float _UseInternalTime;
			float _ScriptTime;

			float4 _DistortTex_ST;

			v2f vert(appdata v)
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

			float hash3D(float3 uv) {
				return frac(sin(dot(uv, float3(100.3f, 10.73f, 1.0f)))*51.214255);
			}

			float noise3D(float3 uv) {
				float3 fl = floor(uv);
				float3 fr = frac(uv);
				return lerp(
					lerp(
						lerp(hash3D(fl + float3(0.0f, 0.0f, 0.0f)), hash3D(fl + float3(1.0f, 0.0f, 0.0f)), fr.x),
						lerp(hash3D(fl + float3(0.0f, 1.0f, 0.0f)), hash3D(fl + float3(1.0f, 1.0f, 0.0f)), fr.x),
						fr.y),
					lerp(
						lerp(hash3D(fl + float3(0.0f, 0.0f, 1.0f)), hash3D(fl + float3(1.0f, 0.0f, 1.0f)), fr.x),
						lerp(hash3D(fl + float3(0.0f, 1.0f, 1.0f)), hash3D(fl + float3(1.0f, 1.0f, 1.0f)), fr.x),
						fr.y),
					fr.z);
			}

			float perlin3D(float3 uv) {
				float total = 0;
				float p = 1.3f;
				for (int i = 0; i < 4; i++) {
					float freq = 2.0f*float(i);
					float amplitude = p*float(i);
					total += noise3D(uv*freq) * amplitude;
				}
				return total;
			}

			float heatNoise(float3 uv) {
				float h = 0.0f;
				h = perlin3D(uv);
				return h;
			}

			float3 disp(float2 uv) {
				return float3(sin((uv.y + _Time.y*_Speed)*_Freq)*_Amp, 0.0f, 0.0f);
			}

			float3 dispHeat(float2 uv) {
				float N = heatNoise(float3(uv.x, uv.y, _Time.y*_Speed));
				return float3(N, N, N);
			}

			float3 dispTex(float2 uv) {
				tex2D(_DistortTex, uv*3.0f + float2(_Time.y / 40.0f, _Time.w / 40.0f));
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(0, 0, 0, 1);
				// getting the sprite texture
				float4 spriteColor = tex2D(_SpriteTex, i.uv);

				// getting 
				if (spriteColor.a == 1.0f) {
					float3 distort = dispHeat(i.uv) * float3(i.color.r, i.color.g, i.color.b);
					float2 offset = distort * _Refraction * _GrabTexture_TexelSize.xy;
					if (!(spriteColor.r == 1.0f && spriteColor.g == 1.0f && spriteColor.b == 1.0f)) {
						i.screenPos.xy = offset * i.screenPos.z + i.screenPos.xy;
					}
					col = tex2D(_GrabTexture, i.screenPos);

					// animation effect computation
					if (_FadeInKeepOut == 0.0) {
						fixed2 center = fixed2(0.5, 0.5);
						float t = 0.0;
						if (_UseInternalTime) t = _Time.y*0.1 - _StartAnim;
						else t = _ScriptTime;
						float alphaSprite = max(0.0, length(i.uv - center) - (t));
						// lerp with the sprite color for animation
						col = lerp(col, spriteColor, alphaSprite);
					}
					else if (_FadeInKeepOut == 2.0) {
						fixed2 center = fixed2(0.5, 0.5);
						float t = 0.0;
						if (_UseInternalTime) t = _Time.y*0.1 - _StartAnim;
						else t = _ScriptTime;
						float alphaSprite = min(1.0, length(i.uv - center) + (t));
						// lerp with the sprite color for animation
						col = lerp(col, spriteColor, alphaSprite);
					}
				}
				else if (spriteColor.a < 1.0f) {
					float2 grabTexcoord = i.screenPos.xy;
					fixed4 colTransparency = tex2D(_GrabTexture, grabTexcoord);

					float3 distort = dispHeat(i.uv) * float3(i.color.r, i.color.g, i.color.b);
					float2 offset = distort * _Refraction * _GrabTexture_TexelSize.xy;
					if (!(spriteColor.r == 1.0f && spriteColor.g == 1.0f && spriteColor.b == 1.0f)) {
						i.screenPos.xy = offset * i.screenPos.z + i.screenPos.xy;
					}
					fixed4 colDistort = tex2D(_GrabTexture, i.screenPos);

					col = lerp(colTransparency, colDistort, spriteColor.a);

				}else {
					float2 grabTexcoord = i.screenPos.xy;
					col = tex2D(_GrabTexture, grabTexcoord);
				}


				//col.r = spriteColor.a;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}