sampler source : register(s0); // 主纹理
float uRotBy;
float4 Cut;

float4 MainPS(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    if (coords.x < Cut.x)
        return float4(0, 0, 0, 0);
    else if (coords.x > Cut.y)
        return float4(0, 0, 0, 0);
    else if (coords.y < Cut.z)
        return float4(0, 0, 0, 0);
    else if (coords.y > Cut.w)
        return float4(0, 0, 0, 0);
    float s, c;
    sincos(uRotBy, s, c);
    coords -= 0.5;
    coords = float2
    (
        coords.x * c - coords.y * s,
        coords.x * s + coords.y * c
    );
    coords += 0.5;
    float4 finalColor = tex2D(source, coords);
    return finalColor * sampleColor;
}
technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}