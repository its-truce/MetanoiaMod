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

namespace Metanoia.Content.Projectiles
{
    public class SkofnungHoldout : ModProjectile
    {
        private const int SwingTime = 40;

        private const int CooldownTime = 30;

        private Vector2 StoredVelocity = Vector2.Zero;

        private int initialDirection;
        SoundStyle HitSound = new SoundStyle("Metanoia/Content/Audio/metal");
        public override string Texture => "Metanoia/Content/Items/SkofnungSword";

        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 58;
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
            Projectile.light = 0.5f;
            Projectile.scale *= 1.4f;
        }

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
            Projectile.ai[0] += 1.7f;
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
            HitSound.Volume *= 10f;
            SoundEngine.PlaySound(HitSound);
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

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D glowMask = (Texture2D)ModContent.Request<Texture2D>("Metanoia/Content/Projectiles/SkofnungGlowmask", (AssetRequestMode)2);
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawOrigin = new Vector2((float)projectileTexture.Width * 0.5f, (float)projectileTexture.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor);
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Main.spriteBatch.Draw(projectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            Main.spriteBatch.Draw(glowMask, drawPos, null, Projectile.GetAlpha(Color.White), Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }
    }
}

