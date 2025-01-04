Shader "Custom/StrictBlackWhite"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {} // Main texture property
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
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
                
                // Check if the pixel is colored (not transparent)
                float isColored = texColor.a > 0 ? 1.0 : 0.0;

                // Convert color to grayscale only for colored pixels
                float grayValue = dot(texColor.rgb, float3(0.299, 0.587, 0.114)) * isColored;
                fixed4 grayColor = fixed4(grayValue, grayValue, grayValue, texColor.a);

                return grayColor;
            }
            ENDCG
        }
    }
}
