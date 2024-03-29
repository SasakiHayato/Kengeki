Shader "Custom/GrayScale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Strength ("Strength", Range(0, 1)) = 0.5
    }

    SubShader
    {
        PASS
        { 
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            };

            sampler2D _MainTex;
            half _Strength;

            // NTSC�W��
            // col.r * 0.298912 + col.g * 0.586611 + col.b * 0.114478; 

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                half s = _Strength;

                fixed v = col.r * 0.298912 + col.g * 0.586611 + col.b * 0.114478;

                half r = lerp(col.r, v, s);
                half g = lerp(col.g, v, s);
                half b = lerp(col.b, v, s);

                return fixed4(r, g, b, col.a);
            };

            ENDCG
        }
    }
}
