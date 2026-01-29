Shader "Custom/UI/MaskTransparency"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _MaskTex ("Mask", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MaskTex;
            float4 _Color;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float mask = tex2D(_MaskTex, i.uv).r; // берем красный канал маски
                fixed4 col = _Color;
                col.a *= 1.0 - mask; // инвертируем маску: белое → прозрачное
                return col;
            }
            ENDCG
        }
    }
}
