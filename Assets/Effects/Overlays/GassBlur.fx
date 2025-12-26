// 用于Alpha裁切的材质
sampler uImage0 : register(s0);
float2 TargetSize;

float gauss[5][5] =
{
   0.00296902, 0.0133062, 0.02193823, 0.0133062, 0.00296902,
   0.01330621, 0.0596343, 0.09832033, 0.0596343, 0.01330621,
   0.02193823, 0.0983203, 0.16210282, 0.0983203, 0.02193823,
   0.01330621, 0.0596343, 0.09832033, 0.0596343, 0.01330621,
   0.00296902, 0.0133062, 0.02193823, 0.0133062, 0.00296902,
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 color = tex2D(uImage0, input.TextureCoordinates);
    float dx = 2 / TargetSize.x;
    float dy = 2 / TargetSize.y;
    color = float4(0, 0, 0, 0);
    for (int i = -2; i <= 2; i++)
    {
        for (int j = -2; j <= 2; j++)
        {
            color += gauss[i + 2][j + 2] * tex2D(uImage0, float2(input.TextureCoordinates.x + dx * i, input.TextureCoordinates.y + dy * j));
        }
    }
    return color;
}

technique SpriteDrawing
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
};