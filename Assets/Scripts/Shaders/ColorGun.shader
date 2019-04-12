Shader "PHNK/ColorGun" {

	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_HiddenTex("Hidden texture", 2D) = "white" {}
		_DClipOff("Dot Product clipoff", Range(0, 1)) = 0.4
		_RayPosition("Ray Position", Vector) = (0, 0, 0, 0)
		_RayDirection("Ray Direction", Vector) = (0, 0, 0, 0)
		_RayColor("Ray Color", Int) = 0
		_OwnColor("Own Color", Int) = 0
		_SmoothFalloff("Smoothing", Range(0, 1)) = 0.2
	}

		SubShader
		{
			Tags {  "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Standard
			struct Input {
				float2 uv_MainTex;
				float2 uv_HiddenTex;
				float3 worldPos;
			};

			sampler2D _MainTex;
			sampler2D _HiddenTex;
			float _DClipOff;
			float _SmoothFalloff;
			float3 _RayPosition;
			float3 _RayDirection;
			int _RayColor;
			int _OwnColor;

			float easein(float start, float end, float value)
			{
				end -= start;
				return end * value * value * value * value + start;
			}

			float3 getColor(int colorNum) {
				float3 newColor;
				switch (colorNum) {
					case 0: //Red
						newColor = float3(255, 0, 0);
						break;
					case 1: //Blue
						newColor = float3(0, 0, 255);
						break;
					case 2: //Green
						newColor = float3(0, 255, 0);
						break;
					case 3: //Yellow
						newColor = float3(255, 255, 0);
						break;
					default:
						newColor = float3(0, 0, 0);
						break;
				}
				return newColor;
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed4 mainColour = tex2D(_MainTex, IN.uv_MainTex);
				fixed4 hiddenColour = tex2D(_HiddenTex, IN.uv_HiddenTex);

				float3 rayGunDir = _RayPosition.xyz - IN.worldPos;
				float distance = length(rayGunDir);
				rayGunDir = normalize(rayGunDir);
				float d = dot(rayGunDir, _RayDirection) * -1;
				float dot = d;
				
				d = 1 - ((1 - dot) / (1 - _DClipOff*_SmoothFalloff));
				
				d = easein(0, 1, max(d, 0));
				o.Albedo.rgb = lerp(mainColour, getColor(_RayColor), d);
				//o.Albedo = lerp(mainColour, hiddenColour, d);

			}
			ENDCG
		}
			Fallback "Diffuse"
}