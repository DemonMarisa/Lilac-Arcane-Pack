using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.Movies
{
    public class LAPMoviesRegister : ModSystem
    {
        public static Asset<Video> Prologue;
        public static Asset<Video> LiliesCreditsC;
        public override void Load()
        {
            Prologue = Request<Video>("LAP/Assets/Movies/Media/Common/Prologue");
            LiliesCreditsC = Request<Video>("LAP/Assets/Movies/Media/Common/LiliesCreditsC");
        }
        public override void Unload()
        {
            Prologue = null;
            LiliesCreditsC = null;
        }
    }
}
