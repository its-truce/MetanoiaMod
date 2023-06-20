using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Metanoia.Content.Buffs;
using System;

namespace Metanoia.Content.Projectiles
{
    public class ArtemisShot : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4; // how long you want the trail to be
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // recording mode
        }

        public override void SetDefaults()
        {
            Projectile.damage = 140;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 1f;
            Projectile.light = 2f;  
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.position, 0.16f, 1.0f, 0.27f);

            if (Main.rand.Next(1, 5) == 2)
            {
                int dust2 = Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, DustID.TerraBlade, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0.8f;
                Main.dust[dust2].fadeIn = 0.5f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            AdvancedPopupRequest request = default;
            request.Text = "BAM!";
            float seconds = Math.Min(request.Text.Length / 7.5f, 4);
            request.DurationInFrames = (int)(seconds * 60);
            request.Color = new Color(41, 255, 69);
            PopupText.NewText(request, target.Center - new Vector2(0,50));
            var settings = new ParticleOrchestraSettings
            {
                PositionInWorld = target.Center,
                MovementVector = target.velocity
            };
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueNightsEdge, settings);
            if (target.life > 0)
            {
                target.AddBuff(ModContent.BuffType<ArtemisTarget>(), 120, false);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                for (float j = 0.0625f; j < 1; j += 0.0625f)
                {
                    Vector2 oldPosForLerp = k > 0 ? Projectile.oldPos[k - 1] : Projectile.position;
                    Vector2 lerpedPos = Vector2.Lerp(oldPosForLerp, Projectile.oldPos[k], j);
                    float oldRotForLerp = k > 0 ? Projectile.oldRot[k - 1] : Projectile.rotation;
                    float lerpedAngle = Utils.AngleLerp(oldRotForLerp, Projectile.oldRot[k], j);
                    lerpedPos += Projectile.Size / 2;
                    lerpedPos -= Main.screenPosition;
                    Color finalColor = Color.Green * 0.3f * (1 - ((float)k / (float)Projectile.oldPos.Length));
                    finalColor.A = 0;//acts like additive blending without spritebatch stuff
                    if (Projectile.friendly)
                        Main.EntitySpriteDraw(texture, lerpedPos, null, finalColor, Projectile.rotation, texture.Size() / 2, 1, SpriteEffects.None, 0);
                }
            }
            return true;
        }
    }
}