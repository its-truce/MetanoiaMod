using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Projectiles
{
    public class VikingAxeHoldout : ModProjectile
    {
        private const int SwingTime = 40;

        private const int CooldownTime = 30;

        private Vector2 StoredVelocity = Vector2.Zero;

        private int initialDirection;

        public override string Texture => "Metanoia/Items/VikingAxe";

        Player Owner => Main.player[Projectile.owner];

        private bool Thrown = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viking Axe");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 58;
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
            if (Projectile.ai[0] < 65f)
            {
                Projectile.velocity = StoredVelocity;
            }
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
            else if (initialDirection == -1 && Projectile.ai[0] < 65f)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(MathHelper.SmoothStep(90f * Projectile.ai[1], 100f * Projectile.ai[1], (Projectile.ai[0] - 40f) / 30f)));
            }
            else if (Projectile.ai[0] < 65f)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(MathHelper.SmoothStep(-90f * Projectile.ai[1], -100f * Projectile.ai[1], (Projectile.ai[0] - 40f) / 30f)));
            }
            Projectile.ai[0] += 1.15f;
            if (Projectile.ai[0] < 65f)
            {
                Projectile.velocity.Normalize();
                Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true) + new Vector2((float)(Projectile.width + Projectile.height) * 0.35f, 0f).RotatedBy(Projectile.velocity.ToRotation());
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.direction = initialDirection;
                Projectile.spriteDirection = initialDirection;
            }
            float AdditionalArmRotation = -(float)Math.PI / 2f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += (float)Math.PI - MathHelper.ToRadians(45f);
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            if (Projectile.ai[0] < 65f)
            {
                Owner.ChangeDir(initialDirection);
                Owner.heldProj = Projectile.whoAmI;
                Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, Projectile.velocity.ToRotation() + AdditionalArmRotation);
                Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, 0f);
                Owner.itemTime = 2;
                Owner.itemAnimation = 2;
            }
            if (Projectile.ai[0] > 65f) // done swinging
            {
                Vector2 direction = Main.MouseWorld;
                direction.Normalize();
                Thrown = true;
                // Projectile.Kill();
                Projectile.velocity = direction * 10;
                Projectile.rotation += 0.5f;
            }
            if (Projectile.ai[0] > 150)
            {
                Projectile.Kill();
            }   
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] <= 40f)
            {
                return null;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Thrown)
            {
                Projectile.Kill();
                Thrown = false;
            }
            else
            {
                ; // do nothing
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Thrown)
            {
                Projectile.Kill();
                Thrown = false;
            }
            else
            {
                ; // do nothing
            }
            return true;
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
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}

