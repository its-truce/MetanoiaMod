using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Projectiles
{
    public class CudgelHoldout3 : ModProjectile
    {
        Player Owner => Main.player[Projectile.owner];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15; // how long you want the trail to be
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // recording mode
        }

        public override void SetDefaults()
        {
            Projectile.damage = 149;
            Projectile.timeLeft = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.width = 258;
            Projectile.height = 258;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
        }

        private int SpawnDebris(Vector2 offset)
        {
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Owner.Center - offset, new Vector2(0f, 0f), ProjectileID.DeerclopsRangedProjectile, 80, 0, Projectile.owner);
            Main.projectile[proj].rotation = MathHelper.ToRadians(180);
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].tileCollide = true;
            Main.projectile[proj].scale = 1.5f;
            Main.projectile[proj].DamageType = DamageClass.Magic;

            return proj;
        }

        public override void AI()
        {
            Owner.heldProj = Projectile.whoAmI;
            Projectile.ai[0] += 1f;
            Projectile.Center = Owner.Center;
            if (Projectile.ai[0] < 61)
            {
                Projectile.rotation += MathHelper.ToRadians(6);
            }
            if (Projectile.ai[0] == 30)
            {
                SpawnDebris(new Vector2(0, 130));
                SpawnDebris(new Vector2(70, 100));
                SpawnDebris(new Vector2(-70, 100));
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Metanoia/Content/Projectiles/CudgelTrail").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                //if (Projectile.oldPos[k] == Vector2.Zero)
                    //return false;
                for (float j = 0.0625f; j < 1; j += 0.0625f)
                {
                    Vector2 oldPosForLerp = k > 0 ? Projectile.oldPos[k - 1] : Projectile.position;
                    Vector2 lerpedPos = Vector2.Lerp(oldPosForLerp, Projectile.oldPos[k], j);
                    float oldRotForLerp = k > 0 ? Projectile.oldRot[k - 1] : Projectile.rotation;
                    float lerpedAngle = Utils.AngleLerp(oldRotForLerp, Projectile.oldRot[k], j);
                    lerpedPos += Projectile.Size / 2;
                    lerpedPos -= Main.screenPosition;
                    Color finalColor = Color.Yellow * 0.3f * (1 - ((float)k / (float)Projectile.oldPos.Length));
                    finalColor.A = 0;//acts like additive blending without spritebatch stuff
                    if (Projectile.friendly)
                        Main.EntitySpriteDraw(texture, lerpedPos, null, finalColor, lerpedAngle, texture.Size() / 2, 1, SpriteEffects.None, 0);
                }
            }
            return true;
        }
    }
}