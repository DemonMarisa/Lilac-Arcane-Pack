using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace LAP.Core.Graphics.Lightning
{
    public class LAPLightning
    {
        public float RandomFlowOffset;
        public float xScale;
        public bool Active;
        public int WhoAmI;
        public int lifeTime;
        public int MaxlifeTime;
        public List<Vector2> Nodes = new();
        public float Width;
        public Color Color;
        public Color GlowColor;
        public float Opacity;
        public void Update()
        {
            float progress = (float)lifeTime / MaxlifeTime;
            xScale = MathHelper.Lerp(xScale, 1f, 0.5f);
            Opacity = MathHelper.Lerp(1f, 0f, EasingHelper.EaseOutCubic(progress));
            lifeTime++;
            if (lifeTime > MaxlifeTime)
                Active = false;
        }
    }
}
