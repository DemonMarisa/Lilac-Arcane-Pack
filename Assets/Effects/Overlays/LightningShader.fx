sampler source : register(s0);
sampler noise : register(s1);
sampler distortion : register(s2);

float2 uvMullt;
float2 uvAdd;
float2 noiseMult;
float2 noiseAdd;
float fade;
float distortionMult;

float uFadeoutLeftLength;
float uFadeinRigtLength;

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 Lightning(VertexShaderOutput input) : COLOR
{
    float4 noiseColor = tex2D(noise, input.TextureCoordinates * noiseMult + noiseAdd);
    float4 distortionColor = tex2D(distortion, input.TextureCoordinates * uvMullt + uvAdd);
    // 羽化溶解
    float mask = smoothstep(fade - 0.15, fade + 0.05, noiseColor.r);
    float2 distortionOffset = (distortionColor.rg - 0.5) * distortionMult;
    float4 baseColor = tex2D(source, input.TextureCoordinates * uvMullt + uvAdd + distortionOffset);
    
    float fadeInL = smoothstep(0.0, uFadeoutLeftLength, input.TextureCoordinates.x);
    float fadeOutR = smoothstep(1.0, 1.0 - uFadeinRigtLength, input.TextureCoordinates.x);

    baseColor.a *= fadeInL * fadeOutR;
    return baseColor * input.Color * mask;
}

technique Technique1
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 Lightning();
    }
}