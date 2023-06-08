using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Audio;
using System;
using Metanoia.Content.Players;
using Metanoia.Content.Systems;

namespace Metanoia.Content.Projectiles
{
    public class MjolnirLightning : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];
        SoundStyle ThunderSound = new SoundStyle("Metanoia/Content/Audio/thunder");
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
            ThunderSound.Volume *= 10f;
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
            LightningSystem.MakeDust(startLocation, startLocation + new Vector2(0, 910), DustID.IceTorch, 2.3f);
            Owner.GetModPlayer<ScreenshakePlayer>().screenshakeMagnitude = 4;
            Owner.GetModPlayer<ScreenshakePlayer>().screenshakeTimer = 7;
            SoundEngine.PlaySound(ThunderSound);
        }
    }
}