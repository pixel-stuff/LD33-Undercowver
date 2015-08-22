Shader "Custom/BeamShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
		_NormalTex ("Normal map", 2D) = "bump" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		ZWrite Off
		//Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		
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
		};

		fixed4 _Color;
		fixed4 _Normal;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 n = tex2D (_NormalTex, IN.uv_BumpMap);
			// Metallic and smoothness come from slider variables
			float lambertTerm = dot(normalize(IN.viewDir), o.Normal);
			//if(c.r<=1.0 && c.g<=1.0 && c.b<=1.0)
			//	c.a = 0.0;
			fixed alpha = abs(IN.uv_MainTex-0.5);
			fixed col_sample = 1-alpha;
			fixed3 col = fixed3(col_sample, col_sample, col_sample);
			//if(1-alpha*2.0f<0.4f)
			//	alpha = 0.0f;
			o.Albedo = c.rgb;
			o.Normal = n.rgb;
			o.Alpha = alpha;
			o.Albedo = clamp(col, 0.1, 1.0);
			//float rim = saturate(lambertTerm);
			//if (rim >= 0.98) {
				//o.Emission = 1.5; // or any large value
			//}
		}
		ENDCG
	} 
	FallBack "Particle/Additive"
}
