Shader "Custom/Magnification"
{
	Properties
	{
		_Magnification("Magnification", Float) = 1
		_MagnificationHeightmap("Magnification Heightmap", 2D) = "white" {}
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" "PreviewType" = "Plane" }
		LOD 100

		GrabPass{ "_GrabTexture" }

		Pass
			{
				ZTest On
				ZWrite Off
				Blend One Zero
				Lighting Off
				Fog{ Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				//our UV coordinate on the GrabTexture
				float4 uv : TEXCOORD0;

				float2 uv_object : TEXCOORD1;
				//our vertex position after projection
				float4 vertex : SV_POSITION;
			};

			sampler2D _MagnificationHeightmap;
			sampler2D _GrabTexture;
			half _Magnification;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//the UV coordinate of our object's center on the GrabTexture
				float4 uv_center = ComputeGrabScreenPos(UnityObjectToClipPos(float4(0, 0, 0, 1)));
				//the vector from uv_center to our UV coordinate on the GrabTexture
				float4 uv_diff = ComputeGrabScreenPos(o.vertex) - uv_center;
				//apply magnification
				uv_diff /= _Magnification;
				//save result
				o.uv = uv_center + uv_diff;

				o.uv_object = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				float3 disp = tex2D(_MagnificationHeightmap, i.uv_object).rgb;
				i.uv.y *= 1 - disp.r;
				i.uv.x *= 1 - disp.b;

				return tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv));
			}
			ENDCG
		}
	}
}