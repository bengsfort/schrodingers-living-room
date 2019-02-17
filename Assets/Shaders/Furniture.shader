Shader "Sch/Furniture/Furniture"
{
    Properties
    {
			_MainTexLive("Live Sprite Texture", 2D) = "white" {}
			_MainTexDead("Dead Sprite Texture", 2D) = "white" {}
			_Color("Tint", Color) = (1,1,1,1)
			[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
			[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
			[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
			[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
			[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
		_State("State", float) = 1
	}

		SubShader
		{
			Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
		{
			CGPROGRAM


			//Hash functions from https://www.shadertoy.com/view/4djSRW
			#define ITERATIONS 4

			// *** Change these to suit your range of random numbers..

			// *** Use this for integer stepped ranges, ie Value-Noise/Perlin noise functions.
			//#define HASHSCALE1 .1031
			//#define HASHSCALE3 float3(.1031, .1030, .0973)
			//#define HASHSCALE4 float4(.1031, .1030, .0973, .1099)

			// For smaller input rangers like audio tick or 0-1 UVs use these...
			#define HASHSCALE1 443.8975
			#define HASHSCALE3 vec3(443.897, 441.423, 437.195)
			#define HASHSCALE4 vec3(443.897, 441.423, 437.195, 444.129)

			//----------------------------------------------------------------------------------------
			//  1 out, 2 in...
			float hash12(float2 p)
		{
			float3 p3 = frac(float3(p.xyx) * HASHSCALE1);
			p3 += dot(p3, p3.yzx + 19.19);
			return frac((p3.x + p3.y) * p3.z);
		}

				#pragma vertex SpriteVertFurniture
				#pragma fragment SpriteFragFurniture
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile _ PIXELSNAP_ON
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
				#include "UnitySprites.cginc"
				#include "UnityCG.cginc"
				//#include "UnityCG.glslinc"

			//float4 iResolution = _ScreenParams;


			float _State;
			sampler2D _MainTexLive;
			sampler2D _MainTexDead;

			v2f SpriteVertFurniture(appdata_full IN)
			{
				v2f OUT;

				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

				OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
				OUT.vertex = UnityObjectToClipPos(OUT.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color * _RendererColor;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SpriteFragFurniture(v2f IN) : SV_Target
			{

				float2 screenPos = IN.vertex.xy / _ScreenParams.w;
				screenPos = floor(screenPos / 16);
				float2 noise = hash12(float2(_Time.w + screenPos.x, screenPos.y + _Time.x));
				fixed state = saturate(_State + noise);

				noise *= abs(_State) > .999 ? 0 : 1;

				fixed4 d = tex2D(_MainTexDead, IN.texcoord + float2(noise.x * 0.081, 0));
				fixed4 l = tex2D(_MainTexLive, IN.texcoord + float2(0, noise.y * 0.12));

				fixed4 c = lerp(d, l, state);
				c.rgb *= c.a;
				return c;
			}
			
			ENDCG
		}
		}
}







