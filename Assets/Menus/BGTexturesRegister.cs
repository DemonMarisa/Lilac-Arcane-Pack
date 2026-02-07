using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ModLoader;

namespace LAP.Assets.Menus
{
    public class BGTextureRegister : ModSystem
    {
        public static Asset<Texture2D> LiliesLogo { get; private set; }
        public static Asset<Texture2D> Galaxy { get; private set; }
        public static Asset<Texture2D> LiliesMenu2 { get; private set; }
        public static Asset<Texture2D> MagnoliaLogo { get; private set; }
        public static Asset<Texture2D> MagnoliaMenu1 { get; private set; }
        public static Asset<Texture2D> RainDrop { get; private set; }
        public static Asset<Texture2D> LiliesCross { get; private set; }
        public static Asset<Texture2D> Line { get; private set; }
        public static Asset<Texture2D> Bloom { get; private set; }
        public static Asset<Texture2D> Input_Keyboard { get; private set; }
        public override void Load()
        {
            LiliesLogo = Request<Texture2D>("LAP/Assets/Menus/Textures/LiliesLogo");
            Galaxy = Request<Texture2D>("LAP/Assets/Menus/Textures/Galaxy");
            LiliesMenu2 = Request<Texture2D>("LAP/Assets/Menus/Textures/LiliesMenu2");
            MagnoliaLogo = Request<Texture2D>("LAP/Assets/Menus/Textures/MagnoliaLogo");
            MagnoliaMenu1 = Request<Texture2D>("LAP/Assets/Menus/Textures/MagnoliaMenu1");
            RainDrop = Request<Texture2D>("LAP/Assets/Menus/Textures/RainDrop");
            LiliesCross = Request<Texture2D>("LAP/Assets/Menus/Textures/LiliesToWeb");
            Line = Request<Texture2D>("LAP/Assets/Menus/Textures/Line");
            Bloom = Request<Texture2D>("LAP/Assets/Menus/Textures/Bloom");
            Input_Keyboard = Request<Texture2D>("LAP/Assets/Menus/Textures/Input_Keyboard");
        }
        public override void Unload()
        {
            LiliesLogo = null;
            Galaxy = null;
            LiliesMenu2 = null;
            MagnoliaLogo = null;
            MagnoliaMenu1 = null;
            RainDrop = null;
            LiliesCross = null;
            Line = null;
            Bloom = null;
            Input_Keyboard = null;
        }
    }
    public class MenuSounds : ModSystem
    {
        public static SoundStyle Click => new SoundStyle("LAP/Assets/Menus/Sounds/Click") { Volume = 1f, PitchVariance = 0f, };
        public static SoundStyle Hover => new SoundStyle("LAP/Assets/Menus/Sounds/Hover") { Volume = 1f, PitchVariance = 0f, };
        public static SoundStyle LiliesOut => new SoundStyle("LAP/Assets/Menus/Sounds/LiliesOut") { Volume = 1f, PitchVariance = 0f, };
        public static SoundStyle MagnoliaOut => new SoundStyle("LAP/Assets/Menus/Sounds/MagnoliaOut") { Volume = 1f, PitchVariance = 0f, };
    }
}
