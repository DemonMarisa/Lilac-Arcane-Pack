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
            Effect shader = LAPShaderRegister.EdgeMeltsShader.Value;
            shader.Parameters["progress"].SetValue(Opacity);
            shader.Parameters["InPutTextureSize"].SetValue(TextureSize);
            shader.Parameters["EdgeColor"].SetValue(color.ToVector4());
            shader.Parameters["EdgeWidth"].SetValue(EdgeWidth);
            shader.CurrentTechnique.Passes[Pass].Apply();
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
            Effect shader = LAPShaderRegister.SlashTrailShader.Value;
            shader.Parameters["UVOffset"].SetValue(UVOffset);
            shader.Parameters["NoiseTextureScale"].SetValue(NoiseTextureScale);
            shader.Parameters["DissolveThreshold"].SetValue(DissolveThreshold);
            shader.Parameters["DissolveTextureScale"].SetValue(DissolveTextureScale);
            shader.Parameters["DissolveTexturUVOffset"].SetValue(DissolveTexturUVOffset);
            shader.Parameters["UseAlphaMult"].SetValue(UseAlphaMult);
            shader.CurrentTechnique.Passes[0].Apply();
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
