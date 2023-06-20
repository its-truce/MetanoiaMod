using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Projectiles
{
    public class CudgelHoldout2 : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.damage = 118;
            Projectile.timeLeft = 40;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.width = 129;
            Projectile.height = 129;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Owner.heldProj = Projectile.whoAmI;
            Projectile.ai[0] += 1f;
            Projectile.Center = Owner.Center;
            Projectile.rotation += MathHelper.ToRadians(9);
            if (Projectile.ai[0] == 20)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0f, 0f), ModContent.ProjectileType<CudgelShock>(), 120, 0, Projectile.owner);
            }
        }
    }
}