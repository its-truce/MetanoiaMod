using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Metanoia.Content.Items;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Metanoia.Content.Players;
using Metanoia.Content.Systems;

namespace Metanoia.Content.Projectiles
{
    public class VikingAxeHoldout : ModProjectile
    {
        private const int SwingTime = 40;

        private const int CooldownTime = 30;

        private Vector2 StoredVelocity = Vector2.Zero;

        private int initialDirection;
        public override string Texture => "Metanoia/Content/Items/VikingAxe";

        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Caliburnus");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 2;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            Projectile.scale *= 1.6f;
        }

        int hitTimer = 0;
        bool justHit = false;
        public override void AI()
        {
            if (Projectile.ai[0] == 0f)
            {
                StoredVelocity = Projectile.velocity;
                initialDirection = Owner.direction;
            }
            if (Owner.active)
            {
                Projectile.timeLeft = 2;
            }
            Projectile.velocity = StoredVelocity;
            if (Projectile.ai[0] <= 40f)
            {
                if (initialDirection == -1)
                {
                    Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(MathHelper.SmoothStep(-95f * Projectile.ai[1], 90f * Projectile.ai[1], Projectile.ai[0] / 40f)));
                }
                else
                {
                    Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(MathHelper.SmoothStep(95f * Projectile.ai[1], -90f * Projectile.ai[1], Projectile.ai[0] / 40f)));
                }
            }
            else if (initialDirection == -1)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(MathHelper.SmoothStep(90f * Projectile.ai[1], 100f * Projectile.ai[1], (Projectile.ai[0] - 40f) / 30f)));
            }
            else
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(MathHelper.SmoothStep(-90f * Projectile.ai[1], -100f * Projectile.ai[1], (Projectile.ai[0] - 40f) / 30f)));
            }
            Projectile.velocity.Normalize();
            float timerAdd = 1f;
            if (justHit)
            {
                hitTimer++;
                timerAdd = 0.7f;
            }
            if (hitTimer == 20)
            {
                justHit = false;
                timerAdd = 1f;
            }
            Projectile.ai[0] += timerAdd;
            Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true) + new Vector2((float)(Projectile.width + Projectile.height) * 0.35f, 0f).RotatedBy(Projectile.velocity.ToRotation());
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.direction = initialDirection;
            Projectile.spriteDirection = initialDirection;
            float AdditionalArmRotation = -(float)Math.PI / 2f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += (float)Math.PI - MathHelper.ToRadians(45f);
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            Owner.ChangeDir(initialDirection);
            Owner.heldProj = Projectile.whoAmI;
            Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, Projectile.velocity.ToRotation() + AdditionalArmRotation);
            Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, 0f);
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;
            if (Projectile.ai[0] > 70f)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundStyle HitSound = AudioSystem.ReturnSound("metal");
            HitSound.Volume *= 10f;
            SoundEngine.PlaySound(HitSound);
            ModContent.GetInstance<CameraSystem>().screenshakeTimer = 4;
            ModContent.GetInstance<CameraSystem>().screenshakeMagnitude = 7;
            int numParticles = 75;

            for (int i = 0; i < numParticles; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.Blood, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
            }
            justHit = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] <= 40f)
            {
                return null;
            }
            return false;
        }

        public override bool? CanCutTiles()
        {
            if (Projectile.ai[0] <= 40f)
            {
                return null;
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D ProjectileTexture = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D trail = (Texture2D)ModContent.Request<Texture2D>("Metanoia/Content/Projectiles/VikingHoldoutTrail", (AssetRequestMode)2);
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawOrigin = new Vector2((float)ProjectileTexture.Width * 0.5f, (float)ProjectileTexture.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor);
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Main.spriteBatch.Draw(ProjectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }
    }
}

