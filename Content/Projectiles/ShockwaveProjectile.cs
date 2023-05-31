using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Transactions;
using Terraria.Graphics.Effects;

namespace Metanoia.Projectiles
{
    public class ShockwaveProjectile : ModProjectile
    {
        private int rippleCount = 4;
        private int rippleSize = 2;
        private int rippleSpeed = 20;
        private float distortStrength = 20f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockwave");
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.damage = -9999;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 120;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 180)
            {
                Projectile.ai[0] = 1; // Set state to exploded
                Projectile.alpha = 255; // Make the Projectile invisible.
                Projectile.friendly = false; // Stop the bomb from hurting enemies.

                if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Projectile.Center);
                }

                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = (180f - Projectile.timeLeft) / 60f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene["Shockwave"].Deactivate();
            }
        }
    }
}