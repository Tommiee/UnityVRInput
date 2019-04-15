Shader "PHNK/DayNight" {
    
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _SecondTex ("Lighting Color Gradient", 2D) = "white"{}
		_LightTex("The amount of light", 2D) = "white"{}
        _SamplePos ("Position In Gradient", Range(0,1)) = 0.25
    }
    
    SubShader {
    
        Pass {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            uniform sampler2D _SecondTex;
            uniform sampler2D _LightTex;
            
            float _SamplePos;

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
            
            float4 frag(v2f i) : COLOR {
                float2 uv = float2(_SamplePos,0);
                float4 c = tex2D(_MainTex, i.uv);
                float4 g = tex2D(_SecondTex, uv);
				float intensity = tex2D(_LightTex, uv);
                
                float4 result = c * g * intensity;
                return result;
            }
            
            ENDCG
        }
    } 
}
