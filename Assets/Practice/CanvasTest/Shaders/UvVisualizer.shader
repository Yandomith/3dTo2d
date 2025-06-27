Shader "Custom/CheckerboardUV"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TileSize("Tile Size", Float) = 10.0
        _Aspect("Aspect Ratio", Float) = 1.0
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
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _TileSize;
            float _Aspect;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.x *= _Aspect; // correct X for aspect ratio

                float2 tiled = uv * _TileSize;
                float checker = fmod(floor(tiled.x) + floor(tiled.y), 2.0);
                return checker < 1.0 ? fixed4(0, 0, 0, 1) : fixed4(1, 1, 1, 1);
            }
            ENDCG
        }
    }
}
    