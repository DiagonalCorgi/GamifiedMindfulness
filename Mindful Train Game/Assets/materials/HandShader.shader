// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/HandShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_HiddenColor("Hidden Color", Color) = (1,1,1,1)
		_HiddenTex("Hidden Holo Tex (RGB)", 2D) = "white" {}
		_HiddenEdgeColor("Hidden Edge Color", Color) = (1,1,1,1)
	}
		SubShader{
			//Draw occluded hand geometry
			Tags 
			{ 
			"Queue" = "Transparent" 
			"RenderType" = "Transparent" 
			}

			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma surface surf Unlit alpha:fade
			#pragma target 3.0


			half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
			{
				return half4(s.Albedo, s.Alpha);
			}

			struct Input {
				float2 uv_HiddenTex;
				float4 screenPos;
				float3 worldPos;
				float3 viewDir;
				float eyeDepth;
			};

			half _Glossiness;
			half _Metallic;
			sampler2D _CameraDepthTexture;
			fixed4 _HiddenColor;
			sampler2D _HiddenTex;
			fixed4 _HiddenEdgeColor;

			void surf(Input IN, inout SurfaceOutput o) {
				float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos)));
				float surfZ = -mul(UNITY_MATRIX_V, float4(IN.worldPos.xyz, 1)).z;
				float diff = surfZ - sceneZ;
				float edge = max(0.99999 - dot(o.Normal, IN.viewDir), 0);

				//Add edge glow to base color
				fixed4 baseCol = lerp(tex2D(_HiddenTex, IN.uv_HiddenTex) * _HiddenColor, _HiddenEdgeColor, min(pow(edge, 2), 0.8));
				//Add glow at intersection with geometry
				fixed4 col = baseCol;
				o.Albedo = col.rgb;
				o.Alpha = col.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}