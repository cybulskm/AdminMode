using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(KillLocalPlayer))]
    internal class KillPlayerPatch
    {
        [HarmonyPatch(nameof(KillLocalPlayer.KillPlayer))]
        [HarmonyPrefix]
        static bool DontKillPlayer()
        {
            return false;
        }
    }
}
