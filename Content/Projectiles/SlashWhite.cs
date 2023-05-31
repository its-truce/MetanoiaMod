using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using System.Transactions;
using Terraria.GameContent.Drawing;

namespace Metanoia.Projectiles
{
    public class SlashWhite : ModProjectile
    {
        public override string GlowTexture => "Metanoia/Projectiles/SlashWhite";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            DisplayName.SetDefault("Slash White");
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 40;
            Projectile.light = 0.3f;
            Projectile.alpha = 255;
            Projectile.damage = 30;
            Projectile.scale *= 1.6f;
            Projectile.DamageType = DamageClass.Magic;
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
                Projectile.frameCounter = 2;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    FadeInAndOut();
                    Projectile.Kill();
                }
            }

            Projectile.velocity = Vector2.Zero;
            Projectile.alpha -= 25;
            if (Projectile.alpha <= 3)
            {
                Projectile.Kill();
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
            var settings = new ParticleOrchestraSettings
            {
                PositionInWorld = target.Center,
                MovementVector = target.velocity
            };
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StellarTune, settings);
        }
    }
}