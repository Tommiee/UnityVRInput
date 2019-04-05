// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PHNK/ImageDisplacement"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PaintTex("Texture paint", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0,0.01)) = 1
		
		_CrackTex ("Cracks", 2D) = "white" {}
        _CrackColor ("Cracks Color", Color) = (0.5, 0.5, 0.5, 1)
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				half3 worldNormal : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _DisplaceTex;
			sampler2D _PaintTex;
			sampler2D _CameraDepthTexture;
			float _Magnitude;
			
			sampler2D _CrackTex;
            float3 _CrackColor;
            half _Cutoff;
            
            
			float4 frag (v2f i) : SV_Target
			{
				float2 distuv = i.uv;

                float3 depth = tex2D(_CameraDepthTexture, i.uv);

				float2 disp = tex2D(_DisplaceTex, distuv * 3).xy;
				float zMultiplier = 2 * (depth.r * depth.r * depth.r);
				float2 paintUV = distuv * 2;
				paintUV *= zMultiplier;
				float3 paint = tex2D(_PaintTex, paintUV);
				disp = ((disp * 2) - 1) * _Magnitude;
				
				fixed4 crackTex = tex2D(_CrackTex, distuv * 3 );
                float crackVisibility = saturate((crackTex.a - _Cutoff ) * 10);
                
				float4 col = tex2D(_MainTex, i.uv + disp);
    			
    			float contrast = 1.1;
    			float3 contrastedColour = ((col.rgb - 0.5f) * contrast) + 0.5f;
				//col.rgb = lerp(col.rgb, contrastedColour, paint.r);
				
				float3 crack = col * (1- crackTex.rgb * _CrackColor);
								
				// grayscale
				float3 grayScale = (col.r + col.g + col.b) / 3;
				
				
				float m = max(sign(depth.r - 0.7), 0);
				grayScale *= grayScale * 1.5; // darken world
				// grayscale end
                
                
                // simple blur
                half4 sum = half4(0,0,0,0);
                #define GRABPIXEL(weight,kernelx) tex2D( _MainTex, float2(i.uv.x + (kernelx/_ScreenParams.x), i.uv.y)) * weight
                sum += GRABPIXEL(0.05, -4.0);
                sum += GRABPIXEL(0.09, -3.0);
                sum += GRABPIXEL(0.12, -2.0);
                sum += GRABPIXEL(0.15, -1.0);
                sum += GRABPIXEL(0.18,  0.0);
                sum += GRABPIXEL(0.15, +1.0);
                sum += GRABPIXEL(0.12, +2.0);
                sum += GRABPIXEL(0.09, +3.0);
                sum += GRABPIXEL(0.05, +4.0);
                sum.rgb = (sum.r + sum.g + sum.b)/3;
                sum.rgb *= sum.rgb * 1.2;
                // simple blur end
                   
                //col.rgb = lerp(col.rgb, grayScale, m);// grayScale render
                //col.rgb = lerp(col.rgb, crack, crackVisibility); // default render
                //col.rgb = lerp(col.rgb, sum.rgb, m);
                
                
                
				return col;
			}
			ENDCG
		}
	}
}