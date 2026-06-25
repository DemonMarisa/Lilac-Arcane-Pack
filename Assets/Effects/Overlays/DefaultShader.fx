sampler InPutTexture : register(s0);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 Main(VertexShaderOutput input) : COLOR
{
    float4 baseColor = tex2D(InPutTexture, input.TextureCoordinates);
    return baseColor * input.Color;
}

technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 Main();
    }
}