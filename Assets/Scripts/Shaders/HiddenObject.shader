Shader "PHNK/HiddenObject" {

	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DClipOff("Dot Product clipoff", Range(0, 1)) = 0.4
		_RayPosition("Ray Position", Vector) = (0, 0, 0, 0)
		_RayDirection("Ray Direction", Vector) = (0, 0, 0, 0)
		_SmoothFalloff("Smoothing", Range(0,1)) = 0.2
	}

		SubShader
		{
			Tags {  "RenderType" = "Transparent"
					"Queue" = "Transparent"
			}
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma surface surf Standard alpha:fade

			struct Input {
				float2 uv_MainTex;
				float3 worldPos;
			};

			sampler2D _MainTex;
			float _InputFlashlight;
			float _DClipOff;
			float3 _RayPosition;
			float3 _RayDirection;
			float _SmoothFalloff;

			float easein(float start, float end, float value)
			{
				end -= start;
				return end * value * value * value * value + start;
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed4 mainColour = tex2D(_MainTex, IN.uv_MainTex);

				float3 rayGunDir = _RayPosition.xyz - IN.worldPos;
				float distance = length(rayGunDir);
				rayGunDir = normalize(rayGunDir);
				float d = dot(rayGunDir, _RayDirection) * -1;
				float dot = d;
				
				d = 1 - ((1 - dot) / (1 - _DClipOff * _SmoothFalloff));
				
				d = easein(0, 1, max(d, 0));
				o.Albedo = mainColour;
				o.Alpha = d;// lerp(0, 1, d);
			}
			ENDCG
		}
			Fallback "Diffuse"
}