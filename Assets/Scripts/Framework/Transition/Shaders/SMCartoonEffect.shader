// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Scene Manager/Cartoon Effect" {
	Properties {
	    _BorderColor ("Border Color", Color) = (.5,0,0,1)
		_Distance ("Distance", float) = 0	
		_CenterX ("CenterX", float) = .5
		_CenterY ("CenterY", float) = .5
		_Background ("Background", 2D) = "black" {}		
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _ScreenContent;
	half4 _ScreenContent_ST;
	float4 _BorderColor;
	float _Distance;
	float _CenterX;
	float _CenterY;
	sampler2D _Background;
	half4 _Background_ST;	
							
	struct v2f {
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
		half2 uvBackground : TEXCOORD1;		
	};
	
	v2f vert(appdata_full v) {
		v2f o;	
		o.pos = UnityObjectToClipPos (v.vertex);	
		o.uv.xy = TRANSFORM_TEX(v.texcoord, _ScreenContent);
		o.uvBackground.xy = TRANSFORM_TEX(v.texcoord, _Background);			
		#if UNITY_UV_STARTS_AT_TOP
		o.uv.y = 1 - o.uv.y;
		#endif
		return o; 
	}
	
	fixed4 frag(v2f i) : COLOR {	
		#if UNITY_UV_STARTS_AT_TOP
		float realY = _ScreenParams.y - _CenterY;
		#else
		float realY = _CenterY;
		#endif
		float distance = length (half2(i.uv.x * _ScreenParams.x, i.uv.y * _ScreenParams.y) - half2(_CenterX, realY));
		float4 screenColor = tex2D(_ScreenContent, i.uv.xy);
		float delta = distance - _Distance; // < 0 for any pixel within the circle; 0 for any pixel at the circle; > 0 for any pixel outside of the circle
		fixed4 maskColor = lerp(_BorderColor, tex2D(_Background, i.uvBackground.xy), clamp(delta / 5f, 0, 1));
		return lerp(screenColor, maskColor, (clamp(delta, -15, 0) / 15f) + 1);  // fade inside
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
	}
	
	FallBack "Diffuse"
} 