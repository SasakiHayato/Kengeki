// �Q�� https://shibuya24.info/entry/unity-shader-noise

Shader "Custom/Noise"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    // 0 ~ 1
    float _HorizonValue = 1;
    // �����V�[�h
    int _Seed;

    // ��������
    // http://neareal.net/index.php?ComputerGraphics%2FHLSL%2FCommon%2FGenerateRandomNoiseInPixelShader
    float rnd(float2 value, int Seed)
    {
        return frac(sin(dot(value.xy, float2(12.9898, 78.233)) + Seed) * 43758.5453);
    }

    fixed4 frag(v2f_img i) : SV_Target
    {
        float rndValue = rnd(i.uv, _Seed);
        // -1 or 1 ���E�ǂ���ɂ���邩�����_���ɂ���
        int tmp = step(rndValue, 0.5) * 2 - 1;
        // �s�N�Z���W�����v�l
        float rndU = _HorizonValue * tmp * rndValue;
        float2 uv = float2(frac(i.uv.x + rndU), i.uv.y);
        fixed4 col = tex2D(_MainTex, uv);
        return col;
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}