sampler source : register(s0);
float uIntensity; // 混合强度

float4 OverlayPass(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 baseColor = tex2D(source, coords);
    float3 base = baseColor.rgb;
    float3 blend = sampleColor;

    float3 overlay = float3(0, 0, 0);
    for (int i = 0; i < 3; i++)
    {
        if (base[i] <= 0.5)
            overlay[i] = 2.0 * base[i] * blend[i];
        else
            overlay[i] = 1.0 - 2.0 * (1.0 - base[i]) * (1.0 - blend[i]);
    }

    float3 finalColor = lerp(base, overlay, uIntensity);
    return float4(finalColor, baseColor.a);
}

technique OverlayTechnique
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 OverlayPass();
    }
}