Shader "PHNK/ColorGun" {

	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_HiddenTex("Hidden texture", 2D) = "black" {}
		/*_HiddenTexRed("Hidden texture red", 2D) = "red" {}
		_HiddenTexBlue("Hidden texture blue", 2D) = "blue" {}
		_HiddenTexGreen("Hidden texture green", 2D) = "green" {}
		_HiddenTexYellow("Hidden texture yellow", 2D) = "yellow" {}
		_HiddenTexBlack("Hidden texture black", 2D) = "black" {}*/
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
				/*float2 uv_HiddenTexRed;
				float2 uv_HiddenTexBlue;
				float2 uv_HiddenTexGreen;
				float2 uv_HiddenTexYellow;
				float2 uv_HiddenTexBlack;*/
				float3 worldPos;
			};

			sampler2D _MainTex;
			sampler2D _HiddenTex;
			/*sampler2D _HiddenTexRed;
			sampler2D _HiddenTexBlue;
			sampler2D _HiddenTexGreen;
			sampler2D _HiddenTexYellow;
			sampler2D _HiddenTexBlack;*/
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

			float3 getTex(int colorNum) {
				float3 newColor;
				switch (colorNum) {
					case 0: //Red
						/*fixed4 HiddenTexRed = tex2D(_HiddenTexRed, IN.uv_HiddenTexRed);
						newColor = HiddenTexRed;*/
						newColor = float3(255, 0, 0);
						break;
					case 1: //Blue
						/*fixed4 HiddenTexBlue = tex2D(_HiddenTexBlue, IN.uv_HiddenTexBlue);
						newColor = HiddenTexBlue;*/
						newColor = float3(0, 0, 255);
						break;
					case 2: //Green
						/*fixed4 HiddenTexGreen = tex2D(_HiddenTexGreen, IN.uv_HiddenTexGreen);
						newColor = HiddenTexGreen;*/
						newColor = float3(0, 255, 0);
						break;
					case 3: //Yellow
						/*fixed4 HiddenTexYellow = tex2D(_HiddenTexYellow, IN.uv_HiddenTexYellow);
						newColor = HiddenTexYellow;*/
						newColor = float3(255, 255, 0);
						break;
					default:
						/*fixed4 HiddenTexBlack = tex2D(_HiddenTexBlack, IN.uv_HiddenTexBlack);
						newColor = HiddenTexBlack;*/
						newColor = float3(0, 0, 0);
						break;
				}
				return newColor;
			}
			
			bool compareColors() {
				bool sameColor = false;
				if (_RayColor == _OwnColor) {
					sameColor = true;
				}
				return sameColor;
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
				o.Albedo.rgb = lerp(mainColour, getTex(_RayColor), d);
				//o.Albedo = lerp(mainColour, getTex(_RayColor, IN), d);
				if (compareColors()) {
					o.Albedo = lerp(o.Albedo, hiddenColour, d);
					//o.Albedo = lerp(mainColour, hiddenColour, d);
				}
				//o.Albedo = lerp(mainColour, hiddenColour, d);

			}
			ENDCG
		}
			Fallback "Diffuse"
}