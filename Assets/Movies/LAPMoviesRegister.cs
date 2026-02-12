using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LAP.Assets.Movies
{
    public class LAPMoviesRegister : ModSystem
    {
        public static Asset<Video> Prologue;
        public override void Load()
        {
            Prologue = Request<Video>("LAP/Assets/Movies/Media/Common/Prologue");
        }
        public override void Unload()
        {
            Prologue = null;
        }
    }
}
