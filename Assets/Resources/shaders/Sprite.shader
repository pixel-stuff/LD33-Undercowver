Shader "Custom/Sprite" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Cutoff ("Shadow alpha cutoff", Range(0,1)) = 1.0
	}
	SubShader {
		//Tags { "RenderType"="Opaque" "CanUseSpriteAtlas"="True" }
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		LOD 300
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha fullforwardshadows
		#include "AutoLight.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			LIGHTING_COORDS(0,10)
		};
		
        struct appdata_t
        {
            float4 vertex   : POSITION;
            float4 color    : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex   : SV_POSITION;
            fixed4 color    : COLOR;
            half2 texcoord  : TEXCOORD0;
        };

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
 
//        v2f vert(appdata_t IN)
//        {
//            v2f OUT;
//            OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
//            OUT.texcoord = IN.texcoord;
//            OUT.color = IN.color * _Color;
//            #ifdef PIXELSNAP_ON
//            OUT.vertex = UnityPixelSnap (OUT.vertex);
//            #endif

//            return OUT;
//        }

        fixed4 frag(v2f IN) : SV_Target
        {
            fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
            c.rgb *= c.a;
            return c;
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float atten = LIGHT_ATTENUATION(IN);
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = atten*c.rgb*c.a;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	Fallback "Transparent/Cutout/VertexLit"
//	FallBack "Sprite/Diffuse"
}
