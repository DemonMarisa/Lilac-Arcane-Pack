sampler uImage0 : register(s0);
float2 uScreenSize;
float2 uTargetCenter; // 屏幕像素坐标中心
float uDistortionRadius; // (0-1 UV空间)
float uDistortionStrength; 
float uLerpFactor; // 用于淡入淡出 1是原图，0是完全扭曲后
float2 uAspectRatioCorrection; // 对应 ScreenScale (用于修正非正方形像素比)

float4 MainPS(float2 coords : TEXCOORD0) : COLOR0
{
    float2 pos = uTargetCenter / uScreenSize;
    float2 offset = (coords - pos);
    float dist = length(offset * uAspectRatioCorrection);
    if (dist > uDistortionRadius)
    {
        return tex2D(uImage0, coords);
    }
    else
    {
        float DisOffset = lerp(1, 0, dist / uDistortionRadius);
        float2 FinalOffset = offset * uDistortionStrength;
        float2 FinalUV = pos + FinalOffset * DisOffset;
        float2 FinalLerp = lerp(FinalUV, coords, uLerpFactor);
        return tex2D(uImage0, FinalLerp);
    }
}

technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}
