Shader "Custom/House" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		_BlurAmount ("Blur Amount", Range(0.1, 1.0)) = 0.1
		_Specular ("Specular", Range(0.1, 1.0)) = 0.1
		_Gloss ("Gloss", Range(0.1, 1.0)) = 0.1
		_Emission ("Emission", Color) = (1,1,1,1)
	}

	SubShader {
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff

		sampler2D _MainTex;
		fixed4 _Color;
		fixed4 _Emission;
		fixed _Specular;
		fixed _Gloss;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
		//	if(c.r>0.65 && c.g>0.55 && c.g>0.55) {
//			if(c.r>0.35 && (c.r-c.g)<0.1 && (c.g-c.b)<0.1) {
		//		half4 sum = half4(0.0, 0.0, 0.0, 0.0);
		//
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x - 5.0 * _BlurAmount, IN.texcoord.y)) * 0.025;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x - 4.0 * _BlurAmount, IN.texcoord.y)) * 0.05;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x - 3.0 * _BlurAmount, IN.texcoord.y)) * 0.09;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x - 2.0 * _BlurAmount, IN.texcoord.y)) * 0.12;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x - _BlurAmount, IN.texcoord.y)) * 0.15;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y)) * 0.16;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x + _BlurAmount, IN.texcoord.y)) * 0.15;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x + 2.0 * _BlurAmount, IN.texcoord.y)) * 0.12;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x + 3.0 * _BlurAmount, IN.texcoord.y)) * 0.09;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x + 4.0 * _BlurAmount, IN.texcoord.y)) * 0.05;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x + 5.0 * _BlurAmount, IN.texcoord.y)) * 0.025;
		//		
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 5.0 * _BlurAmount)) * 0.025;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 4.0 * _BlurAmount)) * 0.05;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 3.0 * _BlurAmount)) * 0.09;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 2.0 * _BlurAmount)) * 0.12;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 1.0 * _BlurAmount)) * 0.15;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y)) * 0.16;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 1.0 * _BlurAmount)) * 0.15;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 2.0 * _BlurAmount)) * 0.12;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 3.0 * _BlurAmount)) * 0.09;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 4.0 * _BlurAmount)) * 0.05;
		//		sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 5.0 * _BlurAmount)) * 0.025;
		//		
		//		c.rgb = sum.rgb;
		//		fixed s = 0.0005;
		//		half4 sum = tex2D(_MainTex, float2(
		//						IN.texcoord.x - s,
		//						IN.texcoord.y - s));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x - s,
		//						IN.texcoord.y));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x - s,
		//						IN.texcoord.y + s));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x,
		//						IN.texcoord.y - s));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x,
		//						IN.texcoord.y));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x,
		//						IN.texcoord.y + s));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x + s,
		//						IN.texcoord.y - s));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x + s,
		//						IN.texcoord.y));
		//		sum *= tex2D(_MainTex, float2(
		//						IN.texcoord.x + s,
		//						IN.texcoord.y + s));
		//		c.rgb = sum.rgb;
		//		int size = 4;
		//		fixed4 t = fixed4(1, 1, 1, 1);
		//		for(int i=0;i<size+1;i++) {
		//			for(int j=0;j<size+1;j++) {
		//				t = tex2D(
		//					_MainTex,
		//					float2(
		//						IN.texcoord.x + i-size/2,
		//						IN.texcoord.y + j-size/2)
		//				);
		//				if(t.r>0.75 && t.g>0.75 && t.b>0.75) {
		//					c.rgb*=t.rgb*1.2;
		//				}
		//			}
//					}
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
//			if(c.r>0.35 && (c.r-c.g)<0.1 && (c.g-c.b)<0.1) {
//				//c.a = 0.0;
//			}else{
//				c *= _Color;
//			}
			o.Albedo = c.rgb;
			//o.Emission = _Emission;
			o.Specular = _Specular;
			o.Gloss = _Gloss;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}


