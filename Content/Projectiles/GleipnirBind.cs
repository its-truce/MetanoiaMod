using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Transactions;

namespace Metanoia.Content.Projectiles
{
    public class GleipnirBind : ModProjectile
    {
        ref float Timer => ref Projectile.ai[0];
        bool hittingNPC = false;
        NPC targetX = null;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gleipnir Bind");
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 60;
            Projectile.height = 26;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 6000;
            Projectile.light = 0.6f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Projectile.rotation = 0f;
            Projectile.ai[0] += 1f;

            FadeInAndOut();

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Timer++;
            int target = Projectile.FindTargetWithLineOfSight(120);

            if (target != -1 && Main.npc[target].life > 1)
            {
                targetX = Main.npc[target];
                Projectile.position = targetX.Center + new Vector2(-30f, 0);
            }
            else
            {
                Projectile.alpha = 50;
                Projectile.Kill();
                hittingNPC = false;
                targetX = null;
                Projectile.Kill();
            }

            //if (hittingNPC && targetX != null)
            //{
            //    Projectile.position = targetX.position;
            //}

            const int Cutoff = 120;
            if (Timer > Cutoff)
            {
                Projectile.alpha = 50;
                Projectile.Kill();
                hittingNPC = false;
                targetX = null;
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            targetX = target;
            hittingNPC = true;
            if (target.life == 0)
            {
                Projectile.alpha = 50;
                Projectile.Kill();
                hittingNPC = false;
                targetX = null;
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