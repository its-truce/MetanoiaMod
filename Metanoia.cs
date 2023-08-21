using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Metanoia
{
	public class Metanoia : Mod
	{
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("Metanoia/Content/Effects/ShockwaveEffect", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();

                Ref<Effect> darkScreenRef = new Ref<Effect>(ModContent.Request<Effect>("Metanoia/Content/Effects/DarkScreen", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["DarkScreen"] = new Filter(new ScreenShaderData(darkScreenRef, "TintScreen"), EffectPriority.High);
                Filters.Scene["DarkScreen"].Load();
            }
        }
    }
}