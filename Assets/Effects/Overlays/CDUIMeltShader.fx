sampler InPutTexture : register(s0); // 背景材质
sampler AlphaTexture : register(s1); // 用于裁切的灰度图
float2 NoiseTextureSize;// 灰度图材质修改
float progress; // 抛弃像素的进度
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};
float4 Main(VertexShaderOutput input) : COLOR
{
    float4 baseColor = tex2D(InPutTexture, input.TextureCoordinates);
    if (!any(baseColor))
        discard;
    float4 NoiseColor = tex2D(AlphaTexture, input.TextureCoordinates * NoiseTextureSize);
    float NoiseR = NoiseColor.r;
    if (NoiseR > progress)
        return float4(0, 0, 0, 0);
    else
        return baseColor * input.Color;
}
// 最终通道
technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_2_0 Main();
    }
}