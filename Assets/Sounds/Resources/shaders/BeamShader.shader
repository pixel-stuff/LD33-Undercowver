Shader "Custom/BeamShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
		//_NormalTex ("Normal map", 2D) = "bump" {}
	}
	SubShader {
		// Horizontal blur pass
//		Pass
//		{
//			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
//			LOD 200
//			ZWrite Off
//			Cull Off
//			//Blend SrcAlpha OneMinusSrcAlpha
//			
//			CGPROGRAM
//			// Physically based Standard lighting model, and enable shadows on all light types
//			#pragma surface surf Lambert alpha
//
//			// Use shader model 3.0 target, to get nicer looking lighting
//			#pragma target 3.0
//
//			sampler2D _MainTex;
//			//sampler2D _NormalTex;
//
//			struct Input {
//				float2 uv_MainTex;
//				float2 uv_BumpMap;
//				float3 viewDir;
//				float3 worldPos;
//				INTERNAL_DATA
//			};
//
//			fixed4 _Color;
//			fixed4 _Normal;
//
//			fixed _step(fixed a, fixed s) {
//				return (fixed)((int)a);
//			}
//
//			void surf (Input IN, inout SurfaceOutput o) {
//				// Albedo comes from a texture tinted by color
//				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//				//o.Normal = c.rgb;
//				//fixed4 n = tex2D (_NormalTex, IN.uv_BumpMap);
//				fixed alpha = IN.uv_MainTex.y;//abs(IN.uv_MainTex.x-0.5);
//				fixed color = alpha;
//				o.Albedo = fixed3(color, color, color);
//				o.Alpha = alpha;
//				float blurAmount = 0.0075;
//
//				half4 sum = half4(0.0, 0.0, 0.0, 0.0);
//
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x - 5.0 * blurAmount, IN.uv_MainTex.y)) * 0.025;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x - 4.0 * blurAmount, IN.uv_MainTex.y)) * 0.05;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x - 3.0 * blurAmount, IN.uv_MainTex.y)) * 0.09;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x - 2.0 * blurAmount, IN.uv_MainTex.y)) * 0.12;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x - blurAmount, IN.uv_MainTex.y)) * 0.15;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x, IN.uv_MainTex.y)) * 0.16;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + blurAmount, IN.uv_MainTex.y)) * 0.15;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 2.0 * blurAmount, IN.uv_MainTex.y)) * 0.12;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 3.0 * blurAmount, IN.uv_MainTex.y)) * 0.09;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 4.0 * blurAmount, IN.uv_MainTex.y)) * 0.05;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 5.0 * blurAmount, IN.uv_MainTex.y)) * 0.025;
//
//				sum.r = 1.0;
//				sum.g = 1.0;
//				sum.b = 0.0;
//
//				o.Albedo = sum;
//			}
//			ENDCG
//		}
//
//		GrabPass { }
//
//		// Vertical blur pass
//		Pass
//		{
//			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
//			LOD 200
//			ZWrite Off
//			Cull Off
//			//Blend SrcAlpha OneMinusSrcAlpha
//			
//			CGPROGRAM
//			// Physically based Standard lighting model, and enable shadows on all light types
//			#pragma surface surf Lambert alpha
//
//			// Use shader model 3.0 target, to get nicer looking lighting
//			#pragma target 3.0
//
//			sampler2D _GrabTexture;
//			sampler2D _NormalTex;
//
//			struct Input {
//				float2 uv_MainTex;
//				float2 uv_BumpMap;
//				float3 viewDir;
//				float3 worldPos;
//				INTERNAL_DATA
//			};
//
//			fixed4 _Color;
//			fixed4 _Normal;
//
//			fixed _step(fixed a, fixed s) {
//				return (fixed)((int)a);
//			}
//
//			void surf (Input IN, inout SurfaceOutput o) {
//				// Albedo comes from a texture tinted by color
//				fixed alpha = IN.uv_MainTex.y;//abs(IN.uv_MainTex.x-0.5);
//				fixed color = alpha;
//				o.Albedo = fixed3(color, color, color);
//				o.Alpha = alpha;
//
//				float blurAmount = 0.0075;
//
//				half4 sum = half4(0.0, 0.0, 0.0, 0.0);
//
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y - 5.0 * blurAmount)) * 0.025;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y - 4.0 * blurAmount)) * 0.05;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y - 3.0 * blurAmount)) * 0.09;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y - 2.0 * blurAmount)) * 0.12;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y - blurAmount)) * 0.15;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y)) * 0.16;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + blurAmount)) * 0.15;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + 2.0 * blurAmount)) * 0.12;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + 3.0 * blurAmount)) * 0.09;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + 4.0 * blurAmount)) * 0.05;
//				sum += tex2D(_GrabTexture, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + 5.0 * blurAmount)) * 0.025;
//
//				sum.r = 1.0;
//				sum.g = 1.0;
//				sum.b = 0.0;
//
//				o.Albedo = sum;
//			}
//			ENDCG
//		}

		// Original image pass
		//Pass
		//{
			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
			LOD 200
			ZWrite Off
			Cull Off
			//Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Lambert alpha

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _NormalTex;

			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float3 viewDir;
				float3 worldPos;
				INTERNAL_DATA
			};

			fixed4 _Color;
			fixed4 _Normal;

			fixed _step(fixed a, fixed s) {
				return (fixed)((int)a);
			}
			
			float3 blur(half3 initial_color, half xy, float blurAmount) {
				half3 sum = initial_color;
				sum+= float3(xy-5.0*blurAmount,xy-5.0*blurAmount, xy-5.0*blurAmount)*0.025;
				sum+= float3(xy-4.0*blurAmount,xy-4.0*blurAmount, xy-4.0*blurAmount)*0.05;
				sum+= float3(xy-3.0*blurAmount,xy-3.0*blurAmount, xy-3.0*blurAmount)*0.09;
				sum+= float3(xy-25.0*blurAmount,xy-2.0*blurAmount, xy-2.0*blurAmount)*0.125;
				sum+= float3(xy-blurAmount,xy-blurAmount, xy-blurAmount)*0.15;
				sum+= float3(xy,xy, xy)*0.16;
				sum+= float3(xy+blurAmount,xy+blurAmount, xy+blurAmount)*0.15;
				sum+= float3(xy+2.0*blurAmount,xy+2.0*blurAmount, xy+2.0*blurAmount)*0.12;
				sum+= float3(xy+3.0*blurAmount,xy+3.0*blurAmount, xy+3.0*blurAmount)*0.09;
				sum+= float3(xy+4.0*blurAmount,xy+4.0*blurAmount, xy+4.0*blurAmount)*0.05;
				sum+= float3(xy+5.0*blurAmount,xy+5.0*blurAmount, xy+5.0*blurAmount)*0.025;
				return sum;
			}

			void surf (Input IN, inout SurfaceOutput o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
				//o.Normal = c.rgb;
				//fixed4 n = tex2D (_NormalTex, IN.uv_BumpMap);
				fixed alpha = IN.uv_MainTex.y;//abs(IN.uv_MainTex.x-0.5);
				fixed color = alpha;
				//o.Albedo = fixed3(color, color, color);
				o.Alpha = alpha;
				float blurAmount = 0.0075;
//
				half3 sum = color;
				
				sum = blur(color, IN.uv_MainTex.x, blurAmount);
				sum = blur(color, IN.uv_MainTex.y, blurAmount);
				o.Albedo = sum;
//
//				sum += tex2D(color, float2(IN.uv_MainTex.x - 5.0 * blurAmount, IN.uv_MainTex.y)) * 0.025;
//				sum += tex2D(color, float2(IN.uv_MainTex.x - 4.0 * blurAmount, IN.uv_MainTex.y)) * 0.05;
//				sum += tex2D(color, float2(IN.uv_MainTex.x - 3.0 * blurAmount, IN.uv_MainTex.y)) * 0.09;
//				sum += tex2D(color, float2(IN.uv_MainTex.x - 2.0 * blurAmount, IN.uv_MainTex.y)) * 0.12;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x - blurAmount, IN.uv_MainTex.y)) * 0.15;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x, IN.uv_MainTex.y)) * 0.16;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + blurAmount, IN.uv_MainTex.y)) * 0.15;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 2.0 * blurAmount, IN.uv_MainTex.y)) * 0.12;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 3.0 * blurAmount, IN.uv_MainTex.y)) * 0.09;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 4.0 * blurAmount, IN.uv_MainTex.y)) * 0.05;
//				sum += tex2D(_MainTex, float2(IN.uv_MainTex.x + 5.0 * blurAmount, IN.uv_MainTex.y)) * 0.025;
//
				//if(alpha>0.4) {
				//	fixed col_sample = (1-alpha)*(1-alpha);
				//	fixed3 col = fixed3(col_sample, col_sample, col_sample);
				//	o.Alpha = alpha;//alpha;
				//	o.Albedo = col;
				//	o.Specular = 1.0;
				//}
			}
			ENDCG
		//}
	}
	FallBack "Particle/Additive"
}