//Shader "Custom/house"
//{
//	Properties
//	{
//		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
//		_Color ("Tint", Color) = (1,1,1,1)
//		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
//		_BlurAmount ("Blur Amount", Range(0.1, 1.0)) = 0.1
//	}
//
//	SubShader
//	{
//		Tags
//		{ 
//			"Queue"="Transparent" 
//			"IgnoreProjector"="True" 
//			"RenderType"="TransparentCutout" 
//			"PreviewType"="Plane"
//			"CanUseSpriteAtlas"="True"
//			"LightMode"="ForwardBase"
//		}
//
//		Cull Off
//		Lighting Off
//		ZWrite Off
//		Blend One OneMinusSrcAlpha
//
//		Pass
//		{
//		CGPROGRAM
////			#pragma surface surf Lambert alphatest:_Cutoff vertex:vert fragment:frag
//			#pragma vertex vert
//			#pragma fragment frag
//			#pragma multi_compile _ PIXELSNAP_ON
//			#pragma alphatest:_Cutoff
//			#include "UnityCG.cginc"
////			
//			struct appdata_t
//			{
//				float4 vertex   : POSITION;
//				float4 color    : COLOR;
//				float2 texcoord : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float4 vertex   : SV_POSITION;
//				fixed4 color    : COLOR;
//				half2 texcoord  : TEXCOORD0;
//			};
//			
//			fixed4 _Color;
//			fixed _BlurAmount;
//
//			v2f vert(appdata_t IN)
//			{
//				v2f OUT;
//				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
//				OUT.texcoord = IN.texcoord;
//				OUT.color = IN.color * _Color;
//				#ifdef PIXELSNAP_ON
//				OUT.vertex = UnityPixelSnap (OUT.vertex);
//				#endif
//
//				return OUT;
//			}
//
//			sampler2D _MainTex;
//
//			fixed4 frag(v2f IN) : SV_Target
//			{
//				fixed4 c = tex2D(_MainTex, IN.texcoord);
//				if(c.r>0.65 && c.g>0.55 && c.g>0.55) {
////				if(c.r>0.35 && (c.r-c.g)<0.1 && (c.g-c.b)<0.1) {
////					half4 sum = half4(0.0, 0.0, 0.0, 0.0);
////
////					sum += tex2D(_MainTex, float2(IN.texcoord.x - 5.0 * _BlurAmount, IN.texcoord.y)) * 0.025;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x - 4.0 * _BlurAmount, IN.texcoord.y)) * 0.05;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x - 3.0 * _BlurAmount, IN.texcoord.y)) * 0.09;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x - 2.0 * _BlurAmount, IN.texcoord.y)) * 0.12;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x - _BlurAmount, IN.texcoord.y)) * 0.15;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y)) * 0.16;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x + _BlurAmount, IN.texcoord.y)) * 0.15;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x + 2.0 * _BlurAmount, IN.texcoord.y)) * 0.12;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x + 3.0 * _BlurAmount, IN.texcoord.y)) * 0.09;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x + 4.0 * _BlurAmount, IN.texcoord.y)) * 0.05;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x + 5.0 * _BlurAmount, IN.texcoord.y)) * 0.025;
////					
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 5.0 * _BlurAmount)) * 0.025;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 4.0 * _BlurAmount)) * 0.05;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 3.0 * _BlurAmount)) * 0.09;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 2.0 * _BlurAmount)) * 0.12;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y - 1.0 * _BlurAmount)) * 0.15;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y)) * 0.16;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 1.0 * _BlurAmount)) * 0.15;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 2.0 * _BlurAmount)) * 0.12;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 3.0 * _BlurAmount)) * 0.09;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 4.0 * _BlurAmount)) * 0.05;
////					sum += tex2D(_MainTex, float2(IN.texcoord.x, IN.texcoord.y + 5.0 * _BlurAmount)) * 0.025;
////					
////					c.rgb = sum.rgb;
////					fixed s = 0.0005;
////					half4 sum = tex2D(_MainTex, float2(
////									IN.texcoord.x - s,
////									IN.texcoord.y - s));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x - s,
////									IN.texcoord.y));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x - s,
////									IN.texcoord.y + s));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x,
////									IN.texcoord.y - s));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x,
////									IN.texcoord.y));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x,
////									IN.texcoord.y + s));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x + s,
////									IN.texcoord.y - s));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x + s,
////									IN.texcoord.y));
////					sum *= tex2D(_MainTex, float2(
////									IN.texcoord.x + s,
////									IN.texcoord.y + s));
////					c.rgb = sum.rgb;
////					int size = 4;
////					fixed4 t = fixed4(1, 1, 1, 1);
////					for(int i=0;i<size+1;i++) {
////						for(int j=0;j<size+1;j++) {
////							t = tex2D(
////								_MainTex,
////								float2(
////									IN.texcoord.x + i-size/2,
////									IN.texcoord.y + j-size/2)
////							);
////							if(t.r>0.75 && t.g>0.75 && t.b>0.75) {
////								c.rgb*=t.rgb*1.2;
////							}
////						}
////					}
//				}else{
//					c *= IN.color;
//				}
//				c.rgb *= c.a;
//				return c;
//			}
//		ENDCG
//		}
//	}
//}