Shader "Custom/EnemyRedEmissive"
{
    Properties
    {
        _BaseColor("Color", Color) = (1,1,1,1)
        _EmissionColor("Emission Color", Color) = (1,1,1,1)
    }
        SubShader
    {
        Tags { "Queue" = "Geometry" }

        CGPROGRAM
        #pragma surface surf Lambert

        float4 _EmissionColor;
        float4 _BaseColor;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _BaseColor.rgb;
            o.Emission = _EmissionColor.rgb;

        }
        ENDCG
    }
        FallBack "Diffuse"
}
