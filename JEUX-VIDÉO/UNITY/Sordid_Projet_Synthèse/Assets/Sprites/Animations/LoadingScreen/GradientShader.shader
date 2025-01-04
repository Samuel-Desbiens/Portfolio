Shader "Custom/HorizontalGradientWithTexture"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {} // Main texture property
        _LeftColor ("Left Color", Color) = (1,1,1,1)
        _RightColor ("Right Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        
        Pass
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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex; // Main texture sampler

            float4 _LeftColor;
            float4 _RightColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the main texture
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Calculate the gradient color
                float t = i.uv.x;
                fixed4 gradientColor = lerp(_LeftColor, _RightColor, t);

                // Multiply texture color with gradient color
                fixed4 finalColor = texColor * gradientColor;

                return finalColor;
            }
            ENDCG
        }
    }
}
