// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Scene Manager/Tiles Effect" {
	Properties {
		_Background ("Background", 2D) = "black" {}
		_Backface ("Backface", 2D) = "black" {}
		_BlendMode ("Blend Mode", range(0,1)) = 0	// 0 == Background, 1 == Tiles
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _Background;
	half4 _Background_ST;	
	sampler2D _ScreenContent;
	half4 _ScreenContent_ST;
	sampler2D _Backface;
	half4 _Backface_ST;	
	float _BlendMode;
			
	struct v2f {
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
		half2 uvBackground : TEXCOORD1;
		half2 uvBackface : TEXCOORD2;
	};
	
	v2f vert(appdata_full v) {
		v2f o;
		o.pos = UnityObjectToClipPos (v.vertex);	
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _ScreenContent);
		o.uvBackground.xy = TRANSFORM_TEX(v.texcoord, _Background);
		o.uvBackface.xy = TRANSFORM_TEX(v.texcoord, _Backface);
		#if UNITY_UV_STARTS_AT_TOP
		o.uv.y = 1 - o.uv.y;
		#endif		
		return o; 
	}
	
	fixed4 frag(v2f i) : COLOR {	
		return lerp(tex2D(_ScreenContent, i.uv.xy), tex2D(_Background, i.uvBackground.xy), 1 - _BlendMode);
	}
	
	ENDCG        
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		Lighting Off
		LOD 200
	
		GrabPass { "_ScreenContent" }
	
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	
		Pass { 
			Cull Front 
			SetTexture [_Backface] {
                Combine texture
            }
		}
	}
	
	FallBack "Diffuse"
} 