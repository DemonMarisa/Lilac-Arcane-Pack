sampler uImage0 : register(s0);

// 完全自定义的清晰参数
float2 uCenter; // 爆炸中心 (屏幕 UV 空间: 0.0 到 1.0)
float uRadius; // 冲击波当前的扩散半径 (0.0 到 1.5)
float uWidth; // 冲击波环带的宽度 (建议 0.05 到 0.2)
float uIntensity; // 扭曲强度 (建议 0.02 到 0.1)
float uFrequency; // 环内波纹的精细频率 (例如 2.0 或 3.0)
float2 uScreenResolution; // 屏幕分辨率
bool useSaturate; // 是否使用 saturate 函数来限制波纹幅度 (true 或 false)
bool UseChromaticAberration; // 是否启用色散效果 (true 或 false)

float4 MainPS(float2 coords : TEXCOORD0) : COLOR0
{
    // 修正长宽比
    float aspect = uScreenResolution.x / uScreenResolution.y;
    float2 uvAspect = coords - uCenter;
    uvAspect.x *= aspect;
    // 计算当前像素到冲击波中心的距离和方向
    float dist = length(uvAspect);
    // 计算方向向量并归一化
    float2 dir = normalize(uvAspect);
    // 将方向向量还原回非平铺的屏幕 UV 空间，确保推挤方向正确
    dir.x /= aspect;
    // 构建两端平滑衰减的环形遮罩
    float diff = dist - uRadius;
    float mask = smoothstep(uWidth, 0.0, abs(diff));
    // 如果完全在圆环之外，直接返回原图，省去后续计算
    if (mask <= 0.0)
        return tex2D(uImage0, coords);
    // 构建双向正弦波
    // 产生一个完整的波峰和波谷，形成高光折射与内凹质感
    float wave = sin(diff * (6.283185 * uFrequency) / uWidth) * mask;
    if (useSaturate)
        wave = saturate(sin(diff * (6.283185 * uFrequency) / uWidth) * mask);
    // 色散效果 (Chromatic Aberration)
    // 模拟光线穿过玻璃透镜时，不同波长的光（红、绿、蓝）折射率不同的物理现象
    float2 distortOffset = dir * wave * uIntensity;
    if (UseChromaticAberration)
    {
        // 给红蓝绿通道分别赋予微弱的偏移权重系数
        float2 offsetR = distortOffset * 1.15;
        float2 offsetG = distortOffset * 1.00;
        float2 offsetB = distortOffset * 0.85;
        // 分离通道采样屏幕
        float r = tex2D(uImage0, coords - offsetR).r;
        float g = tex2D(uImage0, coords - offsetG).g;
        float b = tex2D(uImage0, coords - offsetB).b;
        float a = tex2D(uImage0, coords - offsetG).a;
        return float4(r, g, b, a);
    }
    else
        return tex2D(uImage0, coords - distortOffset);
}

technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}