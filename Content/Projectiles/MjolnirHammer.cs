using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Security.Policy;
using Terraria.Audio;

namespace Metanoia.Content.Projectiles
{
    public class MjolnirHammer : ModProjectile
    {
        ref float Timer => ref Projectile.ai[0];
        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;
            // DisplayName.SetDefault("Thor's Hammer");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7; // how long you want the trail to be
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            // Projectile.timeLeft = 420;
            // Projectile.light = 0.25f;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Projectile.tileCollide = false;
            const int Cutoff = 30;
            const float MaxSpeed = 12;

            if (Projectile.position.HasNaNs())
                Projectile.Kill();

            Timer++;
            Projectile.timeLeft++;
            Projectile.rotation += 0.16f;

            if (Timer > Cutoff)
            {
                float factor = 2.2f * MathHelper.Min((Timer - Cutoff) / (Cutoff + 20f), 1f);
                float angle = Utils.AngleLerp(Projectile.velocity.ToRotation(), Projectile.AngleTo(Owner.Center), factor) - Projectile.velocity.ToRotation();
                Projectile.velocity = Projectile.velocity.RotatedBy(angle);

                if (Projectile.velocity.LengthSquared() > MaxSpeed * MaxSpeed)
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * MaxSpeed;

                if (Projectile.Hitbox.Intersects(Owner.Hitbox))
                    Projectile.Kill();

                int dust = Dust.NewDust(Projectile.Center, 2, 2, DustID.DrillContainmentUnit, 0f, 0f, 0, default(Color), 0.6f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.3f;
            }
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

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Projectile.DirectionTo(Owner.Center) * 14;
            return false;
        }

        int lightningCount = 0;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            lightningCount++;
            if (lightningCount == 1)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(0, -900), new Vector2(0f, 120f), ModContent.ProjectileType<MjolnirLightning>(), 460, 0, Projectile.owner);
                lightningCount = 0;
                int critChance = Main.rand.Next(1, 4);
                int lightningDamage;
                bool critY;
                if (critChance == 1)
                {
                    critY = true;
                    lightningDamage = 800;
                }
                else
                {
                    critY = false;
                    lightningDamage = 400;
                }
                NPC.HitInfo lightningHit = new NPC.HitInfo()
                {
                    Damage = lightningDamage,
                    Crit = critY,
                    DamageType = DamageClass.MeleeNoSpeed
                };
                target.StrikeNPC(lightningHit, false, false);
                NetMessage.SendStrikeNPC(target, lightningHit, -1);
            }
        }
    }
}