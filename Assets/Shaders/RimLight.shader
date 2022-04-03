Shader "Custom/Rim Light"
{
    Properties
    {
        _RimColor("Rim Color", Color) = (0,0.5,0.5,0.0)
        _BaseColor("Base Color", Color) = (0,0,0,0)
        _Intensity("Rim Intensity", Range(0.1,8.0)) = 3.0
        _RimCutoff("Rim Cutoff", Range(0,2)) = 0
    }

     SubShader
    {

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float3 viewDir;
        };

        float4 _RimColor;
        float _Intensity;
        float _RimCutoff;
        float4 _BaseColor;

        void surf(Input IN, inout SurfaceOutput o)
        {
            half rim = 1-saturate(dot(normalize(IN.viewDir), o.Normal));
            half rimMult = rim > _RimCutoff ? rim : 0;
            o.Emission = _RimColor.rgb * pow(rimMult, (1/_Intensity));
            o.Albedo = _BaseColor.rgb;
        }

        ENDCG
    }

    Fallback "Diffuse"
}
