Shader "Hidden/RadialBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _SampleCount("Sample Count", Range(4, 16)) = 8
        _Strength("Strength", Range(0.0, 1.0)) = 0.5
    }
        SubShader
        {
            Cull Off
            ZWrite Off
            ZTest Always

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex   : POSITION;
                    float2 uv       : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv       : TEXCOORD0;
                    float4 vertex   : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                sampler2D _MainTex;
                half _SampleCount;
                half _Strength;

                fixed4 frag(v2f i) : SV_Target
                {
                    half4 col = 0;
                    // UV��-0.5�`0.5�ɕϊ�
                    half2 symmetryUv = i.uv - 0.5;
                    // �O���ɍs���قǂ��̒l���傫���Ȃ�(0�`0.707)
                    half distance = length(symmetryUv);
                    for (int j = 0; j < _SampleCount; j++) {
                        // j���傫���قǁA��ʂ̊O���قǏ������Ȃ�l
                        float uvOffset = 1 - _Strength * j / _SampleCount * distance;
                        // j���傫���Ȃ�ɂ�Ă������̃s�N�Z�����T���v�����O���Ă���
                        // �܂���ʂ̊O���قǂ������̃s�N�Z�����T���v�����O����
                        col += tex2D(_MainTex, symmetryUv * uvOffset + 0.5);
                    }
                    col /= _SampleCount;
                    return col;
                }

                ENDCG
            }
        }
}