using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Metanoia.Content.Systems;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Metanoia.Content.Projectiles
{
    public class CaliburnusHoldout : ModProjectile
    {
        private const int SwingTime = 40;

        private const int CooldownTime = 30;

        private Vector2 StoredVelocity = Vector2.Zero;

        private int initialDirection;
        public override string Texture => "Metanoia/Content/Items/Caliburnus";

        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Caliburnus");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 112;
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
            Projectile.light = 1.2f;
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
            Projectile.ai[0] += 1.35f;
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
            target.AddBuff(BuffID.OnFire3, 120);
            target.AddBuff(BuffID.OnFire, 120);
            SoundStyle HitSound = AudioSystem.ReturnSound("metal");
            HitSound.Volume *= 10f;
            SoundEngine.PlaySound(HitSound);
            Vector2 direction = Projectile.DirectionTo(Main.MouseWorld) * 14;
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
            Texture2D ProjectileTexture = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D trail = (Texture2D)ModContent.Request<Texture2D>("Metanoia/Content/Projectiles/CaliburnusHoldoutTrail", (AssetRequestMode)2);
            Texture2D glowMask = (Texture2D)ModContent.Request<Texture2D>("Metanoia/Content/Projectiles/CaliburnusGlowmask", (AssetRequestMode)2);
            Texture2D glowMaskWhite = (Texture2D)ModContent.Request<Texture2D>("Metanoia/Content/Projectiles/CaliburnusGlowmaskWhite", (AssetRequestMode)2);
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawOrigin = new Vector2((float)ProjectileTexture.Width * 0.5f, (float)ProjectileTexture.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor);
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int j = 0; j < Projectile.oldPos.Length; j++)
            {
                Vector2 drawPosEffect = Projectile.oldPos[j] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color colorAfterEffect = Projectile.GetAlpha(Color.Lerp(new Color(245, 192, 78, 200), new Color(248, 151, 76, 200), Projectile.ai[0] / 70f)) * ((float)(Projectile.oldPos.Length - j) / (float)Projectile.oldPos.Length) * 0.5f;
                float AfterAffectScale = Projectile.scale - (float)j / (float)Projectile.oldPos.Length;
                Main.spriteBatch.Draw(trail, drawPosEffect, null, colorAfterEffect, Projectile.oldRot[j], drawOrigin, AfterAffectScale, effects, 0f);
            }
            for (int i = 0; i < 5; i++)
            {
                Main.spriteBatch.Draw(glowMaskWhite, drawPos, null, Color.Lerp(new Color(245, 192, 78, 200), new Color(248, 151, 76, 200), Projectile.ai[0] / 70f) * 0.05f, Projectile.rotation, drawOrigin, Projectile.scale + 0.1f * (float)i, effects, 0f);
            }
            Main.spriteBatch.Draw(ProjectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            Main.spriteBatch.Draw(glowMask, drawPos, null, Projectile.GetAlpha(Color.White), Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }
    }
}

