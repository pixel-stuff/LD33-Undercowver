Shader "Custom/SpriteLighting"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
			"LightMode" = "ForwardBase"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			//#pragma multi_compile_PIXELSNAP_ON
			#pragma multi_compile_fwbase
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float3 normal	: NORMAL;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float3 lightDir	: TEXCOORD1;
				float3 normal	: TEXCOORD2;
				LIGHTING_COORDS(3, 4)
			};
			
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
//				fixed3 lDir = fixed3(0, 0, 0);
//				fixed3 atten = 1.0;
//				fixed3 diffuseLight = fixed3(0, 0, 0);
//				int i;
//				bool isLight = false;
//				for(i=0;i<6;i++) {
//					if(unity_LightPosition[i].w!=0.0) {
//						lDir = normalize(mul(unity_LightPosition[i], UNITY_MATRIX_IT_MV).xyz - IN.vertex.xyz);
//						atten = 1.0/(length(mul(unity_LightPosition[i], UNITY_MATRIX_IT_MV).xyz - IN.vertex.xyz))*0.5;
//						fixed3 nDir = normalize(IN.normal);
//						diffuseLight += unity_LightColor[i].xyz*max(dot(nDir,lDir),0);
//						isLight = true;
//					}
//				}
//				if(!isLight)
//				OUT.color.xyz = diffuseLight * atten;
//				if(isLight) OUT.color.xyz = fixed3(1, 0, 0);
				OUT.lightDir = ObjSpaceLightDir(IN.vertex);//mul(UNITY_MATRIX_MVP, ObjSpaceLightDir(IN.vertex));
				OUT.normal = IN.normal;//mul(UNITY_MATRIX_MVP, IN.normal);
				TRANSFER_VERTEX_TO_FRAGMENT(OUT)
				return OUT;
			}

			sampler2D _MainTex;
			float4 _LightColor0;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, IN.texcoord) * IN.color;
				fixed atten = length(IN.lightDir);//LIGHT_ATTENUATION(IN);
				//IN.lightDir = normalize(IN.lightDir);
				fixed lambertTerm = dot(normalize(IN.lightDir), normalize(IN.normal));
				fixed4 c = fixed4(0, 0, 0, 0);
				//c.rgb = UNITY_LIGHTMODEL_AMBIENT.rgb;
				c.rgb += tex.rgb * atten * 2;
				//c.rgb = atten;
//				fixed diff = saturate(dot(IN.lightDir, IN.normal));
//				c.rgb = UNITY_LIGHTMODEL_AMBIENT.rgb;
//				c.rgb+= tex.rgb * diff * atten;
//				//c.rgb = tex.rgb;
				c.rgb *= tex.a;
				c.a = tex.a;
				//c.rgb *= c.a*LIGHT_ATTENUATION(IN);
				//c.rgb *= c.a*atten;
				//c.a *= atten;
				return c;
			}
		ENDCG
		}
	}
}
