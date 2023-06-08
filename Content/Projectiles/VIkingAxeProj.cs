using Metanoia.Content.Players;
using Metanoia.Content.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace Metanoia.Content.Projectiles;

public class VikingAxeProj : ModProjectile
{
    ref float Timer => ref Projectile.ai[0];
    Player Owner => Main.player[Projectile.owner];
    SoundStyle HitSound = new SoundStyle("Metanoia/Content/Audio/metal");
    public override string Texture => "Metanoia/Content/Items/VikingAxe";
    bool Hit = false;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // how long you want the trail to be
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // recording mode
    }

    public override void SetDefaults()
    {
        // Base stats
        Projectile.width = 42;
        Projectile.height = 42;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 600;

        // Weapon stats
        Projectile.friendly = true;
        Projectile.penetrate = 10;
        Projectile.DamageType = DamageClass.MeleeNoSpeed;
        Projectile.light = 0.1f;
        Projectile.scale *= 1.5f;
        Projectile.tileCollide = true;
    }

    public override void AI()
    {
        if (Hit)
        {
            Projectile.velocity = Projectile.DirectionTo(Owner.Center) * 14;
            if (Projectile.Hitbox.Intersects(Owner.Hitbox))
                Projectile.Kill();
        }
        Projectile.rotation += MathHelper.ToRadians(25);
        const int Cutoff = 37;

        if (Projectile.position.HasNaNs())
            Projectile.Kill();

        Timer++;
        Projectile.timeLeft++;

        if (Timer > Cutoff)
        {
            Projectile.velocity = Projectile.DirectionTo(Owner.Center) * 14;
            if (Projectile.Hitbox.Intersects(Owner.Hitbox))
                Projectile.Kill();
        }
    }

    public override bool? CanCutTiles() => true;

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Owner.GetModPlayer<ScreenshakePlayer>().screenshakeMagnitude = 8;
        Owner.GetModPlayer<ScreenshakePlayer>().screenshakeTimer = 13;
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        HitSound.Volume *= 10f;
        SoundEngine.PlaySound(HitSound);
        Hit = true;
        return false;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Owner.GetModPlayer<ScreenshakePlayer>().screenshakeMagnitude = 8;
        Owner.GetModPlayer<ScreenshakePlayer>().screenshakeTimer = 13;
        HitSound.Volume *= 10f;
        SoundEngine.PlaySound(HitSound);;
        Hit = true;
        int numParticles = 75;

        for (int i = 0; i < numParticles; i++)
        {
            Dust.NewDust(target.position, target.width, target.height, DustID.Blood, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
        }
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Projectile.type);
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

        Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        }

        return true;
    }
}