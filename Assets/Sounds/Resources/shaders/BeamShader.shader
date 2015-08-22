Shader "Custom/BeamShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
		_NormalTex ("Normal map", 2D) = "bump" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
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
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			float lambertTerm = dot(normalize(IN.viewDir), n.rgb);
			//if(c.r<=1.0 && c.g<=1.0 && c.b<=1.0)
			//	c.a = 0.0;
			o.Alpha = c.a;//+lambertTerm;
			float rim = saturate(dot(normalize(IN.viewDir), o.Normal));
			if (rim >= 0.98) {
			//	o.Emission = 5.5; // or any large value
			}
		}
		ENDCG
	} 
	FallBack "Particle/Additive"
}
