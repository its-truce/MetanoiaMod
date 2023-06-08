using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Metanoia.Content.Projectiles
{
    public class Glowball : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            // Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.damage = 140;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 1f;
            Projectile.light = 2f;  
            Projectile.width = 116;
            Projectile.height = 116;
            Projectile.scale *= 1f;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            Main.EntitySpriteDraw(texture, Owner.Center, null, new Color(255, 255, 255, 0), Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0);
            return true;
        }
    }
}