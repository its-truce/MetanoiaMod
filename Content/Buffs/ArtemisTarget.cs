using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Metanoia.Content.Buffs
{
    public class ArtemisTarget : ModBuff
    {
        public int Counter;

        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true; // This buff can be applied by other players in Pvp, so we need this to be true.
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            Counter++;
            if (Counter >= 119)
            {
                npc.GetGlobalNPC<ArtemisTargetNPC>().artemisShot = true;
            }
            else
            {
                npc.GetGlobalNPC<ArtemisTargetNPC>().artemisShot = false;
            }
            if (Counter % 20 == 0)
            {
                var settings = new ParticleOrchestraSettings
                {
                    PositionInWorld = npc.Center,
                    MovementVector = npc.velocity
                };
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueNightsEdge, settings);
                int dust2 = Dust.NewDust(npc.position, npc.width / 2, npc.height / 2, DustID.TerraBlade, npc.velocity.X, npc.velocity.Y, 0, default, 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0.8f;
                Main.dust[dust2].fadeIn = 1f;
                Lighting.AddLight(npc.position, 0.16f, 1.0f, 0.27f);
            }
        }
    }

    public class ArtemisTargetNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool artemisShot;

        public override void ResetEffects(NPC npc)
        {
            artemisShot = false;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (artemisShot)
            {
                modifiers.FinalDamage *= 2f;
            }
        }
    }
}
