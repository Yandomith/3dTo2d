Shader "Custom/SimpleDrawBrush"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {} // what's already drawn
        _BrushTex ("Brush Texture", 2D) = "white" {} // circular brush image
        _BrushColor ("Brush Color", Color) = (1,1,1,1)
        _BrushUV ("Brush UV", Vector) = (0,0,1,1) // brush rect in UV space
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _BrushTex;
            float4 _BrushColor;
            float4 _BrushUV;

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
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Sample current canvas color
                fixed4 baseColor = tex2D(_MainTex, uv);

                // Calculate brush UV relative to brush rectangle
                float2 brushUVraw = (uv - _BrushUV.xy) / _BrushUV.zw;

                // Discard pixels outside brush rect
                if (brushUVraw.x < 0.0 || brushUVraw.y < 0.0 || brushUVraw.x > 1.0 || brushUVraw.y > 1.0)
                    return baseColor;

                // Sample brush texture
                fixed4 brush = tex2D(_BrushTex, brushUVraw);

                // Blend brush color over base color using brush alpha
                return lerp(baseColor, _BrushColor, brush.a * _BrushColor.a);
            }
            ENDCG
        }
    }
}
