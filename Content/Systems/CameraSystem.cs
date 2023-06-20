using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Projectiles;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Metanoia.Content.Players
{
    public class CameraSystem : ModSystem
    {
        public int screenshakeTimer;
        public int screenshakeMagnitude;

        public override void ModifyScreenPosition()
        {
            screenshakeTimer--;
            if (screenshakeTimer > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(screenshakeMagnitude * -1, screenshakeMagnitude + 1), Main.rand.Next(screenshakeMagnitude * -1, screenshakeMagnitude + 1));
            }
        }
    }
}