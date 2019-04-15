Shader "PHNK/VermeerLighting" {
        
        Properties 
        {
            _MainTex ("Texture", 2D) = "white" {}
            _RoughnessTex ("Texture Roughness", 2D) = "white" {}
            _BumpTex ("Onze normal map", 2D) = "bump" {}
            _BumpTex2 ("Onze 2e normal map", 2D) = "bump" {}
            _GlossinessTex ("Onze glossiness map", 2D) = "bump" {}
		    _BumpMultiplier ("Bump Multiplier", Range(0.0001,5)) = 1
		    _BumpMultiplier2 ("Bump Multiplier 2", Range(0.0001,5)) = 1
		    _RoughnessMultiplier ("Roughness Multiplier", Range(0.0001,5)) = 1
		    _Specular ("Specular level", Range(0.0001,5)) = 1
		    _Contrast ("Contrast level", Range(0.8,1.4)) = 1
		    _DiffuseVal ("Diffuse val", Range(0.01,3)) = 1
		    _ShadowColor ("Schadow color", Color) = (1,1,1,1)
		    _PaintDetail ("Detail", 2D) = "gray" {}
        }
        
        SubShader 
        {
            Tags { "RenderType" = "Opaque" }
            
            CGPROGRAM
            #pragma surface surf VermeerSpecular
            #pragma target 3.0
                        
            fixed4 _ShadowColor;
            half _DiffuseVal;
            sampler2D _MainTex;
            sampler2D _BumpTex;
            sampler2D _BumpTex2;
            sampler2D _RoughnessTex;
            sampler2D _GlossinessTex;
            sampler2D _PaintDetail;
		    half _BumpMultiplier;
		    half _BumpMultiplier2;
		    half _BumpMultiplier3;
		    half _RoughnessMultiplier;
		    half _Specular;
		    half _Contrast;
            
            half4 LightingVermeerSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
                half3 h = normalize (lightDir + viewDir);
        
                half diff = max (0, dot (s.Normal, lightDir));
        
                float nh = max (0, dot (s.Normal, h));
                float spec = min(1, pow (nh, 40.0)) * s.Specular;
        
                half4 c;
                c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
                c.rgb -= _ShadowColor.xyz * max(0.0,(1.0-(diff*atten))) * _DiffuseVal;
                c.a = s.Alpha;
                return c;
            }
            
            struct Input {
                float2 uv_MainTex;
                float2 uv_BumpTex;
                float2 uv_BumpTex2;
                float2 uv_RoughnessTex;
                float4 screenPos;
            };
            
            void surf (Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			    c.rgb = ((c.rgb - 0.5f) * max(_Contrast, 0)) + 0.5f;
                fixed g = tex2D (_GlossinessTex, IN.uv_MainTex).r;
                fixed s = tex2D (_RoughnessTex, IN.uv_RoughnessTex).r * g * _RoughnessMultiplier;

                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                screenUV *= float2(8,6);

                o.Albedo = c.rgb;
                o.Albedo *= (tex2D (_PaintDetail, screenUV).rgb / 3 + 0.67);

    
                float3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
                float3 n2 = UnpackNormal(tex2D(_BumpTex2, IN.uv_BumpTex2));
			    n = float3(n.x * _BumpMultiplier + n2.x * _BumpMultiplier2, n.y * _BumpMultiplier + n2.y * _BumpMultiplier2, n.z);
			    
			    o.Normal = normalize(n);
			    o.Specular = _Specular * s;
            }
            ENDCG
        }
        Fallback "Diffuse"
    }