using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Projectiles
{
    public class CaliburnusShot : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.penetrate = 4;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.width = 120;
            Projectile.height = 288;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
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
                    Color finalColor = new Color(245, 192, 78) * 0.3f * (1 - ((float)k / (float)Projectile.oldPos.Length));
                    finalColor.A = 0;//acts like additive blending without spritebatch stuff
                    if (Projectile.friendly)
                        Main.EntitySpriteDraw(texture, lerpedPos, null, finalColor, Projectile.rotation, texture.Size() / 2, 1, SpriteEffects.None, 0);
                }
            }
            return true;
        }
    }
}