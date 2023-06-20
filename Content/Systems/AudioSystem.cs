using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Metanoia.Content.Systems;

public class AudioSystem : ModSystem
{
    public static SoundStyle ReturnSound(string sound)
    {
        SoundStyle soundStyle = new SoundStyle($"Metanoia/Content/Audio/{sound}");
        return soundStyle;
    }
}