﻿using System;
using System.Linq;
using Terraria.ModLoader;

namespace LAP.Core.ParticleSystem
{
    public partial class BaseParticleManager : ModSystem
    {
        #region 更新主要粒子
        public static void UpdatePriorityParticles()
        {
            UpdateAlphaPriorityParticles();
            UpdateNonPremultipliedPriorityParticles();
            UpdateAdditivePriorityParticles();
        }
        public static void UpdateAlphaPriorityParticles()
        {
            if (PriorityActiveParticlesAlpha.Count == 0)
                return;
            for (int i = 0; i < PriorityActiveParticlesAlpha.Count; i++)
            {
                PriorityActiveParticlesAlpha[i].Update();
                PriorityActiveParticlesAlpha[i].Position += PriorityActiveParticlesAlpha[i].Velocity;
                PriorityActiveParticlesAlpha[i].Time++;
            }
            PriorityActiveParticlesAlpha.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        public static void UpdateNonPremultipliedPriorityParticles()
        {
            if (PriorityActiveParticlesNonPremultiplied.Count == 0)
                return;
            for (int i = 0; i < PriorityActiveParticlesNonPremultiplied.Count; i++)
            {
                PriorityActiveParticlesNonPremultiplied[i].Update();
                PriorityActiveParticlesNonPremultiplied[i].Position += PriorityActiveParticlesNonPremultiplied[i].Velocity;
                PriorityActiveParticlesNonPremultiplied[i].Time++;
            }
            PriorityActiveParticlesNonPremultiplied.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        public static void UpdateAdditivePriorityParticles()
        {
            if (PriorityActiveParticlesAdditive.Count == 0)
                return;
            for (int i = 0; i < PriorityActiveParticlesAdditive.Count; i++)
            {
                PriorityActiveParticlesAdditive[i].Update();
                PriorityActiveParticlesAdditive[i].Position += PriorityActiveParticlesAdditive[i].Velocity;
                PriorityActiveParticlesAdditive[i].Time++;
            }
            PriorityActiveParticlesAdditive.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        #endregion
        #region 更新常规粒子
        public static void UpdateParticles()
        {
            UpdateAlphaParticles();
            UpdateNonPremultipliedParticles();
            UpdateAdditiveParticles();
        }
        public static void UpdateAlphaParticles()
        {
            if (ActiveParticlesAlpha.Count == 0)
                return;
            for (int i = 0; i < ActiveParticlesAlpha.Count; i++)
            {
                ActiveParticlesAlpha[i].Update();
                ActiveParticlesAlpha[i].Position += ActiveParticlesAlpha[i].Velocity;
                ActiveParticlesAlpha[i].Time++;
            }
            ActiveParticlesAlpha.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        public static void UpdateNonPremultipliedParticles()
        {
            if (ActiveParticlesNonPremultiplied.Count == 0)
                return;
            for (int i = 0; i < ActiveParticlesNonPremultiplied.Count; i++)
            {
                ActiveParticlesNonPremultiplied[i].Update();
                ActiveParticlesNonPremultiplied[i].Position += ActiveParticlesNonPremultiplied[i].Velocity;
                ActiveParticlesNonPremultiplied[i].Time++;
            }
            ActiveParticlesNonPremultiplied.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        public static void UpdateAdditiveParticles()
        {
            if (ActiveParticlesAdditive.Count == 0)
                return;
            for (int i = 0; i < ActiveParticlesAdditive.Count; i++)
            {
                ActiveParticlesAdditive[i].Update();
                ActiveParticlesAdditive[i].Position += ActiveParticlesAdditive[i].Velocity;
                ActiveParticlesAdditive[i].Time++;
            }
            ActiveParticlesAdditive.RemoveAll(particle =>
            {
                if (particle.Time >= particle.Lifetime)
                {
                    particle.OnKill();
                    return true;
                }
                return false;
            });
        }
        #endregion
    }
}
