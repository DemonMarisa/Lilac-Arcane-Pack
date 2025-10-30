// 传入的由粒子拼成的通道材质
sampler InPutTexture : register(s0);
sampler uDisplacementSampler : register(s1); // 我们的噪声置换图
float uTime; // 时间变量，用于让火焰动起来
float uIntensity; // 扭曲强度
float4 uColor; // 染色颜色

float4 DisplacementFunction(float2 coords : TEXCOORD0) : COLOR0
{
    // 不能没有颜色就抛弃，因为本质上是每个像素根据偏移坐标去采样原坐标
    // 让噪声图动起来
    // 通过加上 uTime 来滚动噪声图的采样坐标
    float2 noiseUV = float2(coords.x, coords.y - uTime * 0.1);
    // 从噪声图中采样，我们只需要一个通道的值（比如 .r 红色通道）
    float displacement = tex2D(uDisplacementSampler, noiseUV).r;
    // 根据噪声图的R通道进行置换
    // 乘以 uIntensity 来控制扭曲的强度
    float2 offset = float2(0, displacement * uIntensity);
    // 将计算出的偏移量应用到原始贴图的 UV 坐标上
    float2 displacedUV = coords + offset;
    // 使用被扭曲后的 UV 坐标来采样原始贴图
    // 采样原始的、清晰的扭曲后贴图
    float4 originalColor = tex2D(InPutTexture, displacedUV);
    // 最终进行染色
    float4 OutPutColor = originalColor * uColor;
    return OutPutColor;
}

// 最终通道
technique Technique1
{
    pass LPADisplacementPass
    {
        PixelShader = compile ps_2_0 DisplacementFunction();
    }
}