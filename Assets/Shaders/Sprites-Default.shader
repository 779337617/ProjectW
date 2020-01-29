// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Default_zuo"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
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
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex SpriteVert
			#pragma fragment SpriteFrag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"
		ENDCG
		}

		

		Pass {
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }

			ZWrite On ZTest LEqual

			CGPROGRAM
			#pragma target 3.0

			// -------------------------------------


			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			#pragma shader_feature _METALLICGLOSSMAP
			#pragma shader_feature _PARALLAXMAP
			#pragma multi_compile_shadowcaster
			#pragma multi_compile_instancing

			#pragma vertex vertShadowCaster
			#pragma fragment fragShadowCaster

			#include "UnityStandardShadow.cginc"

			ENDCG
		}

		/*
		   pass{  
		   Tags{"LightMode"="ForwardBase"}  
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
            float4 _spPos;  
            float _spR;  
            float _Intensity;  
            float4 _LightColor0;  
            struct v2f{  
                float4 pos:SV_POSITION;  
                float3 litDir:TEXCOORD0;  
                float3 spDir:TEXCOORD1;  
                float4 vc:TEXCOORD2;  
            };  
        v2f vert(appdata_base v)  
        {  
            v2f o;  
            o.pos=mul(UNITY_MATRIX_MVP,v.vertex);  
            o.litDir=WorldSpaceLightDir(v.vertex);  
            o.spDir=(_spPos-mul(_Object2World,v.vertex)).xyz;  
  
            float3 ldir=ObjSpaceLightDir(v.vertex);  
            ldir=normalize(ldir);  
            o.vc=_LightColor0*max(0,dot(ldir,v.normal));  
            return o;  
        }  
        float4 frag(v2f i):COLOR  
        {  
            float3 litDir=normalize(i.litDir);  
            float3 spDir=i.spDir;  
            float spDistance=length(spDir);  
            spDir=normalize(spDir);  
  
            float cosV=dot(spDir,litDir);  
            float sinV=sin(acos(max(0,cosV)));  
            float D=sinV*spDistance;  
            float shadow=step(_spR,D);//spR>D 0,else 1  
            float atten=pow((D/_spR),4);  
            float c=lerp(1-_Intensity,1,min(1,shadow+atten));//0 is dark  //*step(0,dot(i.N,litDir))  
            return i.vc*c;  
        }  
        ENDCG  
        }//endpass  
			*/
	}
	FallBack "VertexLit"
}
