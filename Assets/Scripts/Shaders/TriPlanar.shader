// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PHNK/Triplanar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AlbedoTex ("Albedo Texture", 2D) = "white" {}
        _Tiling ("Tiling", Float) = 1.0
        _OcclusionMap("Occlusion", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                half3 objNormal : TEXCOORD0;
                float3 coords : TEXCOORD1;
                float2 uv : TEXCOORD2;
                half3 worldNormal : TEXCOORD3;
                float4 pos : SV_POSITION;
                float3 worldPos : TXCOORD4;
            };

            float _Tiling;

            v2f vert (float4 pos : POSITION, float3 normal : NORMAL, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(pos);
                o.worldPos = mul(unity_ObjectToWorld, pos).xyz;
                o.coords = pos.xyz * _Tiling;
                o.objNormal = normal;
                o.worldNormal = UnityObjectToWorldNormal(normal);
                o.uv = uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _AlbedoTex;
            sampler2D _OcclusionMap;
            
            fixed4 frag (v2f i) : SV_Target
            {
                // use absolute value of normal as texture weights
                half3 blend = abs(i.objNormal);
                // make sure the weights sum up to 1 (divide by sum of x+y+z)
                blend /= dot(blend,1.0);
                // read the three texture projections, for x,y,z axes
                float tiling = 3;
                fixed4 cx = tex2D(_MainTex, i.worldPos.yz / tiling);
                fixed4 cy = tex2D(_MainTex, i.worldPos.xz / tiling);
                fixed4 cz = tex2D(_MainTex, i.worldPos.xy / tiling);
                fixed4 c = tex2D(_AlbedoTex, i.uv);
                // blend the textures based on weights
                //c += cx * blend.x + cy * blend.y + cz * blend.z;
                //c = c / 4;
                
                c *= cz;
                return c;
            }
            ENDCG
        }
    }
}