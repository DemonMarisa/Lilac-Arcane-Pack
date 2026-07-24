using LAP.Core.Graphics.VFX;
using Microsoft.Xna.Framework;

namespace LAP.Core.SystemsLoader
{
    public static partial class LAPContent
    {
        public static void Kill(this VFXInstance vfx)
        {
            vfx.Time = vfx.Lifetime;
            vfx.Active = false;
            vfx.Behavior.OnKill();
        }
        public static VFXInstance SpawnVFX(int Type, Vector2 position, Vector2 velocity, Color drawColor, float rotation = 0, float scale = 0, float aio = 0, float ai1 = 0, float ai2 = 0)
        {
            for (int i = 0; i < VFXManager.MaxVFXPerPool; i++)
            {
                VFXInstance vfx = VFXManager.VFXInstances[i];
                if (vfx != null && !vfx.Active)
                {
                    vfx.Reset();
                    VFXBehavior vfxBeh = VFXManager.VBehavior[Type];
                    vfx.Behavior = vfxBeh.CloneForSpawner();
                    vfx.Behavior.VFXInstance = VFXManager.VFXInstances[i];

                    vfx.Position = position;
                    vfx.Velocity = velocity;
                    vfx.Time = 0;
                    vfx.ExtraUpdate = 0;
                    vfx.Scale = scale;
                    vfx.Scale2D = Vector2.One * scale;
                    vfx.Opacity = 1f;
                    vfx.DrawColor = drawColor;
                    vfx.Rotation = rotation;
                    vfx.OldPos.Clear();
                    vfx.OldRot.Clear();
                    vfx.Active = true;
                    vfx.AiFloat[0] = aio;
                    vfx.AiFloat[1] = ai1;
                    vfx.AiFloat[2] = ai2;

                    VFXManager.HasAnyVFX = true;

                    vfx.Behavior.OnSpawn();
                    return vfx;
                }
            }
            return null;
        }
    }
}
