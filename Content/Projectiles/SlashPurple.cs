using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using System.Transactions;
using Terraria.GameContent.Drawing;

namespace Metanoia.Content.Projectiles
{
    public class SlashPurple : ModProjectile
    {
        public override string GlowTexture => "Metanoia/Content/Projectiles/SlashPurple";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 16;
            // DisplayName.SetDefault("Slash Purple");
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 90;
            Projectile.light = 0.3f;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 30;
            Projectile.alpha = 200;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var settings = new ParticleOrchestraSettings
            {
                PositionInWorld = target.Center,
                MovementVector = target.velocity,
                UniqueInfoPiece = 200
            };
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.ChlorophyteLeafCrystalShot, settings);
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
                Projectile.frameCounter = 4;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    FadeInAndOut();
                    Projectile.Kill();
                }
            }

            Projectile.velocity = Vector2.Zero;
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