Shader "UI/HighlightMask_PixelPerfect"
{
    Properties
    {
        _Color ("Dim Color", Color) = (0,0,0,0.75)
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _MaskCenter ("Mask Center (px)", Vector) = (960,540,0,0)
        _MaskRadius ("Mask Radius (px)", Float) = 100
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float4 vertex : SV_POSITION; float2 uv : TEXCOORD0; float4 pos : TEXCOORD1; };

            fixed4 _Color;
            sampler2D _MaskTex;
            float4 _MaskCenter;
            float _MaskRadius;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.pos = o.vertex;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Конвертируем координаты clip space в пиксели
                float2 pixelPos = float2(i.pos.x / i.pos.w * 0.5 + 0.5, i.pos.y / i.pos.w * 0.5 + 0.5);
                pixelPos.x *= _ScreenParams.x;
                pixelPos.y *= _ScreenParams.y;

                // Расстояние до центра маски в пикселях
                float dist = distance(pixelPos, _MaskCenter.xy);

                // Используем радиус маски
                float t = saturate(dist / _MaskRadius);

                // Применяем текстуру маски (градиент)
                fixed maskVal = tex2D(_MaskTex, float2(t,0.5)).r;
                maskVal = 1.0 - maskVal;

                fixed4 col = _Color;
                col.a *= maskVal;
                return col;
            }
            ENDCG
        }
    }
}
