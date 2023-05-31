using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Metanoia.Players;

namespace Metanoia.Projectiles
{
    public class LightningStrike : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Strike");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 60;
            Projectile.height = 169;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.extraUpdates = 0;
            Projectile.timeLeft = 900;
            Projectile.light = 0.6f;
        }  

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
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

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Owner.GetModPlayer<ScreenshakePlayer>().screenshakeMagnitude = 4;
            Owner.GetModPlayer<ScreenshakePlayer>().screenshakeTimer = 7;
            target.AddBuff(BuffID.Electrified, 7);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(0f, -200f), new Vector2(0f, 20f), ModContent.ProjectileType<ExplosionLightning>(), 120, 6, Projectile.owner);
        }
    }
}