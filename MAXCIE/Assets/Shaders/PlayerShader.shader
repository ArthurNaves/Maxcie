﻿#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Player Shader " {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_Ambient ("Ambient Color", Color) = (0.588, 0.588, 0.588, 1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
	
		
		//_OverlayColor ("Overlay Color", Color) = (0.1, 0.1, 0.8, 0.5)
	
		//_Emissive ("Emissive Color", Color) = (0.588, 0.588, 0.588, 1)
		//_Diffuse ("Diffuse Color", Color) = (1, 1, 1, 1)
	}
	
	SubShader {
		Tags { 
			"Queue"="Geometry+5" 
			"IgnoreProjector"="True"
			//"RenderType"="Transparent"	
		 }
		 
		 Pass {
			Name "Overlay"
			//Blend SrcAlpha OneMinusSrcAlpha
			zwrite off
			ztest greater
			//Color [_OverlayColor]
			/*
			SetTexture [_MainTex] {
				combine texture * _OverlayColor
			}
			*/
			
			CGPROGRAM
			#pragma vertex vert 
	        #pragma fragment frag
	        #include "UnityCG.cginc" 
	        
	        struct v2f {
	        	float4 pos : SV_POSITION;
	        	float2 uv : TEXCOORD0;
	        };
	        
	        uniform sampler2D _MainTex;
	        uniform fixed4 _OverlayColor;
	        
	        v2f vert(appdata_base v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
	          	return o;
			}
			fixed4 frag(v2f i) : Color {
				return tex2D(_MainTex, i.uv)*_OverlayColor;
			}
			
	        ENDCG
			
		}
		
		Pass {
			Name "BASE"
			
			LOD 200
			CGPROGRAM
			
			#pragma vertex vert 
	        #pragma fragment frag
	        #include "UnityCG.cginc"
	        
	        
	        uniform fixed4 _Color;
			uniform fixed4 _HighLightDir;
			uniform fixed4 _Ambient;
			//uniform fixed4 _Emissive;
			uniform fixed4 _LightDiffuseColor;
			//uniform fixed4 _Diffuse;
			
	        struct v2f {
	        	float4 pos : SV_POSITION;
	        	float2 uv : TEXCOORD0;
	        	//fixed3 diff;
	        	float4 colour : TEXCOORD1;
	        };
	        
	        uniform sampler2D _MainTex;
	        
	        v2f vert(appdata_base v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
				fixed3 viewNor = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
				fixed3 lightDir = -normalize(_HighLightDir);
				
	          	o.colour = _Ambient*_Color+ _Color*saturate(dot(lightDir, viewNor))*_LightDiffuseColor;
	          	o.colour.a = 1;
				return o;
			}
			
			fixed4 frag(v2f i) : Color {
				return tex2D(_MainTex, i.uv)*i.colour;
			}
	        ENDCG
		}
		
		
		Pass {
			Name "SHADOW"
			Blend One Zero
			
			Offset -1.0, -2.0
			CGPROGRAM
			#pragma vertex vert 
	         #pragma fragment frag
	 
	         #include "UnityCG.cginc"
	 
	         // User-specified uniforms
	         uniform fixed4 _ShadowColor;
	         uniform fixed4x4 _World2Receiver; // transformation from 
	         uniform fixed4 _LightDir;
	        
	         float4 vert(float4 vertexPos : POSITION) : SV_POSITION
	         {
	            float4x4 modelMatrix = unity_ObjectToWorld;
	            float4x4 modelMatrixInverse = 
	               unity_WorldToObject * 1.0;
	            modelMatrixInverse[3][3] = 1.0; 
	            float4x4 viewMatrix = 
	               mul(UNITY_MATRIX_MV, modelMatrixInverse);
	 
	            float4 lightDirection = _LightDir;
	            lightDirection = normalize(lightDirection);
	            
	            float4 vertexInWorldSpace = mul(modelMatrix, vertexPos);
	            
	           	float4 world2ReceiverRow1 = 
	               float4(_World2Receiver[1][0], _World2Receiver[1][1], 
	               _World2Receiver[1][2], _World2Receiver[1][3]);
	           
	            float distanceOfVertex = 
	               dot(world2ReceiverRow1, vertexInWorldSpace); 
	            
	            float lengthOfLightDirectionInY = 
	               dot(world2ReceiverRow1, lightDirection); 
	 
	            if (distanceOfVertex > 0.0 && lengthOfLightDirectionInY < 0.0)
	            {
	               lightDirection = lightDirection 
	                  * (distanceOfVertex / (-lengthOfLightDirectionInY));
	            }
	            else
	            {
	               lightDirection = float4(0.0, 0.0, 0.0, 0.0); 
	                  // don't move vertex
	            }
	 
	            return mul(UNITY_MATRIX_P, mul(viewMatrix, 
	               vertexInWorldSpace + lightDirection));
	         	//return mul(UNITY_MATRIX_P, mul(viewMatrix, vertexInWorldSpace));
	         	
	         }
	 
	         float4 frag(void) : COLOR 
	         {
	            return _ShadowColor;
	         }
			
			ENDCG
		}
		
		
	} 
	FallBack "Diffuse"
}