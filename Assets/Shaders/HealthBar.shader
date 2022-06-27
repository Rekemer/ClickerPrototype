Shader "Custom/HealthBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorLowHp ("ColorLowHp", Color) = (1,0,0,1)
        _ColorHighHp ("ColorHighHp", Color) = (0,1,0,1)
        _Health ("health",Range(0,1)) = 1
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
          
            #include "UnityCG.cginc"
            float4 _ColorLowHp;
            float4 _ColorHighHp;
            float _Health;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 outputColor = lerp(_ColorLowHp,_ColorHighHp,_Health);
                float4 bgColor = (0,0,0,0);
                float t = _Health > i.uv.x;
                float4 outColor = lerp(bgColor,outputColor,t );
                return outColor;
            }
            ENDCG
        }
    }
}
