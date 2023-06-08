using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Metanoia.Content.Projectiles
{
    public class ExplosionLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mjölnir Explosion");
            Main.projFrames[Projectile.type] = 5;
        }
                
        public override void SetDefaults()
        {
            Projectile.damage = 120;
            AIType = ProjectileID.Grenade;
            Projectile.width = 37;
            Projectile.height = 37;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 120;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.velocity = new Vector2(0f, 0f);
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.Resize(50, 50);
            int dust = Dust.NewDust(Projectile.Center, 2, 2, DustID.DrillContainmentUnit, 0f, 0f, 0, default(Color), 2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
            Projectile.Kill();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = new Vector2(0f, 0f);
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.Resize(50, 50);
            int dust = Dust.NewDust(Projectile.Center, 2, 2, DustID.DrillContainmentUnit, 0f, 0f, 0, default(Color), 2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
            return OnTileCollide(oldVelocity);
        }

        public override void AI()
        {

            Projectile.ai[0] += 1f;

            FadeInAndOut();

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.PiOver2;
            }
        }
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 50f)
            {

                Projectile.alpha -= 25;
                if (Projectile.alpha < 100)
                    Projectile.alpha = 100;

                return;
            }

            Projectile.alpha += 25;
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;
        }
    }
}