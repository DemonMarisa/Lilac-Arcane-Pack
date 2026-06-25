sampler source : register(s0);
sampler noise : register(s1);
float2 uvMullt;
float2 uvAdd;
float2 noiseMult;
float2 noiseAdd;
float fade;

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 Lightning(VertexShaderOutput input) : COLOR
{
    float4 baseColor = tex2D(source, input.TextureCoordinates * uvMullt + uvAdd);
    float4 noiseColor = tex2D(noise, input.TextureCoordinates * noiseMult + noiseAdd);
    if (noiseColor.r < fade)
        return baseColor * input.Color;
    else
        return float4(0, 0, 0, 0);

}

technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 Lightning();
    }
}