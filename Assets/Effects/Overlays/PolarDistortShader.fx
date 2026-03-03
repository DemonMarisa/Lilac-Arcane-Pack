sampler source : register(s0); // 主纹理
sampler alphaCut : register(s1); // 主纹理
float uXTime; // 游戏时间，用于动画
float uYTime; // 游戏时间，用于动画
float uRingMult; // 纵向的拼贴倍数
float uWidthMult; // 横向的拼贴倍数

float4 MainPS(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 baseColor = tex2D(alphaCut, coords);
    if (!any(baseColor))
        discard;
    float2 vectorFromCenter = coords - 0.5;
    float angleFromCenter = atan2(vectorFromCenter.y, vectorFromCenter.x);
    float angle = (angleFromCenter / (2.0 * 3.14159265)) + 0.5;
    float horizontal = frac(angle * uWidthMult + uXTime);
    float dist = distance(coords, 0.5);
    float radial = frac(dist * uRingMult - uYTime);
    float2 polar = float2(horizontal, radial);
    float4 finalColor = tex2D(source, polar);
    float4 OutputColor = finalColor.r * sampleColor * baseColor.a;
    return OutputColor;
}

technique Technique1
{
    pass LAPPolarDistortPass
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}