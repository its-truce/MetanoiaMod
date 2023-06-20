using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Projectiles;

public class SkofnungProj : ModProjectile
{
    private int frameSpeed = 15;

    public override void SetDefaults()
    {
        Projectile.width = 56;
        Projectile.height = 56;
        Projectile.hostile = false;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 600;
        Projectile.scale = 1f;
        Projectile.extraUpdates = 3;
        Projectile.penetrate = 5;
        Projectile.alpha = 250;
        Projectile.scale *= 0.8f;
        Projectile.tileCollide = false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 64 - Projectile.alpha / 4);
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 0.6f, 0.3f, 0.7f);
        if (Projectile.alpha > 0)
        {
            Projectile.alpha--;
        }
        if (Main.rand.Next(1, 30) == 2)
        {
            int dust2 = Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, DustID.CrystalPulse2, Projectile.velocity.X, Projectile.velocity.Y, 100);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.8f;
            Main.dust[dust2].fadeIn = 1f;
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        if (Projectile.ai[0] < 80f)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(-135f);
        }
        if (Projectile.ai[0] == 0f)
        {
            Projectile.velocity *= -0.02f;
        }
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] == 80f)
        {
            Projectile.velocity *= -5f;
        }
        if (Projectile.ai[0] > 80f)
        {
            Projectile.velocity *= 1.01f;
        }
    }

    public override void Kill(int timeLeft)
    {
        SoundStyle WandSound = new SoundStyle("Metanoia/Content/Audio/wand");
        SoundEngine.PlaySound(WandSound, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        for (int i = 0; i < 26; i++)
        {
            int dust2 = Dust.NewDust(Projectile.position, Projectile.width / 3, Projectile.height / 3, DustID.CrystalPulse2, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.2f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 2f;
            Main.dust[dust2].fadeIn = 1.3f;
        }
    }

    public override bool? CanHitNPC(NPC target)
    {
        if (Projectile.ai[0] >= 120f)
        {
            return null;
        }
        return false;
    }
}
