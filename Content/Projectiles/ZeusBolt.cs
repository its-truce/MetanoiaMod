﻿using Metanoia.Content.Players;
using Metanoia.Content.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace Metanoia.Content.Projectiles;

public class ZeusBolt : ModProjectile
{
    Player Owner => Main.player[Projectile.owner];
    public override string Texture => "Metanoia/Content/Projectiles/ShockwaveProjectile";

    public override void SetDefaults()
    {
        // Base stats
        Projectile.width = 40;
        Projectile.height = 40;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 600;
        Projectile.extraUpdates = 200;

        // Weapon stats
        Projectile.friendly = true;
        Projectile.penetrate = 5;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.light = 0.3f;
    }

    private Vector2 startLocation;
    private bool firstFrame = true;

    public override void AI()
    {
        if (firstFrame)
        {
            startLocation = Projectile.Center;
            firstFrame = false;
        }
    }

    public override bool? CanCutTiles() => true;

    public override void Kill(int timeLeft)
    {
        DustSystem.MakeDust(startLocation, Main.MouseWorld, DustID.IceTorch, 1.8f);
        ModContent.GetInstance<CameraSystem>().screenshakeTimer = 2;
        ModContent.GetInstance<CameraSystem>().screenshakeMagnitude = 2;
    }
}