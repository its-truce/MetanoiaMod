using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Transactions;
using Terraria.Graphics.Effects;
using Microsoft.CodeAnalysis;
using Terraria.GameContent.Drawing;

namespace Metanoia.Content.Projectiles
{
    public class BidentHand : ModProjectile
    {
        int timer = 120;
        NPC targetX = null;
        bool hitNPC;
        Player Owner => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaper Claw");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // how long you want the trail to be
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // recording mode
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 70;
            Projectile.height = 78;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.damage = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 120;
            Projectile.light = 0.7f;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the Projectile with the color not influenced by light
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

        public override void AI()
        {
            Projectile.rotation = MathHelper.ToRadians(90);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life > 1 && target.type != NPCID.TargetDummy)
            {
                Vector2 direction = Owner.Center - target.Center;
                direction.Normalize();
                target.velocity = (direction) * 5;
            }
        }
    }
}