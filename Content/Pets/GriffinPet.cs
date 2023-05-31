using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Pets
{
    public class GriffinPet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LilHarpy); // Copy the stats of the Zephyr Fish

            AIType = ProjectileID.LilHarpy; // Copy the AI of the Zephyr Fish.
            Projectile.frameCounter = 10;
            Projectile.bobber = false;
        }

        public override bool PreAI()
        {
            Projectile.velocity *= 1.025f;
            Player player = Main.player[Projectile.owner];

            player.petFlagLilHarpy = false; // Relic from aiType

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(ModContent.BuffType<GriffinBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}