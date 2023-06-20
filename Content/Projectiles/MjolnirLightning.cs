using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Metanoia.Content.Players;
using Metanoia.Content.Systems;
using Terraria.Audio;

namespace Metanoia.Content.Projectiles
{
    public class MjolnirLightning : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lightning Strike");
        }

        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 200;
            Projectile.extraUpdates = 200;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.light = 0.6f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        private Vector2 startLocation;
        private bool firstFrame = true;

        public override void AI()
        {
            if (firstFrame)
            {
                startLocation = Projectile.Center;
                firstFrame = false;
            }
        }

        public override bool? CanCutTiles() => false;

        public override void Kill(int timeLeft)
        {
            DustSystem.MakeDust(startLocation, startLocation + new Vector2(0, 910), DustID.IceTorch, 2.3f);
            ModContent.GetInstance<CameraSystem>().screenshakeTimer = 4;
            ModContent.GetInstance<CameraSystem>().screenshakeMagnitude = 7;
            SoundStyle ThunderSound = AudioSystem.ReturnSound("thunder");
            ThunderSound.Volume *= 10f;
            SoundEngine.PlaySound(ThunderSound);
        }
    }
}