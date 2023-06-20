using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Projectiles
{
    public class CudgelShock : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];
        public override string Texture => "Metanoia/Content/Projectiles/ShockwaveProjectile";

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.damage = 900;
            Projectile.friendly = true;
            Projectile.timeLeft = 20;
        }

        bool firstFrame = true;
        public override void AI()
        {
            if (firstFrame)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 32, DustID.Firework_Yellow, speed * 2, Scale: 1.5f);
                    d.noGravity = true;
                }
                firstFrame = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0f, 0f), ModContent.ProjectileType<ShockwaveProjectile>(), 0, 0, Projectile.owner);
        }
    }
}