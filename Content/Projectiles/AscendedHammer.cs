using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Security.Policy;
using static Humanizer.In;
using System.Threading;

namespace Metanoia.Projectiles
{
    public class AscendedHammer : ModProjectile
    {
        ref float Timer => ref Projectile.ai[0];
        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;
            DisplayName.SetDefault("Ascended Paladin's Hammer");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7; // how long you want the trail to be
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // recording mode
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            AIType = ProjectileID.PaladinsHammerFriendly;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void AI()
        {
            Projectile.tileCollide = true;
            const int Cutoff = 16;

            if (Projectile.position.HasNaNs())
                Projectile.Kill();

            Timer++;
            Projectile.timeLeft++;
            Projectile.rotation += 0.16f;

            if (Timer > Cutoff)
            {
                Projectile.velocity = Projectile.DirectionTo(Owner.Center) * 14;
                if (Projectile.Hitbox.Intersects(Owner.Hitbox))
                    Projectile.Kill();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Projectile.DirectionTo(Owner.Center) * 14;
            return false;
        }
    }
}