using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Projectiles
{
    public class CudgelHoldout1 : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.damage = 92;
            Projectile.timeLeft = 20;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Owner.heldProj = Projectile.whoAmI;
            Projectile.Center = Owner.Center;
            Projectile.rotation += MathHelper.ToRadians(18);
        }
    }
}