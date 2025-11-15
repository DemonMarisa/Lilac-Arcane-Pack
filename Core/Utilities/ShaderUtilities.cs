using LAP.Assets.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LAP.Core.Utilities
{
    public static partial class LAPUtilities
    {
        public static void FastApplyEdgeMeltsShader(float Opacity, Vector2 TextureSize, Color color, float EdgeWidth = 0.01f, int Pass = 0)
        {
            LAPShaderRegister.EdgeMeltsShader.Parameters["progress"].SetValue(Opacity);
            LAPShaderRegister.EdgeMeltsShader.Parameters["InPutTextureSize"].SetValue(TextureSize);
            LAPShaderRegister.EdgeMeltsShader.Parameters["EdgeColor"].SetValue(color.ToVector4());
            LAPShaderRegister.EdgeMeltsShader.Parameters["EdgeWidth"].SetValue(EdgeWidth);
            LAPShaderRegister.EdgeMeltsShader.CurrentTechnique.Passes[Pass].Apply();
        }
        /// <summary>
        /// 快速使用拖尾Shader
        /// 0材质为Alpha材质
        /// 1材质为噪波材质
        /// 2材质为用于溶解效果的噪波材质
        /// </summary>
        /// <param name="UVOffset">，噪波的材质UV偏移</param>
        /// <param name="NoiseTextureScale">，噪波材质的缩放</param>
        /// <param name="DissolveThreshold">，用于溶解效果的阈值</param>
        /// <param name="DissolveTextureScale">，用于溶解效果的材质的缩放</param>
        /// <param name="DissolveTexturUVOffset">，用于溶解效果的材质的UV偏移</param>
        /// <param name="UseAlphaMult">，是否使用用于Alpha裁剪的材质的像素透明度</param>
        public static void ApplyTrailShader(Vector2 UVOffset, Vector2 NoiseTextureScale, float DissolveThreshold, Vector2 DissolveTextureScale, Vector2 DissolveTexturUVOffset, bool UseAlphaMult)
        {
            LAPShaderRegister.SlashTrailShader.Parameters["UVOffset"].SetValue(UVOffset);
            LAPShaderRegister.SlashTrailShader.Parameters["NoiseTextureScale"].SetValue(NoiseTextureScale);
            LAPShaderRegister.SlashTrailShader.Parameters["DissolveThreshold"].SetValue(DissolveThreshold);
            LAPShaderRegister.SlashTrailShader.Parameters["DissolveTextureScale"].SetValue(DissolveTextureScale);
            LAPShaderRegister.SlashTrailShader.Parameters["DissolveTexturUVOffset"].SetValue(DissolveTexturUVOffset);
            LAPShaderRegister.SlashTrailShader.Parameters["UseAlphaMult"].SetValue(UseAlphaMult);
            LAPShaderRegister.SlashTrailShader.CurrentTechnique.Passes[0].Apply();
        }
        public static void SetTexture(Texture2D texture2D, int Index)
        {
            Main.graphics.GraphicsDevice.Textures[Index] = texture2D;
        }
        public static void SetTexture(Texture2D texture2D, SamplerState samplerState, int Index)
        {
            Main.graphics.GraphicsDevice.Textures[Index] = texture2D;
            Main.graphics.GraphicsDevice.SamplerStates[Index] = samplerState;
        }
    }
}
